//-----------------------------------------------------------------------------
// RecurrenceQueryRequest.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Microsoft.StoreServices
{
    /// <summary>
    /// JSON request body for a query to the Recurrence service
    /// </summary>
    public class RecurrenceQueryRequest
    {
        /// <summary>
        /// UserPurchaseId that identifies the user we are asking about
        /// </summary>
        [JsonProperty("b2bKey")] public string UserPurchaseId { get; set; }

        /// <summary>
        /// Defines the development SandboxId that results should be scoped to
        /// otherwise leave blank to default to RETAIL
        /// </summary>
        [JsonProperty("sbx")] public string SandboxId { get; set; }
    }
}
