//-----------------------------------------------------------------------------
// CollectionsV9ConsumeResponse.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.StoreServices.Collections.V8;

namespace Microsoft.StoreServices.Collections.V9
{
    /// <summary>
    /// JSON response from a successful consume request
    /// NOTE: Collections v9 does not have its own consume API and so we use same endpoint for V8's consume.
    /// </summary>
    public class CollectionsV9ConsumeResponse : CollectionsV8ConsumeResponse
    {

    }
}
