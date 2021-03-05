//-----------------------------------------------------------------------------
// StoreServicesCallerBase.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.StoreServices
{
    public class StoreServicesCallerBase : IDisposable
    {
        public StoreServicesCallerBase() {  }

        // To detect redundant calls
        private bool _disposed = false;

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose() => Dispose(true);

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
        }

        /// <summary>
        /// Calls the StoreService, gets the reply and converts the response
        /// to the appropriate response class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="targetUri"></param>
        /// <param name="requestContent"></param>
        /// <param name="serviceAccessToken"></param>
        /// <param name="serviceIdentity"></param>
        /// <param name="httpCaller"></param>
        /// <param name="additionalHeaders"></param>
        /// <returns></returns>
        protected async Task<T> IssueRequestAsync<T>(Uri targetUri,
                                                     byte[] requestContent,
                                                     string serviceAccessToken,
                                                     string serviceIdentity,
                                                     HttpClient httpCaller,
                                                     NameValueCollection additionalHeaders)
        {
            //  Validate the needed parameters
            {
                if (targetUri == null || string.IsNullOrEmpty(targetUri.ToString()))
                {
                    throw new ArgumentException($"{nameof(targetUri)} must be valid", nameof(targetUri));
                }
                if (requestContent == null)
                {
                    throw new ArgumentException($"{nameof(requestContent)} cannot be null", nameof(requestContent));
                }
                if (serviceAccessToken == null || string.IsNullOrEmpty(serviceAccessToken.ToString()))
                {
                    throw new ArgumentException($"{nameof(serviceAccessToken)} cannot be empty", nameof(serviceAccessToken));
                }
                if (serviceIdentity == null || string.IsNullOrEmpty(serviceIdentity.ToString()))
                {
                    throw new ArgumentException($"{nameof(serviceIdentity)} cannot be empty", nameof(serviceIdentity));
                }
                if (httpCaller == null)
                {
                    throw new ArgumentException($"{nameof(httpCaller)} cannot be null", nameof(httpCaller));
                }
            }

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, targetUri)
            {
                Content = new ByteArrayContent(requestContent)
            };

            httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //  Add the Authorization header for AAD / StoreID keys
            var authHeader = new StringBuilder("");
            authHeader.AppendFormat("Bearer {0}", serviceAccessToken);
            httpRequest.Headers.Add("Authorization", authHeader.ToString());
            httpRequest.Headers.Add("User-Agent", serviceIdentity); //  unique name to identify your service in logging

            if (additionalHeaders != null)
            {
                //  Add the rest of the headers the caller wants 
                foreach (string key in additionalHeaders.AllKeys)
                {
                    httpRequest.Headers.Add(key, additionalHeaders[key]);
                }
            }

            //  issue the request to the service
            var httpResponse = await httpCaller.SendAsync(httpRequest);
            string responseBody = await httpResponse.Content.ReadAsStringAsync();

            //  De-serialize the JSON response and pass it back.  All responses from the 
            //  Microsoft Store Services use UTC time so we make sure to specify that
            return JsonConvert.DeserializeObject<T>(responseBody, new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            }); ;
        }
    }
}
