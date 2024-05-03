//-----------------------------------------------------------------------------
// ClawbackV2OrderInfo.cs
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
    /// Represents a single product that was included in the purchase transaction made by the user.
    /// </summary>
    public class ClawbackV2OrderInfo
    {
        /// <summary>
        /// Unique Id representing this product within the purchase transaction.
        /// </summary>   
        [JsonProperty("lineItemId")]
        public string LineItemId { get; set; }

        /// <summary>
        /// OrderId represents a unique identifier for the purchase transaction this information is tied to.
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// ProductId (StoreId) of the item offered within the store.
        /// </summary>
        [JsonProperty("productId")]
        public string ProductId { get; set; }

        /// <summary>
        /// Product type the event relates to, Consumable or UnmanagedConsumable
        /// </summary>
        [JsonProperty("productType")]
        public string ProductType { get; set; }

        /// <summary>
        /// UTC date time when the purchase transaction was made.
        /// </summary>
        [JsonProperty("purchasedDate")]
        public DateTimeOffset PurchasedDate { get; set; }

        /// <summary>
        /// UTC date time when the purchase was refunded (if in a refunded state).
        /// </summary>
        [JsonProperty("eventDate")]
        public DateTimeOffset? EventDate { get; set; }

        /// <summary>
        /// Current state of the item indicating if it is active, revoked, or refunded.
        /// </summary>
        [JsonProperty("eventState")]
        public string EventState { get; set; }

        /// <summary>
        /// UTC date time when the purchase transaction was made.
        /// </summary>
        [JsonProperty("sandboxId")]
        public string SandboxId { get; set; }

        /// <summary>
        /// SkuId representing the specific sub-offering of the product that was purchased.
        /// </summary>
        [JsonProperty("skuId")]
        public string SkuId { get; set; }

        /// <summary>
        /// Specific information related to a subscription product that has triggered a Clawback event
        /// </summary>
        [JsonProperty("subscriptionData")]
        public ClawbackSubscriptionData SubscriptionData { get; set; }
    }
}

/// <summary>
/// Values supported for the EventState of the order
/// </summary>
public static class EventStates
{
    /// <summary>
    /// Refunded item was able to be removed from the user's entitlement. 
    /// No action should be required on the partner service.
    /// </summary>
    public const string Refunded = "Refunded";

    /// <summary>
    /// Refunded item was already fulfilled / consumed and could not be removed from the user's entitlements.
    /// Partner service should check for this item in their consume transaction records.
    /// </summary>
    public const string Revoked = "Revoked";
}


