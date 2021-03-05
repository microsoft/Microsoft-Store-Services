//-----------------------------------------------------------------------------
// UserStoreIdRefreshRequest.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Microsoft.StoreServices.Authentication
{
    class UserStoreIdRefreshResponse
    {
        [JsonProperty("key")] public string Key { get; set; }
    }
}
