//-----------------------------------------------------------------------------
// StoreServicesClient.Recurrence.cs
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
    /// Recurrences is part of the Purchase Services that allows the caller to
    /// check a user's subscription status and manage the subscription as well
    /// </summary>
    public sealed partial class StoreServicesClient : IStoreServicesClient
    {
        public async Task<RecurrenceQueryResponse> RecurrenceQueryAsync(RecurrenceQueryRequest request)
        {
            if (string.IsNullOrEmpty(request.Beneficiary))
            {
                throw new ArgumentException($"{nameof(request.Beneficiary)} must be provided", nameof(request.Beneficiary));
            }

            //  Post the request and wait for the response
            var userRecurrences = await IssueRequestAsync<RecurrenceQueryResponse>(
                "https://purchase.mp.microsoft.com/v8.0/b2b/recurrences/query",
                JsonConvert.SerializeObject(request),
                null);

            return userRecurrences;
        }

        public async Task<RecurrenceChangeResponse> RecurrenceChangeAysnc(RecurrenceChangeRequest request)
        {
            if (string.IsNullOrEmpty(request.Beneficiary))
            {
                throw new ArgumentException($"{nameof(request.Beneficiary)} must be provided", nameof(request.Beneficiary));
            }

            //  Post the request and wait for the response
            var userRecurrence = await IssueRequestAsync<RecurrenceChangeResponse>(
                $"https://purchase.mp.microsoft.com/v8.0/b2b/recurrences/{request.RecurrenceId}/change",
                JsonConvert.SerializeObject(request),
                null);

            return userRecurrence;
        }

    }
}
