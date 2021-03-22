//-----------------------------------------------------------------------------
// RecurrenceQueryRequest.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Microsoft.StoreServices
{
    public class RecurrenceQueryRequest
    {
        [JsonProperty("b2bKey")] public string Beneficiary { get; set; }
    }
}
