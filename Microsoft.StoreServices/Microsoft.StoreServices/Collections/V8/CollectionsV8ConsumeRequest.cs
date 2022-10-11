//-----------------------------------------------------------------------------
// CollectionsConsumeRequest.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Microsoft.StoreServices.Collections.V8
{
    /// <summary>
    /// JSON request body to initiate fulfillment of a consumable product
    /// </summary>
    public class CollectionsV8ConsumeRequest
    {
        /// <summary>
        /// Identifies the store account to consume the quantity from.  This
        /// contains the UserCollectionsId obtained from the client. This is
        /// marked with virtual so it can be overridden to define a key value.
        /// </summary>
        [JsonProperty("beneficiary")] public virtual CollectionsRequestBeneficiary RequestBeneficiary { get; set; }

        /// <summary>
        /// ProductId / StoreId of the consumable product.
        /// </summary>
        [JsonProperty("productId")] public string ProductId { get; set; }

        /// <summary>
        /// Unique Id that is used to track the consume request and can
        /// be used to replay the request and verify the resulting status.
        /// Generally this would be the Key if you are using this in a DB
        /// so it is marked with virtual so it can be overridden.
        /// </summary>
        [JsonProperty("trackingId")] public virtual string TrackingId { get; set; }

        /// <summary>
        /// Quantity to be removed from the user's balance of the consumable product.
        /// </summary>
        [JsonProperty("removeQuantity")] public uint RemoveQuantity { get; set; }

        /// <summary>
        /// Quantity to be removed from the user's balance of the consumable product.
        /// </summary>
        [JsonProperty("includeOrderIds")] public bool IncludeOrderIds { get; set; } = false;

        /// <summary>
        /// Used to determine if this is a managed or unmanaged consumable as the consume request JSON is different
        /// between them.
        /// </summary>
        [JsonIgnore] public bool IsUnmanagedConsumable { get; set; }

        /// <summary>
        /// If this is an unmanaged consumable, then we don't want to add the 'removeQuantity' parameter.  Unmanaged
        /// are always qty of 1 and the request will fail if we pass in any value for 'removeQuantity'.
        /// </summary>
        /// <returns></returns>
        public bool ShouldSerializeRemoveQuantity()
        {
            // don't serialize the Manager property if an employee is their own manager
            return !IsUnmanagedConsumable;
        }
    }
}
