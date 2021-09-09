//-----------------------------------------------------------------------------
// ClawbackResponse.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.StoreServices
{
    /// <summary>
    /// JSON response body from the Clawback service
    /// </summary>
    public class ClawbackQueryResponse
    {
        /// <summary>
        /// List of ClawbackItems returned from the request
        /// </summary>
        public List<ClawbackItem> Items { get; set; }
    }
}
