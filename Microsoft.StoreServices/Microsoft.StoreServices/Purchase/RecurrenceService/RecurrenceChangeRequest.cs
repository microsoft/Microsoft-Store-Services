//-----------------------------------------------------------------------------
// RecurrenceChangeRequest.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;

namespace Microsoft.StoreServices.Purchase
{
    public enum RecurrenceChangeType
    {
        Cancel,
        Extend,
        Refund,
        ToggleAutoRenew
    }

    public class RecurrenceChangeRequest
    {
        [JsonProperty("b2bKey")] public string Beneficiary { get; set; }
        [JsonProperty("changeType")] public string ChangeType { get; set; }
        [JsonProperty("extensionTimeInDays")] public int ExtensionTimeInDays { get; set; }
    }
}
