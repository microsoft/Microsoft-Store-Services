//-----------------------------------------------------------------------------
// UserStoreIdClaims.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;

namespace Microsoft.StoreServices
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
        [JsonProperty("iat")] public uint EpochIssued { get; set; }
        [JsonProperty("exp")] public uint EpochExpires { get; set; }
        [JsonProperty("nbf")] public uint EpochNotBefore { get; set; }
        public DateTime NotBefore => DateTime.UnixEpoch.AddSeconds(EpochNotBefore);
        public DateTime Expires => DateTime.UnixEpoch.AddSeconds(EpochExpires);
        public DateTime Issued => DateTime.UnixEpoch.AddSeconds(EpochIssued);
    }
}
