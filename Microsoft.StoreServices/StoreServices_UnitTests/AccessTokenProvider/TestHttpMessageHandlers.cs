//-----------------------------------------------------------------------------
// CachedAccessTokenProvider_UnitTests.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Microsoft.StoreServices;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace StoreServices_UnitTests
{
    class TestExpiringServiceTokenHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var tokenResponse = new AccessToken
            {
                Audience = AccessTokenTypes.Service,
                EpochNotBefore = (uint)DateTimeOffset.Now.ToUnixTimeSeconds(),
                EpochExpires = (uint)DateTimeOffset.Now.ToUnixTimeSeconds() + 240,
                ExpiresIn = 240,
                ExtExpiredIn = 240,
                Token = "TestExpiringServiceTokenIn4Minutes",
                TokenType = AccessTokenTypes.Service
            };
            result.Content = new StringContent(JsonConvert.SerializeObject(tokenResponse));

            return Task.FromResult(result);
        }
    }

    class TestServiceTokenHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var tokenResponse = new AccessToken
            {
                Audience = AccessTokenTypes.Service,
                EpochNotBefore = (uint)DateTimeOffset.Now.ToUnixTimeSeconds(),
                EpochExpires = (uint)DateTimeOffset.Now.ToUnixTimeSeconds() + 14400,
                ExpiresIn = 14400,
                ExtExpiredIn = 14400,
                Token = "TestServiceToken",
                TokenType = AccessTokenTypes.Service
            };

            result.Content = new StringContent(JsonConvert.SerializeObject(tokenResponse));

            return Task.FromResult(result);
        }
    }

    class TestCollectionsTokenHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var tokenResponse = new AccessToken
            {
                Audience = AccessTokenTypes.Service,
                EpochNotBefore = (uint)DateTimeOffset.Now.ToUnixTimeSeconds(),
                EpochExpires = (uint)DateTimeOffset.Now.ToUnixTimeSeconds() + 14400,
                ExpiresIn = 14400,
                ExtExpiredIn = 14400,
                Token = "TestCollectionsToken",
                TokenType = AccessTokenTypes.Collections
            };

            result.Content = new StringContent(JsonConvert.SerializeObject(tokenResponse));

            return Task.FromResult(result);
        }
    }

    class TestPurchaseTokenHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var tokenResponse = new AccessToken
            {
                Audience = AccessTokenTypes.Service,
                EpochNotBefore = (uint)DateTimeOffset.Now.ToUnixTimeSeconds(),
                EpochExpires = (uint)DateTimeOffset.Now.ToUnixTimeSeconds() + 14400,
                ExpiresIn = 14400,
                ExtExpiredIn = 14400,
                Token = "TestCollectionsToken",
                TokenType = AccessTokenTypes.Purchase
            };

            result.Content = new StringContent(JsonConvert.SerializeObject(tokenResponse));

            return Task.FromResult(result);
        }
    }
}
