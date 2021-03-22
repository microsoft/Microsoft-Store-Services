//-----------------------------------------------------------------------------
// StoreServicesClient.Clawback.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.StoreServices
{
    public sealed partial class StoreServicesClient : IStoreServicesClient
    {
        /// <summary>
        /// This can be overwritten with an HttpClientFactory.CreateClient() for better performance
        /// </summary>
        public static Func<HttpClient> CreateHttpClientFunc = () => new HttpClient();
        public string ServiceIdentity { get; private set; }

        private readonly IAccessTokenProvider _accessTokenProvider;

        private bool _isDisposed;

        public StoreServicesClient(string serviceIdentity, IAccessTokenProvider accessTokenProvider) 
        {
            _accessTokenProvider = accessTokenProvider;
            ServiceIdentity = serviceIdentity ?? "Some reasonable default";
            _isDisposed = false;
        }

        private async Task<T> IssueRequestAsync<T>(string uri, string requestBodyString, IDictionary<string, string> additionalHeaders = null)
        {
            var client = CreateHttpClientFunc();

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(requestBodyString, Encoding.UTF8, "application/json")
            };

            //  Add the Authorization header for AAD / StoreID keys
            var serviceToken = await _accessTokenProvider.GetServiceAccessTokenAsync();
            httpRequest.Headers.Add("Authorization", $"Bearer {serviceToken.Token}");
            httpRequest.Headers.Add("User-Agent", ServiceIdentity); //  unique name to identify your service in logging

            if (additionalHeaders != null)
            {
                //  Add the rest of the headers the caller wants 
                foreach (var header in additionalHeaders)
                {
                    httpRequest.Headers.Add(header.Key, header.Value);
                }
            }

            try
            {
                //  issue the request to the service
                var httpResponse = await client.SendAsync(httpRequest);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    throw new StoreServicesHttpResponseException($"Http request failed with status code {httpResponse.StatusCode}.", httpResponse);
                }

                string responseBody = await httpResponse.Content.ReadAsStringAsync();

                //  De-serialize the JSON response and pass it back.  All responses from the 
                //  Microsoft Store Services use UTC time so we make sure to specify that
                return JsonConvert.DeserializeObject<T>(responseBody, new JsonSerializerSettings
                {
                    DateTimeZoneHandling = DateTimeZoneHandling.Utc
                });
            }
            catch (HttpRequestException httpReqEx)
            {
                throw new StoreServicesClientException($"Http request for type {typeof(T)} failed.", httpReqEx);
            }
        }

        public Task<AccessToken> GetServiceAccessTokenAsync()
        {
            return _accessTokenProvider.GetServiceAccessTokenAsync();
        }

        public Task<AccessToken> GetCollectionsAccessTokenAsync()
        {
            return _accessTokenProvider.GetCollectionsAccessTokenAsync();
        }

        public Task<AccessToken> GetPurchaseAccessTokenAsync()
        {
            return _accessTokenProvider.GetPurchaseAccessTokenAsync();
        }

        public void Dispose()
        {
            if (_isDisposed) return;
            _isDisposed = true;
            GC.SuppressFinalize(this);
        }
    }
}