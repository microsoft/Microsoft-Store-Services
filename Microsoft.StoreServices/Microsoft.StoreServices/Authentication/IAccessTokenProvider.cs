//-----------------------------------------------------------------------------
// IAccessTokenProvider.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using System.Threading.Tasks;

namespace Microsoft.StoreServices
{
    public interface IAccessTokenProvider
    {
        Task<AccessToken> GetServiceAccessTokenAsync();

        Task<AccessToken> GetCollectionsAccessTokenAsync();

        Task<AccessToken> GetPurchaseAccessTokenAsync();
    }
}
