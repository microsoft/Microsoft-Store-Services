//-----------------------------------------------------------------------------
// CollectionsV9Item.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Microsoft.StoreServices.Collections.V9
{
    /// <summary>
    /// An item the user owns or is entitled to
    /// </summary>
    public class CollectionsV9Item : CollectionsItemBase
    {
        /// <summary>
        /// Provides specific information to manage this product through the Recurrence services if this is a subscription.
        /// </summary>
        [JsonProperty("recurrenceId")] public string RecurrenceId { get; set; }
    }
}
