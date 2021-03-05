﻿//-----------------------------------------------------------------------------
// RecurrenceQueryResponse.cs
//
// Xbox Advanced Technology Group (ATG)
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

using System.Collections.Generic;

namespace Microsoft.StoreServices.Purchase
{
    public class RecurrenceQueryResponse
    {
        public List<RecurrenceItem> Items { get; set; }
    }
}
