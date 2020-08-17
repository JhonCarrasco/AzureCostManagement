using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISVDemoUsage.Models.Metrics
{
    public class Values
    {
        public string id { get; set; }
        public string type { get; set; }
        public Name name { get; set; }
        public string displayDescription { get; set; }
        public string unit { get; set; }
        public List<TimeSeries> timeseries { get; set; }
        public string errorCode { get; set; }
    }
}