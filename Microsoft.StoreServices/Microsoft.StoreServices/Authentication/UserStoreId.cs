//-----------------------------------------------------------------------------
// UserStoreId.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;

namespace Microsoft.StoreServices
{
    public class UserStoreIdAudiences
    {
        //  These are the audience values for each access token type
        public const string UserCollectionsId = "https://collections.mp.microsoft.com/v6.0/keys";
        public const string UserPurchaseId = "https://purchase.mp.microsoft.com/v6.0/keys";
    }

    /// <summary>
    /// Extracts useful claims info from a UserStoreId (general term for either a UserCollectionsId
    /// or UserPurchaseId) and provides utility function to refresh an expired key
    /// </summary>
    public class UserStoreId
    {
        public string RefreshUri { get; set; }

        /// <summary>
        /// Identifies which type of UserStoreId this is based on the audience value
        /// UserCollectionsId (https://collections.mp.microsoft.com/v6.0/keys) or
        /// UserPurchaseId (https://purchase.mp.microsoft.com/v6.0/keys)
        /// </summary>
        public UserStoreIdType KeyType { get; set; }

        /// <summary>
        /// UTC date time when the key will expire and need to be refreshed
        /// </summary>
        public DateTime Expires { get; set; }
        
        /// <summary>
        /// The UserStoreId that was used to generate this object and would be
        /// used as the beneficiary in a b2b call for authentication
        /// </summary>
        public string Key { get; set; }

        public UserStoreId(string storeIdKey)
        {
            //  We can use the values in the payload to know how and when to 
            //  refresh it.
            UserStoreIdClaims keyClaims = JsonConvert.DeserializeObject<UserStoreIdClaims>(Jose.JWT.Payload(storeIdKey));
            RefreshUri = keyClaims.RefreshUri;
            Expires = keyClaims.Expires;
            Key = storeIdKey;

            if (keyClaims.Audience == UserStoreIdAudiences.UserCollectionsId)
            {
                KeyType = UserStoreIdType.UserCollectionsId;
            }
            else if (keyClaims.Audience == UserStoreIdAudiences.UserPurchaseId)
            {
                KeyType = UserStoreIdType.UserPurchaseId;
            }
            else
            {
                KeyType = UserStoreIdType.Unknown;
            }
        }
    }
}
