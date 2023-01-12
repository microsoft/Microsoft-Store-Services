//-----------------------------------------------------------------------------
// StoreServicesClient.Clawback.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Microsoft.StoreServices.Clawback.V1;

namespace Microsoft.StoreServices
{
    public sealed partial class StoreServicesClient : IStoreServicesClient
    {
        /// <summary>
        /// Query for the user's refunded products from the Clawback service based on the 
        /// parameters of the request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ClawbackV1QueryResponse> ClawbackV1QueryAsync(ClawbackV1QueryRequest queryParameters)
        {
            if (string.IsNullOrEmpty(queryParameters.UserPurchaseId))
            {
                throw new ArgumentException($"{nameof(queryParameters.UserPurchaseId)} must be provided", nameof(queryParameters.UserPurchaseId));
            }

            //  Post the request and wait for the response
            var userClawback = await IssueRequestAsync<ClawbackV1QueryResponse>(
                "https://purchase.mp.microsoft.com/v8.0/b2b/orders/query",
                JsonConvert.SerializeObject(queryParameters),
                null);

            return userClawback;
        }
    }
}
