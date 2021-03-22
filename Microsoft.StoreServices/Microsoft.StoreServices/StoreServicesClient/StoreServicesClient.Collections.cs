//-----------------------------------------------------------------------------
// StoreServicesClient.Collections.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Microsoft.StoreServices
{
    public sealed partial class StoreServicesClient : IStoreServicesClient
    {
        public async Task<CollectionsQueryResponse> CollectionsQueryAsync(CollectionsQueryRequest request)
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
            var userCollection = await IssueRequestAsync<CollectionsQueryResponse>(
                "https://collections.mp.microsoft.com/v8.0/collections/b2bLicensePreview",
                JsonConvert.SerializeObject(request),
                null);

            return userCollection;
        }

        public async Task<CollectionsConsumeResponse> CollectionsConsumeAsync(CollectionsConsumeRequest request)
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
                if (request.Beneficiary == null ||
                    string.IsNullOrEmpty(request.Beneficiary.UserCollectionsId))
                {
                    throw new ArgumentException($"{nameof(request.Beneficiary.UserCollectionsId)} must be provided", nameof(request.Beneficiary.UserCollectionsId));
                }
            }

            //  Post the request and wait for the response
            var consumeResponse = await IssueRequestAsync<CollectionsConsumeResponse>(
                "https://collections.mp.microsoft.com/v8.0/collections/consume",
                JsonConvert.SerializeObject(request),
                null);
            
            return consumeResponse;           
        }
    }
}
