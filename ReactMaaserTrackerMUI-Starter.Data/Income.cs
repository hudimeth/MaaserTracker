﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReactMaaserTrackerMUI_Starter.Data
{
    public class Income
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int SourceId { get; set; }

        [JsonIgnore]
        public IncomeSource Source { get; set; }
    }
}
