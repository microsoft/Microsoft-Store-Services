//-----------------------------------------------------------------------------
// CollectionsConsumeRequest.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Microsoft.StoreServices
{
    public class CollectionsConsumeRequest
    {
        /// <summary>
        /// Identifies the store account to consume the quantity from.  This
        /// contains the UserCollectionsId obtained from the client.
        /// </summary>
        [JsonProperty("beneficiary")] public Beneficiary Beneficiary { get; set; }
        
        /// <summary>
        /// ProductId / StoreId of the consumable product.
        /// </summary>
        [JsonProperty("productId")] public string ProductId { get; set; }

        /// <summary>
        /// Unique Id that is used to track the consume request and can
        /// be used to replay the request and verify the resulting status.
        /// </summary>
        //  Generally this would be the Key if you are using this in a DB
        [JsonProperty("trackingId")] public string TrackingId { get; set; }

        /// <summary>
        /// Quantity to be removed from the user's balance of the consumable product.
        /// </summary>
        [JsonProperty("removeQuantity")] public uint RemoveQuantity { get; set; }
    }
}
