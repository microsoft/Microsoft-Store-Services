//-----------------------------------------------------------------------------
// CollectionsQueryResponse.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.StoreServices.Collections.V8
{
    /// <summary>
    /// JSON response body from the Collections service
    /// </summary>
    public class CollectionsV8QueryResponse
    {
        /// <summary>
        /// Token used in a follow-up query request to get the remainder of the
        /// query results.
        /// </summary>
        public string ContinuationToken { get; set; }

        /// <summary>
        /// List of CollectionsItems returned from the request
        /// </summary>
        public List<CollectionsV8Item> Items { get; set; }
    }
}
