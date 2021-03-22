//-----------------------------------------------------------------------------
// StoreServicesException.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Runtime.Serialization;

namespace Microsoft.StoreServices
{
    class StoreServicesException : Exception
    {
        public StoreServicesException()
        {
        }

        public StoreServicesException(string message) : base(message)
        {
        }

        public StoreServicesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StoreServicesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    class StoreServicesHttpResponseException : StoreServicesException
    {
        public HttpResponseMessage HttpResponseMessage { get; private set; }

        public StoreServicesHttpResponseException(string message, HttpResponseMessage response) : base(message)
        {
            HttpResponseMessage = response;
        }
    }

    class StoreServicesClientRequestValidationException : StoreServicesException
    {

    }

    class AccessTokenProviderException : StoreServicesException
    {
        public HttpResponseMessage HttpResponseMessage { get; private set; }

        public AccessTokenProviderException(string message, HttpResponseMessage response) : base(message)
        {
            HttpResponseMessage = response;
        }
    }

    class StoreServicesClientException : StoreServicesException
    {
        public StoreServicesClientException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
