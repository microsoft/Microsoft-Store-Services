//-----------------------------------------------------------------------------
// CollectionsItemBase.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Microsoft.StoreServices.Collections
{
    /// <summary>
    /// CollectionsItem contract values shared with both V8 and V9
    /// </summary>
    public class CollectionsItemBase
    {
        /// <summary>
        /// The date on which the user acquired the item.
        /// </summary>
        [JsonProperty("acquiredDate")] public DateTimeOffset AcquiredDate { get; set; }

        /// <summary>
        /// Indicates how the user has this entitlement.
        /// </summary>
        [JsonProperty("acquisitionType")] public string AcquisitionType { get; set; }

        /// <summary>
        /// The UTC date that the item will expire.
        /// </summary>
        [JsonProperty("endDate")] public DateTimeOffset EndDate { get; set; }

        /// <summary>
        /// An ID that identifies this collection item from other items that the user owns. This ID is unique per product.
        /// </summary>
        [JsonProperty("id")] public string Id { get; set; }

        /// <summary>
        /// The UTC date that this item was last modified. With consumable products, this value changes when the user’s quantity
        /// balance changes through an additional purchase of the consumable product or when a consume request is issued.
        /// </summary>
        [JsonProperty("modifiedDate")] public DateTimeOffset ModifiedDate { get; set; }

        /// <summary>
        /// Also refereed to as the Store ID for the product within the Microsoft Store catalog. An example Store ID for
        /// a product is 9NBLGGH42CFD.
        /// </summary>
        [JsonProperty("productId")] public string ProductId { get; set; }

        /// <summary>
        /// Indicates the product type. For more information, see ProductKindTypes.
        /// </summary>
        [JsonProperty("productKind")] public string ProductKind { get; set; }

        /// <summary>
        /// The quantity of the item. For non-consumable products, this is always 1. For consumable products, this represents the remaining balance that can be consumed or fulfilled for the user.
        /// </summary>
        [JsonProperty("quantity")] public int Quantity { get; set; }

        /// <summary>
        /// If this product is entitled because of a bundle or subscription, the ProductIds of those parent products are provided here.
        /// </summary>
        [JsonProperty("satisfiedByProductIds")] public List<object> SatisfiedByProductIds { get; set; }

        /// <summary>
        /// The specific SKU identifier if there are multiple offerings of the product in the Microsoft Store catalog. An example Store ID for a SKU is 0010.
        /// </summary>
        [JsonProperty("skuId")] public string SkuId { get; set; }

        /// <summary>
        /// The UTC date that the item became or will become valid.
        /// </summary>
        [JsonProperty("startDate")] public DateTimeOffset StartDate { get; set; }

        /// <summary>
        /// The status of the item.
        /// </summary>
        [JsonProperty("status")] public string Status { get; set; }

        /// <summary>
        /// Tags related to the product.
        /// </summary>
        [JsonProperty("tags")] public List<object> Tags { get; set; }

        /// <summary>
        /// The transaction ID as a result of the purchase of this item. Can be used for reporting an item as fulfilled.
        /// </summary>
        [JsonProperty("transactionId")] public string TransactionId { get; set; }

        /// <summary>
        /// Information about this product—if it’s a trial and the time remaining.
        /// </summary>
        [JsonProperty("trialData")] public TrialData TrialData { get; set; }
    }

    /// <summary>
    /// Trial information related to the users entitlement to the product.
    /// </summary>
    public class TrialData
    {
        /// <summary>
        /// Indicates if this product is licensed through a trial.
        /// </summary>
        [JsonProperty("isInTrialPeriod")] public bool IsInTrialPeriod { get; set; }

        /// <summary>
        /// Indicates if the product is in a trial period, such as a subscription.
        /// </summary>
        [JsonProperty("isTrial")] public bool IsTrial { get; set; }

        /// <summary>
        /// Information about how long the trial remains valid.
        /// </summary>
        [JsonProperty("trialTimeRemaining")] public TimeSpan TrialTimeRemaining { get; set; }
    }

    /// <summary>
    /// The AcquisitionType values that are returned in calls to the Microsoft Store 
    /// Service and indicate how the product is entitled to the user.
    /// </summary>
    public class AcquisitionTypes
    {
        /// <summary>
        /// Owned or entitled through a subscription.
        /// </summary>
        public static string Recurring = "Recurring";

        /// <summary>
        /// Direct digital purchase or code redemption.
        /// </summary>
        public static string Single = "Single";

        /// <summary>
        /// Owned but requires other products to continue use.
        /// Ex: Games With Gold obtained games which expire if Gold subscription ends
        /// </summary>
        public static string Conditional = "Conditional";
    }

    /// <summary>
    /// Values returned with a Collections item as the Status value
    /// </summary>
    public class ProductStatusTypes
    {
        /// <summary>
        /// The product is actively entitled. The user should have access to it.
        /// </summary>
        public static string Active = "Active";

        /// <summary>
        /// Most commonly indicates that the user requested a refund.
        /// </summary>
        public static string Revoked = "Revoked";

        /// <summary>
        /// The product was part of an entitlement (usually a subscription) that has since expired.
        /// </summary>
        public static string Expired = "Expired";

        /// <summary>
        /// No information on this type
        /// </summary>
        public static string Banned = "Banned";

        /// <summary>
        /// No information on this type
        /// </summary>
        public static string Suspended = "Suspended";
    }

    /// <summary>
    /// String values that can be returned as the ProductKind in the Collections result.
    /// </summary>
    public class ProductKindTypes
    {
        /// <summary>
        /// Game application or a bundle that includes a game application
        /// </summary>
        public const string Game = "Game";

        /// <summary>
        /// Apps that are not games.
        /// </summary>
        public const string Application = "Application";

        /// <summary>
        /// Store-managed Subscription such as Xbox Game Pass.
        /// </summary>
        public const string Pass = "Pass";

        /// <summary>
        /// Game content such as DLC and most single-time purchase items.
        /// </summary>
        public const string Durable = "Durable";

        /// <summary>
        /// Store-managed consumable product (recommended consumable type for most scenarios).
        /// </summary>
        public const string Consumable = "Consumable";

        /// <summary>
        /// Developer-managed consumable that must be fulfilled before being able to be purchased by the user again.
        /// </summary>
        public const string UnmanagedConsumable = "UnmanagedConsumable";
    }
}
