//-----------------------------------------------------------------------------
// StoreServicesCachedTokenProvider.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using Microsoft.Extensions.Caching.Memory;
using Microsoft.StoreServices.Authentication;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace Microsoft.StoreServices
{
    /// <summary>
    /// An IStoreServicesProvider that generates, caches, retrieves, and manages the expiration of the 
    /// access tokens for your service required for Microsoft Store Services authentication.
    /// </summary>
    public class StoreServicesCachedTokenProvider : StoreServicesTokenProvider
    {
        /// <summary>
        /// Cache used to store and retrieve access tokens
        /// </summary>
        private readonly IMemoryCache _serverCache;

        /// <summary>
        /// Generates an access token provider that will manage a cache of the access tokens based 
        /// on your Entra ID credentials provided with either a secret key or Azure Managed Identity. 
        /// </summary>
        /// <param name="serverCache"></param>
        /// <param name="tenantId"></param>
        /// <param name="clientId"></param>
        /// <param name="secretOrManagedId"></param>
        /// <param name="useManagedId"></param>
        /// <exception cref="ArgumentException"></exception>
        public StoreServicesCachedTokenProvider( IMemoryCache serverCache,
                                                 string tenantId,
                                                 string clientId,
                                                 string secretOrManagedId,
                                                 bool useManagedId) : base (tenantId, clientId, secretOrManagedId, useManagedId)
        {
            if (serverCache == null)
            {
                throw new ArgumentException($"{nameof(serverCache)} required", nameof(serverCache));
            }
            _serverCache = serverCache;
        }

        /// <summary>
        /// Gets the currently cached Service access token or generates a new one if not cached.
        /// </summary>
        /// <returns></returns>
        public override Task<AccessToken> GetServiceAccessTokenAsync()
        {
            return GetTokenAsync(AccessTokenAudienceTypes.Service);
        }

        /// <summary>
        /// Gets the currently cached Collections access token or generates a new one if not cached.
        /// </summary>
        /// <returns></returns>
        public override Task<AccessToken> GetCollectionsAccessTokenAsync()
        {
            return GetTokenAsync(AccessTokenAudienceTypes.Collections);
        }

        /// <summary>
        /// Gets the currently cached Purchase access token or generates a new one if not cached.
        /// </summary>
        /// <returns></returns>
        public override Task<AccessToken> GetPurchaseAccessTokenAsync()
        {
            return GetTokenAsync(AccessTokenAudienceTypes.Purchase);
        }

        /// <summary>
        /// Retrieves a valid cached access token based on the audience provided. If not cached,
        /// a new token is created and cached.
        /// </summary>
        /// <param name="audience"></param>
        /// <returns></returns>
        protected async Task<AccessToken> GetTokenAsync(string audience)
        {
            var currentUTC = DateTimeOffset.UtcNow;

            //  If we are unable to acquire a token, it is expired, or expiring
            //  in less than 5 minutes we create a new one and cache it
            if(!_serverCache.TryGetValue(audience, out AccessToken token) ||
               (token.ExpiresOn.AddMinutes(-5)) <= currentUTC)
            {
                token = await CreateAccessTokenAsync(audience);
                CacheAccessToken(audience, token);
            }

            return token;
        }

        /// <summary>
        /// Adds this token to the cache so that future calls for the same audience do not
        /// require generating a new one.
        /// </summary>
        /// <param name="audience"></param>
        /// <param name="token"></param>
        private void CacheAccessToken(string audience, AccessToken token)
        {
            //  Add the new token so other calls can get it
            //  We will set the cache expiration for an hour less than the token is valid for.
            //  This will remove it from the cache before it expires and will get a new one
            //  for the next process
            var cacheExpirationOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = token.ExpiresOn,
                Priority = CacheItemPriority.High
            };

            _serverCache.Set<AccessToken>(audience, token, cacheExpirationOptions);
        }

        /// <summary>
        /// Gets the currently cached SAS Token to connect to the Clawback v2 event service.
        /// If not cached, it will create a new one.
        /// </summary>
        /// <returns></returns>
        public override Task<SASToken> GetClawbackV2SASTokenAsync()
        {
            return GetSASTokenAsync(SASTokenType.ClawbackV2);    
        }

        /// <summary>
        /// Retrieves a valid cached SAS Token the token type provided. If not cached,
        /// a new SAS token is created and cached.
        /// </summary>
        /// <param name="audience"></param>
        /// <returns></returns>
        protected virtual async Task<SASToken> GetSASTokenAsync(string tokenType)
        {
            var currentUTC = DateTimeOffset.UtcNow;

            //  If we are unable to acquire a token, it is expired, or expiring
            //  in less than 5 minutes we create a new one and cache it
            if (!_serverCache.TryGetValue(tokenType, out SASToken token) ||
               (token.ExpiresOn.AddMinutes(-5)) <= currentUTC)
            {
                AccessToken accessToken;
                //  Check which Access Token type we need to generate a new SAS Token
                if (tokenType == SASTokenType.ClawbackV2)
                {
                    accessToken = await GetServiceAccessTokenAsync();
                }
                else
                {
                    throw new ArgumentException($"Unknown SASTokenType of {tokenType} ", nameof(tokenType));
                }

                token = await CreateSASTokenAsync(tokenType, accessToken.Token);

                CacheSASToken(tokenType, token);
            }

            return token;
        }

        /// <summary>
        /// Adds this token to the cache so that future calls for the same SAS target do not
        /// require generating a new one.
        /// </summary>
        /// <param name="tokenType"></param>
        /// <param name="token"></param>
        private void CacheSASToken(string tokenType, SASToken token)
        {
            //  Add the new token so other calls can get it
            //  We will set the cache expiration for an hour less than the token is valid for.
            //  This will remove it from the cache before it expires and will get a new one
            //  for the next process
            var cacheExpirationOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = token.ExpiresOn,
                Priority = CacheItemPriority.High
            };

            _serverCache.Set<SASToken>(tokenType, token, cacheExpirationOptions);
        }

    }
}
