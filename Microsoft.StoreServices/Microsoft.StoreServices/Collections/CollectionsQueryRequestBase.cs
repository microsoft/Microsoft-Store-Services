//-----------------------------------------------------------------------------
// CollectionsQueryRequestBase.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Microsoft.StoreServices.Collections
{
    public class CollectionsQueryRequestBase
    {
        /// <summary>
        /// The user for which this item is entitled. You will use the UserCollectionsId in this property to define the user you want the results for.
        /// </summary>
        [JsonProperty("beneficiaries")][JsonRequired] public List<CollectionsRequestBeneficiary> Beneficiaries { get; set; }

        /// <summary>
        /// The maximum number of products to return in one response. The default and maximum value is 100.
        /// </summary>
        [JsonProperty("continuationToken")][JsonIgnore] public string ContinuationToken { get; set; }

        /// <summary>
        /// Removes duplicate entitlements where the user might be entitled to a single product from multiple sources.
        /// </summary>
        [JsonProperty("excludeDuplicates")] public bool ExcludeDuplicates { get; set; }

        /// <summary>
        /// The maximum number of products to return in one response. The default and maximum value is 100.
        /// </summary>
        [JsonProperty("maxPageSize")] public int MaxPageSize { get; set; }

        /// <summary>
        /// If specified, the service only returns products applicable to the provided product/SKU pairs. Recommended for all service-to-service queries for faster and more reliable results.
        /// </summary>
        [JsonProperty("productSkuIds")][JsonIgnore] public List<ProductSkuId> ProductSkuIds { get; set; }

        /// <summary>
        /// Specifies which development sandbox the results should be scoped to.  If no sandbox is specified the results will always be to the sandbox RETAIL.
        /// </summary>
        [JsonProperty("sbx")][JsonIgnore] public string SandboxId { get; set; }

        /// <summary>
        /// Filter the results to this validity type which is based off the Status values of the items to be returned.
        /// </summary>
        [JsonProperty("validityType")] public string ValidityType { get; set; }
    }

    /// <summary>
    /// JSON structure to provide the UserCollectionsId to identify which user we are making the request for.
    /// </summary>
    public class CollectionsRequestBeneficiary
    {
        /// <summary>
        /// Must be set to "b2b".
        /// </summary>
        [JsonProperty("identitytype")] public string Identitytype { get; set; }

        /// <summary>
        /// The User Store ID key that represents the identity of the user for whom you want to report a consumable product as fulfilled.
        /// </summary>
        [JsonProperty("identityValue")] public virtual string UserCollectionsId { get; set; }

        /// <summary>
        /// The requested identifier for the returned response. We recommend that you use the same value as the userId claim in the User Store ID key.
        /// </summary>
        [JsonProperty("localTicketReference")] public string LocalTicketReference { get; set; }

        public CollectionsRequestBeneficiary()
        {
            Identitytype = "b2b";
            UserCollectionsId = "";
            LocalTicketReference = "";
        }
    }

    /// <summary>
    /// JSON structure to denote the ProductId and the Sku
    /// </summary>
    public class ProductSkuId
    {
        /// <summary>
        /// ProductId (StoreId) of the item offered within the store.
        /// </summary>
        [JsonProperty("productId")] public string ProductId { get; set; }

        /// <summary>
        /// SkuId representing the specific sub-offering of the product that was purchased.  For query requests, this value can be blank.
        /// </summary>
        [JsonProperty("skuId")][JsonIgnore] public string SkuId { get; set; }
    }

    /// <summary>
    /// Filter options for Collections query requests.  Results will be based on specific Status values.
    /// </summary>
    public static class ValidityTpes
    {
        /// <summary>
        /// Everything will be returned.
        /// </summary>
        public const string All = "All";

        /// <summary>
        /// Items that have a state of Active
        /// </summary>
        public const string Valid = "Valid";

        /// <summary>
        /// Items that are Expired, Revoked, or in other non-valid states.
        /// </summary>
        public const string Invalid = "Invalid";

        /// <summary>
        /// Items that are active but not yet reached their end date
        /// </summary>
        public const string NotYetEnded = "NotYetEnded";

        /// <summary>
        /// Items that have not yet been activated such as pre-orders
        /// </summary>
        public const string NotYetStarted = "NotYetStarted";
    }
}
