//-----------------------------------------------------------------------------
// RecurrenceItem.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;

namespace Microsoft.StoreServices.Purchase
{
    public class RecurrenceItem
    {
        [JsonProperty("autoRenew")] public bool AutoRenew { get; set; }
        [JsonProperty("beneficiary")] public string Beneficiary { get; set; }
        [JsonProperty("expirationTime")] public DateTime ExpirationTime { get; set; }
        [JsonProperty("expirationTimeWithGrace")] public DateTime ExpirationTimeWithGrace { get; set; }
        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("isTrial")] public bool IsTrial { get; set; }
        [JsonProperty("lastModified")] public DateTime LastModified { get; set; }
        [JsonProperty("market")] public string Market { get; set; }
        [JsonProperty("productId")] public string ProductId { get; set; }
        [JsonProperty("recurrenceState")] public string RecurrenceState { get; set; }
        [JsonProperty("skuId")] public string SkuId { get; set; }
        [JsonProperty("startTime")] public DateTime StartTime { get; set; }
        [JsonProperty("cancellationDate")] public DateTime CancellationDate { get; set; }
    }
}
