//-----------------------------------------------------------------------------
// ClawbackV2Event.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using Microsoft.StoreServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

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
        /// Clawback Event Type
        /// </summary>
        [JsonProperty("type")] 
        public string Type { get; set; }

        /// <summary>
        /// Data related to the Clawback event including which product and order
        /// </summary>
        [JsonProperty("data")]
        public ClawbackV2Data Data { get; set; }

        /// <summary>
        /// Time that the event was logged
        /// </summary>
        [JsonProperty("time")] 
        public DateTime Time { get; set; }

        /// <summary>
        /// Contract version of the event
        /// </summary>
        [JsonProperty("specversion")] 
        public string SpecVersion { get; set; }

        /// <summary>
        /// Content type of the data structure
        /// </summary>
        [JsonProperty("dataContentType")] 
        public string DataContentType { get; set; }
    }

    /// <summary>
    /// Represents a single product that was included in the purchase transaction made by the user.
    /// </summary>
    public class ClawbackV2Data
    {
        /// <summary>
        /// ProductId (StoreId) of the item offered within the store.
        /// </summary>
        [JsonProperty("productId")]
        public string ProductId { get; set; }
        
        /// <summary>
        /// SkuId representing the specific sub-offering of the product that was purchased.
        /// </summary>
        [JsonProperty("skuId")]
        public string SkuId { get; set; }

        /// <summary>
        /// OrderId represents a unique identifier for the purchase transaction this information is tied to.
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// Unique Id representing this product within the purchase transaction.
        /// </summary>   
        [JsonProperty("lineItemId")]
        public string LineItemId { get; set; }

        /// <summary>
        /// Current state of the item indicating if it is active, revoked, or refunded.
        /// </summary>
        [JsonProperty("refundState")]
        public string RefundState { get; set; }

        /// <summary>
        /// UTC date time when the purchase transaction was made.
        /// </summary>
        [JsonProperty("orderPurchasedDate")]
        public DateTimeOffset OrderPurchaseDate { get; set; }

        /// <summary>
        /// UTC date time when the purchase was refunded (if in a refunded state).
        /// </summary>
        [JsonProperty("orderRefundedDate")]
        public DateTimeOffset OrderRefundedDate { get; set; }

        /// <summary>
        /// Quantity granted with the purchase. Usually 1 but can be more if this is a consumable.
        /// </summary>
        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}


