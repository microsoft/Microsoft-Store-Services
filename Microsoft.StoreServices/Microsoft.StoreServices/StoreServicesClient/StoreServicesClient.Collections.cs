//-----------------------------------------------------------------------------
// StoreServicesClient.Collections.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using Microsoft.StoreServices.Collections.V8;
using Microsoft.StoreServices.Collections.V9;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Microsoft.StoreServices
{
    public sealed partial class StoreServicesClient : IStoreServicesClient
    {
        /// <summary>
        /// Query for the user's current entitlements based on the filtering options in the request agaomst v8.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CollectionsV8QueryResponse> CollectionsQueryAsync(CollectionsV8QueryRequest request)
        {
            //  Validate that we have a UserCollectionsId
            if (request.Beneficiaries == null ||
                request.Beneficiaries.Count != 1 ||
                string.IsNullOrEmpty(request.Beneficiaries[0].UserCollectionsId))
            {
                throw new ArgumentException($"{nameof(request.Beneficiaries)} must be provided", nameof(request.Beneficiaries));
            }

            //  Now pass these values to get the correct Delegated Auth and Signature headers for
            //  the request
            //  Post the request and wait for the response
            var userCollection = await IssueRequestAsync<CollectionsV8QueryResponse>(
                "https://collections.mp.microsoft.com/v8.0/collections/b2bLicensePreview",
                JsonConvert.SerializeObject(request),
                null);

            return userCollection;
        }

        /// <summary>
        /// Query for the user's current entitlements based on the filtering options in the request against v9.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CollectionsV9QueryResponse> CollectionsQueryAsync(CollectionsV9QueryRequest request)
        {

            //  Validate that we have a UserCollectionsId
            if (request.Beneficiaries == null ||
                request.Beneficiaries.Count != 1 ||
                string.IsNullOrEmpty(request.Beneficiaries[0].UserCollectionsId))
            {
                throw new ArgumentException($"{nameof(request.Beneficiaries)} must be provided", nameof(request.Beneficiaries));
            }

            //  Now pass these values to get the correct Delegated 
            //  Auth and Signature headers for the request
            //  Post the request and wait for the response
            var userCollection = await IssueRequestAsync<CollectionsV9QueryResponse>(
                "https://collections.mp.microsoft.com/v9.0/collections/PublisherQuery",
                JsonConvert.SerializeObject(request),
                null);

            return userCollection;
        }

        /// <summary>
        /// Preform a consume transaction from the user's balance of the product based on the request parameters.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<CollectionsV8ConsumeResponse> CollectionsConsumeAsync(CollectionsV8ConsumeRequest request)
        {
            //  Validate that we have a productID, quantity, trackingId and a UserCollectionsId
            {
                if (string.IsNullOrEmpty(request.ProductId))
                {
                    throw new ArgumentException($"{nameof(request.ProductId)} must be provided", nameof(request.ProductId));
                }
                if (request.RemoveQuantity <= 0)
                {
                    throw new ArgumentException($"{nameof(request.RemoveQuantity)} must be greater than 0", nameof(request.RemoveQuantity));
                }
                if (string.IsNullOrEmpty(request.TrackingId))
                {
                    throw new ArgumentException($"{nameof(request.TrackingId)} must be provided", nameof(request.TrackingId));
                }
                if (request.RequestBeneficiary == null)
                {
                    throw new ArgumentException($"{nameof(request.RequestBeneficiary)} must be provided", nameof(request.RequestBeneficiary));
                }
                if (string.IsNullOrEmpty(request.RequestBeneficiary.UserCollectionsId))
                {
                    throw new ArgumentException("request.RequestBeneficiary.UserCollectionsId must be provided", "request.RequestBeneficiary.UserCollectionsId");
                }
            }

            CollectionsV8ConsumeResponse consumeResponse;
            try
            {
                //  Post the request and wait for the response
                consumeResponse = await IssueRequestAsync<CollectionsV8ConsumeResponse>(
                    "https://collections.mp.microsoft.com/v8.0/collections/consume",
                    JsonConvert.SerializeObject(request),
                    null);
            }
            catch(StoreServicesHttpResponseException ex)
            {
                //  Consume failures have a specific body format that will give us more information so we need to 
                //  deserialize the JSON into a ConsumeError object
                CollectionsConsumeErrorResponseV8 responseError;
                try
                {
                    string responseBody = await ex.HttpResponseMessage.Content.ReadAsStringAsync();
                    responseError = JsonConvert.DeserializeObject<CollectionsConsumeErrorResponseV8>(responseBody, new JsonSerializerSettings
                    {
                        DateTimeZoneHandling = DateTimeZoneHandling.Utc
                    });
                }
                catch (Exception e)
                {
                    throw new StoreServicesClientException("Unable to parse ConsumeErrorResponse",e);
                }

                throw new StoreServicesClientConsumeException(responseError.InnerError);
            }
            return consumeResponse;
        }
    }
}
