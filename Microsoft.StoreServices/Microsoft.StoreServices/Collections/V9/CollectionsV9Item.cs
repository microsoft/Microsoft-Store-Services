//-----------------------------------------------------------------------------
// CollectionsV9Item.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Microsoft.StoreServices.Collections.V9
{
    /// <summary>
    /// An item the user owns or is entitled to
    /// </summary>
    public class CollectionsV9Item : CollectionsItemBase
    {
        

        /// <summary>
        /// The offerInstanceId value that would have been provided if calling the Xbox Inventory Service. This shouldn’t be needed
        /// in most cases.
        /// </summary>
        [JsonProperty("legacyOfferInstanceId")] public string LegacyOfferInstanceId { get; set; }

        /// <summary>
        /// The older ProductID format from the Xbox Developer Portal and used by the Xbox Inventory Service. New products created
        /// in Partner Center don’t have these by default but can be enrolled to have this value if needed.
        /// </summary>
        [JsonProperty("legacyProductId")] public string LegacyProductId { get; set; }

        /// <summary>
        /// The ID of the previously supplied localTicketReference in the request body.
        /// </summary>
        [JsonProperty("localTicketReference")] public string LocalTicketReference { get; set; }

        /// <summary>
        /// The two-character ISO 3166 country code indicating the region store the product was acquired from.
        /// </summary>
        [JsonProperty("purchasedCountry")] public string PurchasedCountry { get; set; }

        /// <summary>
        /// Indicates what type of product that this relates to. Usually, this is “Games” but can also be blank for
        /// game-related content.
        /// </summary>
        [JsonProperty("productFamily")] public string ProductFamily { get; set; }

        /// <summary>
        /// Provides specific information to manage this product through the Recurrence services if this is a subscription.
        /// </summary>
        [JsonProperty("recurrenceData")] public RecurrenceData RecurrenceData { get; set; }

        /// <summary>
        /// Indicates if the item is entitled because of a sharing scenario. When calling Microsoft Store Services from your own service this should always return as None.
        /// </summary>
        [JsonProperty("sharingSource")] public string SharingSource { get; set; }

       

        /// <summary>
        /// The offer ID from an in-app purchase.
        /// </summary>
        [JsonProperty("devOfferId")] public string DevOfferId { get; set; }

    }
}
