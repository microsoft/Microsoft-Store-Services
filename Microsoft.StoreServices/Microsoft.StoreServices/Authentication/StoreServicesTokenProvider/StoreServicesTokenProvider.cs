//-----------------------------------------------------------------------------
// StoreServicesTokenProvider.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using Azure.Core;
using Azure.Identity;
using Microsoft.StoreServices.Authentication;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.StoreServices
{
    /// <summary>
    /// An IStoreServicesTokenProvider that generates access tokens required for Microsoft Store
    /// Services authentication.
    /// </summary>
    public class StoreServicesTokenProvider : IStoreServicesTokenProvider
    {
        /// <summary>
        /// Audience value used for the Managed Identity token exchange.
        /// </summary>
        private const string AzureADTokenExchangeAudience = "api://AzureADTokenExchange";

        /// <summary>
        /// Can be overridden with an HttpClientFactory.CreateClient() if used by your service.
        /// Ex: StoreServicesTokenProvider.CreateHttpClientFunc = httpClientFactory.CreateClient;
        /// </summary>
        public static Func<HttpClient> CreateHttpClientFunc = () => new HttpClient();

        /// <summary>
        /// Registered Entra Tenant Id for your service.
        /// </summary>
        protected string _tenantId;

        /// <summary>
        /// Registered Entra Client Id for your service.
        /// </summary>
        protected string _clientId;

        /// <summary>
        /// Registered Entra Client secret for your service.
        /// </summary>
        protected string _clientSecret;

        /// <summary>
        /// Registered Entra Managed ID for your service.
        /// </summary>
        protected string _managedId;

        /// <summary>
        /// Generates an access token provider based on your Entra credentials passed in with a secret key
        /// The Access Tokens are required to authenticate with the Microsoft Store Services.  This API 
        /// takes a secret key, for Azure Managed Identity use the overloaded constructor.
        /// </summary>
        /// <param name="tenantId">Registered AAD Tenant Id for your service</param>
        /// <param name="clientId">Registered AAD Client Id for your service</param>
        /// <param name="clientSecret">Registered AAD Client secret for your service</param>
        public StoreServicesTokenProvider(string tenantId, string clientId, string clientSecret)
        {
            if (string.IsNullOrEmpty(tenantId))
            {
                throw new ArgumentException($"{nameof(tenantId)} required", nameof(tenantId));
            }
            if (string.IsNullOrEmpty(clientId))
            {
                throw new ArgumentException($"{nameof(clientId)} required", nameof(clientId));
            }
            if (string.IsNullOrEmpty(clientSecret))
            {
                throw new ArgumentException($"{nameof(clientSecret)} required", nameof(clientSecret));
            }

            _tenantId = tenantId;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _managedId = null;
        }

        /// <summary>
        /// Generates an access token provider based on your Entra credentials passed in with either a
        /// secret key or Azure Managed Identity. The Access Tokens are required to authenticate with 
        /// the Microsoft Store Services.
        /// </summary>
        /// <param name="tenantId">The Entra Tenant Id registered for your service. Used to identify the Azure Active Directory tenant.</param>
        /// <param name="clientId">The Entra Client Id registered for your service. Used to identify the application requesting authentication.</param>
        /// <param name="secretOrManagedId">The Entra Client secret or Managed Identity value. Used for authenticating the application, depending on the value of useManagedId.</param>
        /// <param name="useManagedId">If true, secretOrManagedId is treated as a Managed Identity; if false, it is treated as a client secret.</param>
        /// <exception cref="ArgumentException"></exception>
        public StoreServicesTokenProvider(string tenantId, string clientId, string secretOrManagedId, bool useManagedId = false)
        {
            if (string.IsNullOrEmpty(tenantId))
            {
                throw new ArgumentException($"{nameof(tenantId)} required", nameof(tenantId));
            }
            if (string.IsNullOrEmpty(clientId))
            {
                throw new ArgumentException($"{nameof(clientId)} required", nameof(clientId));
            }
            if (string.IsNullOrEmpty(secretOrManagedId))
            {
                throw new ArgumentException($"{nameof(secretOrManagedId)} required", nameof(secretOrManagedId));
            }

            _tenantId = tenantId;
            _clientId = clientId;

            //  Write the values based on if the caller specified they passed in a secret key or a managed id
            if (useManagedId)
            {
                _managedId = secretOrManagedId;
                _clientSecret = null;
            }
            else
            {
                _clientSecret = secretOrManagedId;
                _managedId = null;
            }
        }

        /// <summary>
        /// Creates a new Service Access Token
        /// </summary>
        /// <returns></returns>
        public virtual Task<AccessToken> GetServiceAccessTokenAsync()
        {
            return CreateAccessTokenAsync(AccessTokenAudienceTypes.Service);
        }

        // <summary>
        /// Creates a new Collections Access Token
        /// </summary>
        /// <returns></returns>
        public virtual Task<AccessToken> GetCollectionsAccessTokenAsync()
        {
            return CreateAccessTokenAsync(AccessTokenAudienceTypes.Collections);
        }

        // <summary>
        /// Creates a new Purchase Access Token
        /// </summary>
        /// <returns></returns>
        public virtual Task<AccessToken> GetPurchaseAccessTokenAsync()
        {
            return CreateAccessTokenAsync(AccessTokenAudienceTypes.Purchase);
        }

        /// <summary>
        /// Generates an access token based on the URI audience value passed in.  
        /// provided.
        /// </summary>
        /// <param name="audience">audience URI defining the token (see AccessTokenAudienceTypes)</param>
        /// <returns>Access token, otherwise Exception will be thrown</returns>
        protected virtual async Task<AccessToken> CreateAccessTokenAsync(string audience)
        {
            //  Validate we have the needed values
            if (string.IsNullOrEmpty(audience))
            {
                throw new ArgumentException($"{nameof(audience)} required", nameof(audience));
            }

            var accessToken = new AccessToken();
            if (!string.IsNullOrEmpty(_clientSecret))
            {
                accessToken = await CreateAccessTokenFromSecret(audience);
            }
            else if (!string.IsNullOrEmpty(_managedId)) 
            {
                accessToken = await CreateAccessTokenFromManagedIdentity(audience);
            }
            else
            {
                throw new ArgumentException($"Secret key or Managed ID required when creating token provider", "SecretOrManagedId");
            }

            return accessToken;
        }

        /// <summary>
        /// Creates an Access Token based on the Secret Key provided when creating the Token Provider.
        /// </summary>
        /// <param name="audience"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="StoreServicesHttpResponseException"></exception>
        protected virtual async Task<AccessToken> CreateAccessTokenFromSecret(string audience)
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
                        //  The Azure v2.0 Entra ID URI doesn't pass back the audience in the request body
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

        /// <summary>
        /// Creates an access token for an Entra ID app, but uses Managed Identity assigned to the Azure Resource / VM 
        /// instead of a Secret key.
        /// </summary>
        /// <param name="audience">Target audience.</param>
        /// <returns>If successful, returns managed identity token.</returns>
        protected virtual async Task<AccessToken> CreateAccessTokenFromManagedIdentity(string audience)
        {
            ClientAssertionCredential assertion = new(
                _tenantId,
                _clientId,
                 async (token) => await GetManagedIdentityToken(_managedId, AzureADTokenExchangeAudience));
            
            //  The scopes need to end with "/.default" here to work
            string[] scopes = { $"{audience}/.default" };
            
            // Request an access token for our Client ID using the managed ID credentials as the auth / secret
            var token = await assertion.GetTokenAsync(new TokenRequestContext(scopes));
            
            var convertedToken = new AccessToken()
            {
                Audience = audience,
                ExpiresOn = token.ExpiresOn,
                Token = token.Token
            };
            
            return convertedToken;
        }

        /// <summary>
        /// Gets a token for the user-assigned Managed Identity.
        /// </summary>
        /// <param name="miClientId">Client ID for the Managed Identity.</param>
        /// <param name="audience">Target audience. For public clouds should be api://AzureADTokenExchange.</param>
        /// <returns>If successful, returns managed identity access token.</returns>
        protected static async Task<string> GetManagedIdentityToken(string miClientId, string audience)
        {
            var miCredential = new ManagedIdentityCredential(miClientId);
            string[] scopes = { $"{audience}/.default" };
            return (await miCredential.GetTokenAsync(new Azure.Core.TokenRequestContext(scopes)).ConfigureAwait(false)).Token;
        }

        /// <summary>
        /// Creates an SAS Token to connect to the Clawback v2 event service.
        /// </summary>
        /// <returns></returns>
        public virtual Task<SASToken> GetClawbackV2SASTokenAsync()
        {
            return CreateSASTokenAsync(SASTokenType.ClawbackV2, "");    // We do not have a cached PurchaseAccessToken to pass
                                                                        // that will be implemented with the StoreServicesCachedTokenProvider class.
        }

        /// <summary>
        /// Generates an SAS Token (URI) based on the audience (generation URI) passed in
        /// and using the appropriate Access Tokens from the configured Entra ID Credentials.
        /// </summary>
        /// <param name="SASTarget">URI defining the SAS Token generation endpoint</param>
        /// <param name="accessToken">accessToken to use and will represent the Entra Client App Identity</param>
        /// <returns>SASToken, otherwise Exception will be thrown</returns>
        protected virtual async Task<SASToken> CreateSASTokenAsync(string tokenType, string accessToken)
        {
            //  Validate we have the needed values
            if (string.IsNullOrEmpty(tokenType))
            {
                throw new ArgumentException($"{nameof(tokenType)} required", nameof(tokenType));
            }

            //  If an existing accessToken was not provide, we need to generate one based on 
            //  the SASTarget
            if (string.IsNullOrEmpty(accessToken))
            {
                if (tokenType == SASTokenType.ClawbackV2)
                {
                    var purchaseAccessToken = await GetServiceAccessTokenAsync();
                    accessToken = purchaseAccessToken.Token;

                    if(string.IsNullOrEmpty(accessToken))
                    {
                        throw new StoreServicesException($"Unable to generate a new PurchaseAccessToken");
                    }
                }
                else
                {
                    throw new ArgumentException($"Unknown SASTokenType of {tokenType} ", nameof(tokenType));
                }
            }

            var requestUri = tokenType;  // For the SAS tokens the Type is the URI to generate the token
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, requestUri.ToString());

            httpRequest.Headers.Add("Authorization", $"Bearer {accessToken}");

            // Post the request and wait for the response
            var httpClient = CreateHttpClientFunc();
            using (var httpResponse = await httpClient.SendAsync(httpRequest))
            {
                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.IsSuccessStatusCode)
                {
                    var token = JsonConvert.DeserializeObject<SASToken>(responseBody);
                    return token;
                }
                else
                {
                    throw new StoreServicesHttpResponseException($"Unable to acquire SAS token from {tokenType} : {httpResponse.ReasonPhrase}", httpResponse);
                }
            }
        }
    }
}

