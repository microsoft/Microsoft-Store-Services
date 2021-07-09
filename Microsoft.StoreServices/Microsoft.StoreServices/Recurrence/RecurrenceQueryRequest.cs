//-----------------------------------------------------------------------------
// RecurrenceQueryRequest.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
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
    }
}
