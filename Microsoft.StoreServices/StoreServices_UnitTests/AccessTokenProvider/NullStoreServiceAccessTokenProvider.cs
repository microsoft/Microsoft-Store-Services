//-----------------------------------------------------------------------------
// NullStoreServiceAccessTokenProvider.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Microsoft.StoreServices;
using Microsoft.StoreServices.Authentication;
using System.Threading.Tasks;

namespace StoreServices_UnitTests
{
    internal class NullStoreServiceAccessTokenProvider : IStoreServicesTokenProvider
    {
        public Task<AccessToken> GetCollectionsAccessTokenAsync()
        {
            return Task.FromResult(new AccessToken());
        }

        public Task<AccessToken> GetPurchaseAccessTokenAsync()
        {
            return Task.FromResult(new AccessToken());
        }

        public Task<AccessToken> GetServiceAccessTokenAsync()
        {
            return Task.FromResult(new AccessToken());
        }

        public Task<SASToken> GetClawbackV2SASTokenAsync()
        {
            return Task.FromResult(new SASToken());
        }
    }
}
