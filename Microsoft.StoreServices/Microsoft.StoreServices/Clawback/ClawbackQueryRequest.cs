//-----------------------------------------------------------------------------
// ClawbackRequest.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Microsoft.StoreServices
{
    public class ClawbackQueryRequest
    {
        [JsonProperty("b2bKey")] public string Beneficiary { get; set; }
        [JsonProperty("lineItemStateFilter")] public List<string> LineItemStateFilter { get; set; }

        public ClawbackQueryRequest()
        {
            //  default values most commonly used
            LineItemStateFilter = new List<string>() 
            { 
                LineItemStates.Revoked,
                LineItemStates.Refunded
            };

            Beneficiary = ""; 
        }
    }

    public static class LineItemStates
    {
        public const string Purchased = "Purchased";
        public const string Revoked   = "Revoked";
        public const string Refunded  = "Refunded";
    }


    


}
