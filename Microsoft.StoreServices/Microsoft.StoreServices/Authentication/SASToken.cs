//-----------------------------------------------------------------------------
// 2SASToken.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License file under the project root for
// license information.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.StoreServices.Authentication
{
    public class SASToken
    {
        /// <summary>
        /// Actual token to be used in calling the services or obtaining User Store Ids
        /// </summary>
        [JsonProperty("uri")]
        public string Token { get; set; }

        /// <summary>
        /// The UTC date and time when the Access Token expires
        /// </summary>
        public DateTimeOffset ExpiresOn { get; set; }
    }

    /// <summary>
    /// Audience URI string values for each of the Access Token types used for Microsoft Store Services auth
    /// </summary>
    public class SASTokenType
    {
        /// <summary>
        /// SAS token that will generate a limited lifetime URI to read Clawback V2 events related to refunds and chargebacks
        /// </summary>
        public const string ClawbackV2 = "https://purchase.mp.microsoft.com/v8.0/b2b/clawback/sastoken";
    }
}
