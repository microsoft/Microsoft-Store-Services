//-----------------------------------------------------------------------------
// CollectionsV9QueryResponse.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.StoreServices.Collections.V9
{
    /// <summary>
    /// JSON response body from the Collections service
    /// </summary>
    public class CollectionsV9QueryResponse
    {
        /// <summary>
        /// List of CollectionsItems returned from the request
        /// </summary>
        public List<CollectionsV9Item> Items { get; set; }
    }
}
