//-----------------------------------------------------------------------------
// CachedAccessTokenProvider.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Microsoft.StoreServices
{
    /// <summary>
    /// Manages the active cache of Access Tokens for the service that
    /// will be used in the authentication flow of the
    /// Microsoft.StoreServices
    /// </summary>
    public class CachedAccessTokenProvider : AccessTokenProvider
    {
        private readonly IMemoryCache _serverCache;

        public CachedAccessTokenProvider(IMemoryCache serverCache,
                                         string tenantId,
                                         string clientId,
                                         string clientSecret) : base (tenantId, clientId, clientSecret)
        {
            if (serverCache == null)
            {
                throw new ArgumentException($"{nameof(serverCache)} required", nameof(serverCache));
            }
            _serverCache = serverCache;
        }

        new public Task<AccessToken> GetServiceAccessTokenAsync()
        {
            return GetTokenAsync(AccessTokenTypes.Service);
        }

        new public Task<AccessToken> GetCollectionsAccessTokenAsync()
        {
            return GetTokenAsync(AccessTokenTypes.Collections);
        }

        new public Task<AccessToken> GetPurchaseAccessTokenAsync()
        {
            return GetTokenAsync(AccessTokenTypes.Purchase);
        }

        /// <summary>
        /// Will cache and fetch active tokens for the audience provided using the
        /// ServerCache.  If the target token is expired or not present in the
        /// cache this API will get a new one and cache it.
        /// </summary>
        /// <param name="audience"></param>
        /// <returns></returns>
        protected async Task<AccessToken> GetTokenAsync(string audience)
        {
            var currentUTC = DateTime.Now.ToUniversalTime();

            //  If we are unable to acquire a token or
            //  the token is expired, create a new one
            if(!_serverCache.TryGetValue(audience, out AccessToken token) ||
               DateTime.Compare(token.Expires.AddMinutes(-5), currentUTC) <= 0)
            {
                token = await CreateAccessTokenAsync(audience);
                CacheAccessToken(audience, token);
            }

            return token;
        }

        /// <summary>
        /// Puts this token into the cache so that we can recall it faster than generating
        /// a new one for each request.
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
                AbsoluteExpiration = token.Expires,
                Priority = CacheItemPriority.High
            };

            _serverCache.Set<AccessToken>(audience, token, cacheExpirationOptions);
        }
    }
}
