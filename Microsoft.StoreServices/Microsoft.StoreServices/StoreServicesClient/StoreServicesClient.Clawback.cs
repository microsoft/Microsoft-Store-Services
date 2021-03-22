//-----------------------------------------------------------------------------
// StoreServicesClient.Clawback.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Microsoft.StoreServices
{
    /// <summary>
    /// Clawback is part of the Purchase Services that allows the caller to
    /// check a user's account for refunded and revoked items that may have 
    /// already been used or consumed on the game service side.
    /// </summary>
    public sealed partial class StoreServicesClient : IStoreServicesClient
    {
        public async Task<ClawbackQueryResponse> ClawbackQueryAsync(ClawbackQueryRequest queryParameters)
        {
            if (string.IsNullOrEmpty(queryParameters.Beneficiary))
            {
                throw new ArgumentException($"{nameof(queryParameters.Beneficiary)} must be provided", nameof(queryParameters.Beneficiary));
            }

            //  Post the request and wait for the response
            var userClawback = await IssueRequestAsync<ClawbackQueryResponse>(
                "https://purchase.mp.microsoft.com/v8.0/b2b/orders/query",
                JsonConvert.SerializeObject(queryParameters),
                null);

            return userClawback;
        }
    }
}
