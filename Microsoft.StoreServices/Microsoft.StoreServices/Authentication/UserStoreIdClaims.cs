//-----------------------------------------------------------------------------
// UserStoreIdClaims.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;

namespace Microsoft.StoreServices.Authentication
{
    /// <summary>
    /// Claims contained within a UserStoreId (general term for either UserCollectionsId or UserPurchaseId)
    /// </summary>
    public class UserStoreIdClaims
    {
        /// <summary>
        /// Azure Active Directory App Client ID used for the Access Token that generated this
        /// UserStoreId
        /// </summary>
        [JsonProperty("http://schemas.microsoft.com/marketplace/2015/08/claims/key/clientId")]
        public string ClientId { get; set; }
        
        /// <summary>
        /// The PublisherUserId string that was used when generating the UserStoreId on the
        /// Client.  NOTE: This is controlled by the client and does not represent an actual 
        /// identity value of the user account this UserStoreId represents.
        /// </summary>
        [JsonProperty("http://schemas.microsoft.com/marketplace/2015/08/claims/key/userId")]
        public string UserId { get; set; }

        [JsonProperty("http://schemas.microsoft.com/marketplace/2015/08/claims/key/payload")]
        public string Payload { get; set; }

        [JsonProperty("http://schemas.microsoft.com/marketplace/2015/08/claims/key/refreshUri")]
        public string RefreshUri { get; set; }

        [JsonProperty("iss")] public string Issuer { get; set; }

        [JsonProperty("aud")] public string Audience { get; set; }

        public DateTime Issued { get; set; }

        private uint _epochIssued;
        [JsonProperty("iat")] public uint EpochIssued
        {
            get
            {
                return this._epochIssued;
            }

            set
            {
                this.Issued = UnixEpochToDateTime(value);
                this._epochIssued = value;
            }
        }

        public DateTime Expires { get; set; }

        private uint _epochExpires;
        [JsonProperty("exp")] public uint EpochExpires
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

        private uint _epochNotBefore;
        [JsonProperty("nbf")] public uint EpochNotBefore
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

        private DateTime UnixEpochToDateTime(uint seconds)
        {
            //  So that we can use a DateTime object we will
            //  just do the conversion from Unix Epoch now
            return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(seconds);
        }
    }
}
