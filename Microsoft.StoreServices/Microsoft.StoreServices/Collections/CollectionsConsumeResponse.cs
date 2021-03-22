//-----------------------------------------------------------------------------
// CollectionsConsumeResponse.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Microsoft.StoreServices
{
    public class CollectionsConsumeResponse
    {
        [JsonProperty("itemId")] public string ItemId { get; set; }
        [JsonProperty("newQuantity")] public int NewQuantity { get; set; }
        [JsonProperty("trackingId")] public string TrackingId { get; set; }
        [JsonProperty("productId")] public string ProductId { get; set; }
        [JsonProperty("innererror")] public Innererror Innererror { get; set; }
    }

    public class Innererror
    {
        [JsonProperty("code")] public string Code { get; set; }
        [JsonProperty("data")] public List<string> Data { get; set; }
        [JsonProperty("details")] public List<object> Details { get; set; }
        [JsonProperty("message")] public string Message { get; set; }
        [JsonProperty("source")] public string Source { get; set; }
    }

    public class ConsumeError
    {
        [JsonProperty("code")] public string Code { get; set; }
        [JsonProperty("data")] public List<ConsumeErrorData> Data { get; set; }
        [JsonProperty("details")] public List<object> Details { get; set; }
        [JsonProperty("innererror")] public Innererror Innererror { get; set; }
        [JsonProperty("message")] public string Message { get; set; }
        [JsonProperty("source")] public string Source { get; set; }
    }

    public class ConsumeErrorData
    {
        public string QuantityAvailable { get; set; }
    }
}
