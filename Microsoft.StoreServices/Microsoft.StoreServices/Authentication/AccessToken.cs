//-----------------------------------------------------------------------------
// AccessToken.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;

namespace Microsoft.StoreServices.Authentication
{
    public class AccessToken
    {
        [JsonProperty("token_type")] public string TokenType { get; set; }
        [JsonProperty("expires_in")] public uint ExpiresIn { get; set; }
        [JsonProperty("ext_expires_in")] public uint ExtExpiredIn { get; set; }
        public DateTime Expires { get; set; }
        [JsonProperty("expires_on")] public uint EpochExpires 
        {
            get
            {
                return this._epochExpires;
            }

            set
            {
                this.Expires = UnixEpochToDateTime(value);
                this._epochExpires = value;
            }
        }

        public DateTime NotBefore { get; set; }
        [JsonProperty("not_before")] public uint EpochNotBefore
        {
            get
            {
                return this._epochNotBefore;
            }

            set
            {
                this.NotBefore = UnixEpochToDateTime(value);
                this._epochNotBefore = value;
            }
        }
        [JsonProperty("resource")] public string Audience { get; set; }
        [JsonProperty("access_token")] public string Token { get; set; }

        private uint _epochNotBefore;
        private uint _epochExpires;

        private DateTime UnixEpochToDateTime(uint seconds)
        {
            //  So that we can use a DateTime object we will
            //  just do the conversion from Unix Epoch now
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(seconds);
        }
    }
}
