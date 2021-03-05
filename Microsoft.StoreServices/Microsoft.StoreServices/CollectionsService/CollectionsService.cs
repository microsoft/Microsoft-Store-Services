//-----------------------------------------------------------------------------
// CollectionsService.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.StoreServices
{
    public class CollectionsService : StoreServicesCallerBase
    {
        /// <summary>
        /// Query the user's collections information and returns any items that meet the 
        /// query parameters.  These items are directly owned by the user who's 
        /// UserCollectionsId we are passing with the QueryParameters.
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <param name="serviceAccessToken"></param>
        /// <param name="serviceIdentity"></param>
        /// <param name="httpCaller"></param>
        /// <returns></returns>
        public async Task<List<CollectionsItem>> QueryAsync(CollectionsQueryRequest queryParameters,
                                                            string serviceAccessToken,
                                                            string serviceIdentity,
                                                            HttpClient httpCaller)
        {

            //  Validate that we have a UserCollectionsId
            if (queryParameters.Beneficiaries == null ||
                queryParameters.Beneficiaries.Count != 1 ||
                string.IsNullOrEmpty(queryParameters.Beneficiaries[0].UserCollectionsId))
            {
                throw new ArgumentException($"{nameof(queryParameters.Beneficiaries)} must be provided", nameof(queryParameters.Beneficiaries));
            }

            //  Build the Request's URI, headers, and body first
            var uri = new Uri("https://collections.mp.microsoft.com/v8.0/collections/b2bLicensePreview");
           
            // Serialize our request body to a UTF8 byte array
            string requestBodyString = JsonConvert.SerializeObject(queryParameters);
            byte[] requestBodyContent = System.Text.Encoding.UTF8.GetBytes(requestBodyString);

            //  Now pass these values to get the correct Delegated Auth and Signature headers for
            //  the request
            //  Post the request and wait for the response
            var userCollection = await IssueRequestAsync<CollectionsQueryResponse>(uri,
                                                                                   requestBodyContent,
                                                                                   serviceAccessToken,
                                                                                   serviceIdentity,
                                                                                   httpCaller,
                                                                                   null);

            return userCollection.Items;
        }

        /// <summary>
        /// Consumes a specified quantity of a product from a user's active balance in the store.
        /// </summary>
        /// <param name="consumeParameters"></param>
        /// <param name="serviceAccessToken"></param>
        /// <param name="serviceIdentity"></param>
        /// <param name="httpCaller"></param>
        /// <returns></returns>
        public async Task<CollectionsConsumeResponse> Consume(CollectionsConsumeRequest consumeParameters,
                                                              string serviceAccessToken,
                                                              string serviceIdentity,
                                                              HttpClient httpCaller)
        {
            //  Validate that we have a productID, quantity, trackingId and a UserCollectionsId
            {
                if (string.IsNullOrEmpty(consumeParameters.ProductId))
                {
                    throw new ArgumentException($"{nameof(consumeParameters.ProductId)} must be provided", nameof(consumeParameters.ProductId));
                }
                if (consumeParameters.RemoveQuantity <= 0)
                {
                    throw new ArgumentException($"{nameof(consumeParameters.RemoveQuantity)} must be greater than 0", nameof(consumeParameters.RemoveQuantity));
                }
                if (string.IsNullOrEmpty(consumeParameters.TrackingId))
                {
                    throw new ArgumentException($"{nameof(consumeParameters.TrackingId)} must be provided", nameof(consumeParameters.TrackingId));
                }
                if (consumeParameters.Beneficiary == null ||
                    string.IsNullOrEmpty(consumeParameters.Beneficiary.UserCollectionsId))
                {
                    throw new ArgumentException($"{nameof(consumeParameters.Beneficiary.UserCollectionsId)} must be provided", nameof(consumeParameters.Beneficiary.UserCollectionsId));
                }

            }

            //  Build the Request's URI, headers, and body first
            var uri = new Uri("https://collections.mp.microsoft.com/v8.0/collections/consume");

            // Serialize our request body to a UTF8 byte array
            string requestBodyString = JsonConvert.SerializeObject(consumeParameters);
            byte[] requestBodyContent = System.Text.Encoding.UTF8.GetBytes(requestBodyString);

            //  Post the request and wait for the response
            var consumeResponse = await IssueRequestAsync<CollectionsConsumeResponse>(uri,
                                                                                      requestBodyContent,
                                                                                      serviceAccessToken,
                                                                                      serviceIdentity,
                                                                                      httpCaller,
                                                                                      null); 
            return consumeResponse;           
        }
    }
}
