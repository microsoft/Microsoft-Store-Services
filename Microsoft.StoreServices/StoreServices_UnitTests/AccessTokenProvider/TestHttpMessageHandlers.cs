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
    /// <summary>
    /// Used to simulate a Service Access Token request where the resulting token is set to expire in 4 minutes.
    /// </summary>
    class TestExpiringServiceTokenHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var tokenResponse = new AccessToken
            {
                Audience = AccessTokenAudienceTypes.Service,
                ExpiresOn = DateTimeOffset.Now.AddSeconds(240),
                ExpiresIn = 240,
                Token = "TestExpiringServiceTokenIn4Minutes"
            };
            result.Content = new StringContent(JsonConvert.SerializeObject(tokenResponse));

            return Task.FromResult(result);
        }
    }

    /// <summary>
    /// Used to simulate a Service Access Token request.
    /// </summary>
    class TestServiceTokenHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var tokenResponse = new AccessToken
            {
                Audience = AccessTokenAudienceTypes.Service,
                ExpiresOn = DateTimeOffset.Now.AddSeconds(14400),
                ExpiresIn = 14400,
                Token = "TestServiceToken"
            };

            result.Content = new StringContent(JsonConvert.SerializeObject(tokenResponse));

            return Task.FromResult(result);
        }
    }

    /// <summary>
    /// Used to simulate a Collections Access Token request.
    /// </summary>
    class TestCollectionsTokenHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var tokenResponse = new AccessToken
            {
                Audience = AccessTokenAudienceTypes.Service,
                ExpiresOn = DateTimeOffset.Now.AddSeconds(14400),
                ExpiresIn = 14400,
                Token = "TestCollectionsToken"
            };

            result.Content = new StringContent(JsonConvert.SerializeObject(tokenResponse));

            return Task.FromResult(result);
        }
    }

    /// <summary>
    /// Used to simulate a Purchase Access Token request.
    /// </summary>
    class TestPurchaseTokenHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var tokenResponse = new AccessToken
            {
                Audience = AccessTokenAudienceTypes.Service,
                ExpiresOn = DateTimeOffset.Now.AddSeconds(14400),
                ExpiresIn = 14400,
                Token = "TestCollectionsToken"
            };

            result.Content = new StringContent(JsonConvert.SerializeObject(tokenResponse));

            return Task.FromResult(result);
        }
    }
}
