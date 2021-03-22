﻿//-----------------------------------------------------------------------------
// ClawbackItem.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Microsoft.StoreServices
{
    public class ClawbackItem
    {
        //  Generally this would be the Key if you are using this in a DB
        [JsonProperty("orderId")] public string OrderId { get; set; }
        [JsonProperty("orderLineItems")] public List<OrderLineItem> OrderLineItems { get; set; }
        [JsonProperty("orderPurchasedDate")] public DateTime OrderPurchaseDate { get; set;}
        [JsonProperty("orderRefundedDate")] public DateTime OrderRefundedDate { get; set;}

    }
    
    public class OrderLineItem
    {
        //  Generally this would be the Key if you are using this in a DB
        [JsonProperty("lineItemId")] public string LineItemId { get; set; }
        [JsonProperty("lineItemState")] public string LineItemState { get; set; }
        [JsonProperty("productId")] public string ProductId { get; set; }
        [JsonProperty("quantity")] public int Quantity { get; set; }
        [JsonProperty("skuId")] public string SkuId { get; set; }
    }
}
