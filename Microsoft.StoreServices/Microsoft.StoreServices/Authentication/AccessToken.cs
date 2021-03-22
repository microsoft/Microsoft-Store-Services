//-----------------------------------------------------------------------------
// AccessToken.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;

namespace Microsoft.StoreServices
{
    public class AccessToken
    {
        [JsonProperty("token_type")] public string TokenType { get; set; }
        [JsonProperty("expires_in")] public uint ExpiresIn { get; set; }
        [JsonProperty("ext_expires_in")] public uint ExtExpiredIn { get; set; }
        [JsonProperty("expires_on")] public uint EpochExpires { get; set; }
        [JsonProperty("not_before")] public uint EpochNotBefore { get; set; }
        [JsonProperty("resource")] public string Audience { get; set; }
        [JsonProperty("access_token")] public string Token { get; set; }

        /// <summary>
        /// The date and time when the Access Token expires
        /// </summary>
        public DateTime Expires => DateTime.UnixEpoch.AddSeconds(EpochExpires);

        /// <summary>
        /// The earliest time when the Access Token can expire
        /// </summary>
        public DateTime NotBefore => DateTime.UnixEpoch.AddSeconds(EpochNotBefore);
    }

    public class AccessTokenTypes
    {
        //  These are the audience values for each access token type
        public const string Service     = "https://onestore.microsoft.com";
        public const string Collections = "https://onestore.microsoft.com/b2b/keys/create/collections";
        public const string Purchase    = "https://onestore.microsoft.com/b2b/keys/create/purchase";
    }
}
