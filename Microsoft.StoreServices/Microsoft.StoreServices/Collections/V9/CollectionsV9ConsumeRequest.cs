//-----------------------------------------------------------------------------
// CollectionsV9ConsumeRequest.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using Microsoft.StoreServices.Collections.V8;

namespace Microsoft.StoreServices.Collections.V9
{
    /// <summary>
    /// JSON request body to initiate fulfillment of a consumable product.
    /// NOTE: Collections v9 does not have its own consume API and so we use same endpoint for V8's consume.
    /// </summary>
    public class CollectionsV9ConsumeRequest : CollectionsV8ConsumeRequest
    {
        
    }
}
