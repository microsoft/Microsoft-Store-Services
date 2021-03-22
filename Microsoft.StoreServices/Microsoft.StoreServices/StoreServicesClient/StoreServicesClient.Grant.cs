//-----------------------------------------------------------------------------
// StoreServicesClient.Grant.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace Microsoft.StoreServices
{
    public sealed partial class StoreServicesClient : IStoreServicesClient
    {
#pragma warning disable 1998
        public async Task<GrantProductResponse> GrantProductAsync(GrantProductRequest request)
        {
            throw new NotImplementedException();
        }
#pragma warning restore 1998
    }
}
