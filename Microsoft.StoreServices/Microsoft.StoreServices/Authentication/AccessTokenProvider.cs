//-----------------------------------------------------------------------------
// AccessTokenProvider.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.StoreServices
{
    /// <summary>
    /// An IAccessTokenProvider that generates access tokens required for Microsoft Store
    /// Services authentication.
    /// </summary>
    public class AccessTokenProvider : IAccessTokenProvider
    {
        /// <summary>
        /// Can be overridden with an HttpClientFactory.CreateClient() if used by your service.
        /// Ex: AccessTokenProvider.CreateHttpClientFunc = httpClientFactory.CreateClient;
        /// </summary>
        public static Func<HttpClient> CreateHttpClientFunc = () => new HttpClient();

        /// <summary>
        /// Registered AAD Tenant Id for your service.
        /// </summary>
        protected string _tenantId;

        /// <summary>
        /// Registered AAD Client Id for your service.
        /// </summary>
        protected string _clientId;

        /// <summary>
        /// Registered AAD Client secret for your service.
        /// </summary>
        protected string _clientSecret;

        /// <summary>
        /// Generates an access token provider based on your AAD credentials passed in that will generate
        /// access tokens required to authenticate with the Microsoft Store Services.
        /// </summary>
        /// <param name="tenantId">Registered AAD Tenant Id for your service</param>
        /// <param name="clientId">Registered AAD Client Id for your service</param>
        /// <param name="clientSecret">Registered AAD Client secret for your service</param>
        public AccessTokenProvider(string tenantId, string clientId, string clientSecret)
        {
            if (string.IsNullOrEmpty(tenantId))
            {
                throw new ArgumentException($"{nameof(_tenantId)} required", nameof(_tenantId));
            }
            if (string.IsNullOrEmpty(clientId))
            {
                throw new ArgumentException($"{nameof(_clientId)} required", nameof(_clientId));
            }
            if (string.IsNullOrEmpty(clientSecret))
            {
                throw new ArgumentException($"{nameof(_clientSecret)} required", nameof(_clientSecret));
            }

            _tenantId = tenantId;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public virtual Task<AccessToken> GetServiceAccessTokenAsync()
        {
            return CreateAccessTokenAsync(AccessTokenAudienceTypes.Service);
        }

        public virtual Task<AccessToken> GetCollectionsAccessTokenAsync()
        {
            return CreateAccessTokenAsync(AccessTokenAudienceTypes.Collections);
        }

        public virtual Task<AccessToken> GetPurchaseAccessTokenAsync()
        {
            return CreateAccessTokenAsync(AccessTokenAudienceTypes.Purchase);
        }

        /// <summary>
        /// Generates an access token based on the URI audience value passed in.  
        /// provided.
        /// </summary>
        /// <param name="audience">Audience URI defining the token (see AccessTokenAudienceTypes)</param>
        /// <returns>Access token, otherwise Exception will be thrown</returns>
        protected virtual async Task<AccessToken> CreateAccessTokenAsync(string audience)
        {
            //  Validate we have the needed values
            if (string.IsNullOrEmpty(audience))
            {
                throw new ArgumentException($"{nameof(audience)} required", nameof(audience));
            }

            //  URL encode the Secret key to ensure it gets properly transmitted if containing
            //  characters such as '%'.  We just encode the secret so the rest of the body is
            //  easily read in debugging tools such as Fiddler.
            var encodedSecret = System.Web.HttpUtility.UrlEncode(_clientSecret);

            //  Build the HTTP request information to generate the access token.  We are using
            //  Azure AD v2.0 to generate the tokens. See the following:
            //  https://docs.microsoft.com/en-us/azure/active-directory/develop/v2-oauth2-client-creds-grant-flow

            var requestUri = $"https://login.microsoftonline.com/{_tenantId}/oauth2/v2.0/token";
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUri.ToString());
            var requestBody = $"grant_type=client_credentials&client_id={_clientId}" +
                              $"&client_secret={encodedSecret}" +
                              $"&scope={audience}/.default";

            httpRequest.Content = new StringContent(requestBody, Encoding.UTF8, "application/x-www-form-urlencoded");

            // Post the request and wait for the response
            var httpClient = CreateHttpClientFunc();
            using (var httpResponse = await httpClient.SendAsync(httpRequest))
            {
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.IsSuccessStatusCode)
                {
                    var token = JsonConvert.DeserializeObject<AccessToken>(responseBody);
                    if (string.IsNullOrEmpty(token.Audience))
                    {
                        //  The Azure v2.0 AAD URI doesn't pass back the audience in the request body
                        //  so we copy it from the audience that we put in the request
                        token.Audience = audience;
                    }
                    return token;
                }
                else
                {
                    throw new StoreServicesHttpResponseException($"Unable to acquire access token for {audience} : {httpResponse.ReasonPhrase}", httpResponse);
                }
            }
        }
    }
}
