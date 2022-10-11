//-----------------------------------------------------------------------------
// CollectionsV9QueryRequest.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Microsoft.StoreServices.Collections.V9
{
    /// <summary>
    /// JSON request body for a query to the Collections service
    /// </summary>
    public class CollectionsV9QueryRequest : CollectionsQueryRequestBase
    {
        /// <summary>
        /// Required for Collections v9 - the service only returns products applicable to the provided productIds. 
        /// </summary>
        [JsonRequired]
        [JsonProperty("productSkuIds")] public new List<ProductSkuId> ProductSkuIds { get; set; }

        public CollectionsV9QueryRequest()
        {
            Beneficiaries     = new List<CollectionsRequestBeneficiary>();
            ProductSkuIds     = new List<ProductSkuId>();
            ExcludeDuplicates = true;     //  Only include one result (entitlement) per item.
            MaxPageSize       = 100;            //  Default Max is 100
        }
    }

    

    
}
