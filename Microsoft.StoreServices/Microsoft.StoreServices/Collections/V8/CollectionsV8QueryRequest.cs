//-----------------------------------------------------------------------------
// CollectionsV8QueryRequest.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Microsoft.StoreServices.Collections.V8
{
    /// <summary>
    /// JSON request body for a query to the Collections service
    /// </summary>
    public class CollectionsV8QueryRequest : CollectionsQueryRequestBase
    {

        /// <summary>
        /// Specifies which product types to return in the query results. For a list of valid values, see EntitlementFilterTypes.]
        /// </summary>
        [JsonProperty("entitlementFilters")]
        public List<string> EntitlementFilters { get; set; }

        /// <summary>
        /// The country/region/market that you want to check the entitlement for. Using “neutral” (recommended) searches all markets. Otherwise, use the two-character ISO 3166 country/region code, for example, US.
        /// </summary>
        [JsonProperty("market")]
        public string Market { get; set; }

        /// <summary>
        /// Include items that are entitled through bundles or subscriptions in the results. If set to false, the results only contain the items the user has purchased, such as the parent bundle’s product information. If you’re using this parameter, always specify which products you want results for to avoid long or timed-out requests.
        /// </summary>
        [JsonProperty("expandSatisfyingItems")]
        public bool ExpandSatisfyingItems { get; set; }

        public CollectionsV8QueryRequest()
        {
            //  default values most commonly used
            Market = "neutral";
            ExpandSatisfyingItems = true; //  This expands the results to include any products that
                                          //  are included in a bundle the user owns.
            ExcludeDuplicates = true;     //  Only include one result (entitlement) per item.

            MaxPageSize = 100;            //  Default Max is 100

            //  Default Game related product types
            //  Filter our results to include these product types
            EntitlementFilters = new List<string>() {
                EntitlementFilterTypes.Game,
                EntitlementFilterTypes.Consumable,
                EntitlementFilterTypes.UnmanagedConsumable,
                EntitlementFilterTypes.Durable,
                EntitlementFilterTypes.Pass
            };

            Beneficiaries = new List<CollectionsRequestBeneficiary>();
        }
    }

    /// <summary>
    /// String values that can be added to a query request to filter the results to these specific
    /// product types.
    /// </summary>
    public static class EntitlementFilterTypes
    {
        /// <summary>
        /// Games products.
        /// </summary>
        public const string Game = "*:Game";

        /// <summary>
        /// Apps that are not games.
        /// </summary>
        public const string Application = "*:Application";

        /// <summary>
        /// Game content such as DLC and most single-time purchase items
        /// </summary>
        public const string Durable = "*:Durable";

        /// <summary>
        /// Store-managed consumable products (recommended consumable type for most scenarios).
        /// </summary>
        public const string Consumable = "*:Consumable";

        /// <summary>
        /// Developer-managed consumables that must be fulfilled before being able to be purchased by the user again.
        /// </summary>
        public const string UnmanagedConsumable = "*:UnmanagedConsumable";

        /// <summary>
        /// Store-managed subscriptions. Ex: Xbox Game Pass, Publisher specific subscription.  This product type is not 
        /// the Add-on Subscription type that can be configured in the Add-ons page in Partner Center.
        /// </summary>
        public const string Pass = "*:Pass";
    }

}
