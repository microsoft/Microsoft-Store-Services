//-----------------------------------------------------------------------------
// ClawbackV2Event.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;

namespace Microsoft.StoreServices.Clawback.V2
{
    /// <summary>
    /// Item representing a result from the purchase/query service (aka Clawback).  Depending on the 
    /// OrderLineItems[].LineItemState values the item may be a refunded or revoked item from the 
    /// user's owned items.
    /// </summary>
    public class ClawbackV2Event
    {
        /// <summary>
        /// Unique ID for the Clawback event
        /// </summary>
        [JsonProperty("id")] 
        public string Id { get; set; }

        /// <summary>
        /// Identifies the source of the refund / clawback event came from
        /// </summary>
        [JsonProperty("source")] 
        public string Source { get; set; }

        /// <summary>
        /// Clawback ClawbackEvent Type
        /// </summary>
        [JsonProperty("type")] 
        public string Type { get; set; }

        /// <summary>
        /// OrderInfo related to the Clawback event including which product and order
        /// </summary>
        [JsonProperty("data")]
        public ClawbackV2OrderInfo OrderInfo { get; set; }

        /// <summary>
        /// Time that the event was logged
        /// </summary>
        [JsonProperty("time")] 
        public DateTimeOffset Time { get; set; }
        
        /// <summary>
        /// Content type of the data structure
        /// </summary>
        [JsonProperty("subject")]
        public string Subject { get; set; }
    }
}


