using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISVDemoUsage.Models.Metrics
{
    public class MetricsResource
    {
        public int cost { get; set; }
        public string timespan { get; set; }
        public string interval { get; set; }
        public List<Values> value { get; set; }
        public string @namespace { get; set; }
        public string resourceregion { get; set; }
    }
}