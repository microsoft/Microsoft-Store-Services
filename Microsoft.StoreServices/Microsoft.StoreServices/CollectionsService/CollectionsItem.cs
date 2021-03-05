//-----------------------------------------------------------------------------
// CollectionsItem.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Microsoft.StoreServices
{
    public class CollectionsItem
    {
        [JsonProperty("acquiredDate")] public DateTime AcquiredDate { get; set; }
        [JsonProperty("acquisitionType")] public string AcquisitionType { get; set; }
        [JsonProperty("endDate")] public DateTime EndDate { get; set; }
        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("legacyOfferInstanceId")] public string LegacyOfferInstanceId { get; set; }
        [JsonProperty("legacyProductId")] public string LegacyProductId { get; set; }
        [JsonProperty("localTicketReference")] public string LocalTicketReference { get; set; }
        [JsonProperty("modifiedDate")] public DateTime ModifiedDate { get; set; }
        [JsonProperty("purchasedCountry")] public string PurchasedCountry { get; set; }
        [JsonProperty("productFamily")] public string ProductFamily { get; set; }
        [JsonProperty("productId")] public string ProductId { get; set; }
        [JsonProperty("productKind")] public string ProductKind { get; set; }
        [JsonProperty("recurrenceData")] public RecurrenceData RecurrenceData { get; set; }
        [JsonProperty("satisfiedByProductIds")] public List<object> SatisfiedByProductIds { get; set; }
        [JsonProperty("sharingSource")] public string SharingSource { get; set; }
        [JsonProperty("skuId")] public string SkuId { get; set; }
        [JsonProperty("startDate")] public DateTime StartDate { get; set; }
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("tags")] public List<object> Tags { get; set; }
        [JsonProperty("trialData")] public TrialData TrialData { get; set; }
        [JsonProperty("devOfferId")] public string DevOfferId { get; set; }
        [JsonProperty("quantity")] public int Quantity { get; set; }
        [JsonProperty("transactionId")] public string TransactionId { get; set; }
    }

    public class RecurrenceData
    {
        [JsonProperty("recurrenceId")] public string RecurrenceId { get; set; }
    }

    public class TrialData
    {
        [JsonProperty("isInTrialPeriod")] public bool IsInTrialPeriod { get; set; }
        [JsonProperty("isTrial")] public bool IsTrial { get; set; }
        [JsonProperty("trialTimeRemaining")] public string TrialTimeRemaining { get; set; }
    }

}
