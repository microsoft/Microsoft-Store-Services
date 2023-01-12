//-----------------------------------------------------------------------------
// IAccessTokenProvider.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using Microsoft.StoreServices.Authentication;
using System.Threading.Tasks;

namespace Microsoft.StoreServices
{
    public interface IAccessTokenProvider
    {
        /// <summary>
        /// Provides a Service access token for your service that will have an audience 
        /// of https://onestore.microsoft.com
        /// </summary>
        /// <returns></returns>
        Task<AccessToken> GetServiceAccessTokenAsync();

        /// <summary>
        /// Provides a Collections access token for your service that will have an audience 
        /// of https://onestore.microsoft.com/b2b/keys/create/collections
        /// </summary>
        /// <returns></returns>
        Task<AccessToken> GetCollectionsAccessTokenAsync();

        /// <summary>
        /// Provides a Purchase access token for your service that will have an audience 
        /// of https://onestore.microsoft.com/b2b/keys/create/purchase
        /// </summary>
        /// <returns></returns>
        Task<AccessToken> GetPurchaseAccessTokenAsync();

        /// <summary>
        /// Provides an SAS Token (URI) to read and process Clawback v2 events for refunded products
        /// related to the Azure Active Directory credentials from the Access Tokens
        /// </summary>
        /// <returns></returns>
        Task<SASToken> GetClawbackV2SASTokenAsync();
    }
}
