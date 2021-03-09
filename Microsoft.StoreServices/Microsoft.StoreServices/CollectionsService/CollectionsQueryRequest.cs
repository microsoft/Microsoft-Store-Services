//-----------------------------------------------------------------------------
// CollectionsQueryRequest.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Microsoft.StoreServices
{
    public class CollectionsQueryRequest
    {
        [JsonProperty("maxPageSize")] public int MaxPageSize { get; set; }
        [JsonProperty("excludeDuplicates")] public bool ExcludeDuplicates { get; set; }
        [JsonProperty("EntitlementFilters")] public List<string> EntitlementFilters { get; set; }
        [JsonProperty("productSkuIds")] public List<ProductSkuId> ProductSkuIds { get; set; }
        [JsonProperty("market")] public string Market { get; set; }
        [JsonProperty("expandSatisfyingItems")] public bool ExpandSatisfyingItems { get; set; }
        [JsonProperty("beneficiaries")] public List<Beneficiary> Beneficiaries { get; set; }

        public CollectionsQueryRequest()
        {
            //  default values most commonly used
            Market = "neutral";
            ExpandSatisfyingItems = true; //  This expands the results to include any products that
                                          //  are included in a bundle the user owns.
            ExcludeDuplicates = true;     //  Only include one result (entitlement) per item.

            MaxPageSize = StoreServicesConstants.CollectionsPageSize;   //  Default Max is 100

            //  Default Game related product types
            //  Filter our results to include these product types
            EntitlementFilters = new List<string>() {
                EntitlementFilterTypes.Game,
                EntitlementFilterTypes.Consumable,
                EntitlementFilterTypes.Durable
            };

            Beneficiaries = new List<Beneficiary>();
        }
    }

    public class Beneficiary 
    {
        //  Generally this would be the Key if you are using this in a DB
        [JsonIgnore] public long BeneficiaryId { get; set; }
        [JsonProperty("identitytype")] public string Identitytype { get; set; }
        [JsonProperty("identityValue")] public string UserCollectionsId { get; set; }
        [JsonProperty("localTicketReference")] public string LocalTicketReference { get; set; }

        public Beneficiary()
        {
            Identitytype = "b2b";
            UserCollectionsId = "";
            LocalTicketReference = "";
        }
    }

    public class ProductSkuId
    {
        [JsonProperty("productId")] public string ProductId { get; set; }
        [JsonProperty("skuId")] public string SkuId { get; set; }
    }

    public static class EntitlementFilterTypes
    {
        public const string Game                = "*:Game";
        public const string Application         = "*:Application";
        public const string Durable             = "*:Durable";
        public const string Consumable          = "*:Consumable";
        public const string UnmanagedConsumable = "*:UnmanagedConsumable";
    }
}
