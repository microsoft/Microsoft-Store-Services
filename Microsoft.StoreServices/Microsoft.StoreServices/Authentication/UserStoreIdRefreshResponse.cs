//-----------------------------------------------------------------------------
// UserStoreIdRefreshRequest.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Microsoft.StoreServices
{
    /// <summary>
    /// JSON response that contains the new UserStoreId after a refresh request.
    /// </summary>
    class UserStoreIdRefreshResponse
    {
        [JsonProperty("key")] public string Key { get; set; }
    }
}
