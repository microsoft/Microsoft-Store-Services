//-----------------------------------------------------------------------------
// IStoreServicesClientFactory.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

namespace Microsoft.StoreServices
{
    public interface IStoreServicesClientFactory
    {
        IStoreServicesClient CreateClient();

        /// <summary>
        /// Used to set the IAccessTokenProvider and ServiceIdentity if the factory needed
        /// to be created before those were available such as ASP.NET ConfigureService()
        /// </summary>
        /// <param name="serviceIdentity"></param>
        /// <param name="accessTokenProvider"></param>
        public void Initialize(string serviceIdentity, IAccessTokenProvider accessTokenProvider);
    }
}
