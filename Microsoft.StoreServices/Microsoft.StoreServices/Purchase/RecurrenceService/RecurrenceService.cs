//-----------------------------------------------------------------------------
// RecurrenceService.cs
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
    /// Recurrences is part of the Purchase Services that allows the caller to
    /// check a user's subscription status and manage the subscription as well
    /// </summary>
    public class RecurrenceService : StoreServicesCallerBase
    {
        /// <summary>
        /// Query's the Recurrence information for the user based on the parameters object
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <param name="serviceAccessToken"></param>
        /// <param name="serviceIdentity"></param>
        /// <param name="httpCaller"></param>
        /// <returns>Array of ClawbackItems that match the criteria for the user</returns>
        public async Task<List<RecurrenceItem>> QueryAsync(RecurrenceQueryRequest queryParameters,
                                                           string serviceAccessToken,
                                                           string serviceIdentity,
                                                           HttpClient httpCaller)
        {
            if (string.IsNullOrEmpty(queryParameters.Beneficiary))
            {
                throw new ArgumentException($"{nameof(queryParameters.Beneficiary)} must be provided", nameof(queryParameters.Beneficiary));
            }

            //  Build the Request's URI, headers, and body first
            var uri = new Uri("https://purchase.mp.microsoft.com/v8.0/b2b/recurrences/query");

            // Serialize our request body to a UTF8 byte array
            string requestBodyString = JsonConvert.SerializeObject(queryParameters);
            byte[] requestBodyContent = System.Text.Encoding.UTF8.GetBytes(requestBodyString);

            //  Post the request and wait for the response
            var userRecurrences = await IssueRequestAsync<RecurrenceQueryResponse>(uri,
                                                                         requestBodyContent,
                                                                         serviceAccessToken,
                                                                         serviceIdentity,
                                                                         httpCaller,
                                                                         null);

            return userRecurrences.Items;
        }

        /// <summary>
        /// Allows the caller to change the billing state of an existing user's
        /// subscription.  Extend its date, cancel, enable/disable auto-renew, or
        /// refund the subscription.
        /// </summary>
        /// <param name="queryParameters"></param>
        /// <param name="serviceAccessToken"></param>
        /// <param name="serviceIdentity"></param>
        /// <param name="httpCaller"></param>
        /// <returns></returns>
        public async Task<RecurrenceItem> ChangeAsync(RecurrenceChangeRequest queryParameters,
                                                            string recurrenceId,
                                                            string serviceAccessToken,
                                                            string serviceIdentity,
                                                            HttpClient httpCaller)
        {
            if (string.IsNullOrEmpty(queryParameters.Beneficiary))
            {
                throw new ArgumentException($"{nameof(queryParameters.Beneficiary)} must be provided", nameof(queryParameters.Beneficiary));
            }

            //  Build the Request's URI, headers, and body first
            var uri = new Uri($"https://purchase.mp.microsoft.com/v8.0/b2b/recurrences/{recurrenceId}/change");

            // Serialize our request body to a UTF8 byte array
            string requestBodyString = JsonConvert.SerializeObject(queryParameters);
            byte[] requestBodyContent = System.Text.Encoding.UTF8.GetBytes(requestBodyString);

            //  Post the request and wait for the response
            var userRecurrence = await IssueRequestAsync<RecurrenceItem>(uri,
                                                                         requestBodyContent,
                                                                         serviceAccessToken,
                                                                         serviceIdentity,
                                                                         httpCaller,
                                                                         null);

            return userRecurrence;
        }

    }
}
