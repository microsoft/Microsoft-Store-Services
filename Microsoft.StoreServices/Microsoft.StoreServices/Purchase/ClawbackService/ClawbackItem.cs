//-----------------------------------------------------------------------------
// ClawbackItem.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.StoreServices.Purchase
{
    public class ClawbackItem
    {
        [Key]
        [JsonProperty("orderId")] public string OrderId { get; set; }
        [JsonProperty("orderLineItems")] public List<OrderLineItem> OrderLineItems { get; set; }
        [JsonProperty("orderPurchasedDate")] public DateTime OrderPurchaseDate { get; set;}
        [JsonProperty("orderRefundedDate")] public DateTime OrderRefundedDate { get; set;}

    }
    
    public class OrderLineItem
    {
        [Key]
        [JsonProperty("lineItemId")] public string LineItemId { get; set; }
        [JsonProperty("lineItemState")] public string LineItemState { get; set; }
        [JsonProperty("productId")] public string ProductId { get; set; }
        [JsonProperty("quantity")] public int Quantity { get; set; }
        [JsonProperty("skuId")] public string SkuId { get; set; }
    }
}
