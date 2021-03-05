//-----------------------------------------------------------------------------
// ClawbackService.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.StoreServices.Purchase
{
    /// <summary>
    /// Clawback is part of the Purchase Services that allows the caller to
    /// check a user's account for refunded and revoked items that may have 
    /// already been used or consumed on the game service side.
    /// </summary>
    public class ClawbackService : StoreServicesCallerBase
    {
        /// <summary>
        /// Query's the Clawback information for the user based on the parameters object
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <param name="serviceAccessToken"></param>
        /// <param name="serviceIdentity"></param>
        /// <param name="httpCaller"></param>
        /// <returns>Array of ClawbackItems that match the criteria for the user</returns>
        public async Task<List<ClawbackItem>> QueryAsync( ClawbackRequest queryParameters,
                                                          string serviceAccessToken,
                                                          string serviceIdentity,
                                                          HttpClient httpCaller)
        {
            if (string.IsNullOrEmpty(queryParameters.Beneficiary))
            {
                throw new ArgumentException($"{nameof(queryParameters.Beneficiary)} must be provided", nameof(queryParameters.Beneficiary));
            }

            //  Build the Request's URI, headers, and body first
            var uri = new Uri("https://purchase.mp.microsoft.com/v8.0/b2b/orders/query");

            // Serialize our request body to a UTF8 byte array
            string requestBodyString = JsonConvert.SerializeObject(queryParameters);
            byte[] requestBodyContent = System.Text.Encoding.UTF8.GetBytes(requestBodyString);

            //  Post the request and wait for the response
            var userClawback = await IssueRequestAsync<ClawbackResponse>(uri,
                                                                         requestBodyContent,
                                                                         serviceAccessToken,
                                                                         serviceIdentity,
                                                                         httpCaller,
                                                                         null);

            return userClawback.Items;
        }
    }
}
