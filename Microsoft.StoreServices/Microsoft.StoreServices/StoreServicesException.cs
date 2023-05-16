//-----------------------------------------------------------------------------
// StoreServicesException.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using System;
using System.Net.Http;
using System.Runtime.Serialization;
using Microsoft.StoreServices.Collections.V8;

namespace Microsoft.StoreServices
{
    /// <summary>
    /// Represents general errors that happen in execution of the Microsoft.StoreServices namespace.
    /// </summary>
    public class StoreServicesException : Exception
    {
        /// <summary>
        /// Initialize a new instance of the StoreServicesException class
        /// </summary>
        public StoreServicesException()
        {
        }

        /// <summary>
        /// Initialize a new instance of the StoreServicesException class with the specified error message
        /// </summary>
        public StoreServicesException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initialize a new instance of the StoreServicesException class with the specified error message and 
        /// inner exception.
        /// </summary>
        public StoreServicesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initialize a new instance of the StoreServicesException class with serialized info.
        /// </summary>
        protected StoreServicesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    /// <summary>
    /// Represents errors from the HTTP request to the Microsoft Store Services.
    /// </summary>
    public class StoreServicesHttpResponseException : StoreServicesException
    {
        /// <summary>
        /// HTTP response that represents the failure to call the Microsoft Store Service.
        /// </summary>
        public HttpResponseMessage HttpResponseMessage { get; private set; }

        /// <summary>
        /// Initialize a new instance of the StoreServicesHttpResponseException class with the specified error message and 
        /// inner exception.
        /// </summary>
        public StoreServicesHttpResponseException(string message, HttpResponseMessage response) : base(message)
        {
            HttpResponseMessage = response;
        }
    }

    /// <summary>
    /// Represents errors when validating the request information before calling the Microsoft Store Services.
    /// </summary>
    public class StoreServicesClientRequestValidationException : StoreServicesException
    {

    }

    /// <summary>
    /// Represents errors from execution within an IStoreServicesTokenProvider of the Microsoft.StoreServices namespace.
    /// </summary>
    public class StoreServicesTokenProviderException : StoreServicesException
    {
        /// <summary>
        /// HTTP response that represents the failure to obtain an AccessToken.
        /// </summary>
        public HttpResponseMessage HttpResponseMessage { get; private set; }

        /// <summary>
        /// Initialize a new instance of the StoreServicesTokenProviderException class with the specified error message and 
        /// inner exception.
        /// </summary>
        public StoreServicesTokenProviderException(string message, HttpResponseMessage response) : base(message)
        {
            HttpResponseMessage = response;
        }
    }

    /// <summary>
    /// Represents errors from execution within an IStoreServicesClient of the Microsoft.StoreServices namespace.
    /// </summary>
    public class StoreServicesClientException : StoreServicesException
    {
        /// <summary>
        /// Initialize a new instance of the StoreServicesClientException class with the specified error message and 
        /// inner exception.
        /// </summary>
        public StoreServicesClientException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Represents an error from a consume request that failed
    /// </summary>
    public class StoreServicesClientConsumeException : StoreServicesException
    {
        public ConsumeErrorV8 ConsumeErrorInformation{ get; private set; }
        /// <summary>
        /// Initialize a new instance of the StoreServicesClientConsumeException class with the specified error message and 
        /// inner exception.
        /// </summary>
        public StoreServicesClientConsumeException(ConsumeErrorV8 consumeErrorInformation)
        {
            ConsumeErrorInformation = consumeErrorInformation;
        }
    }
}
