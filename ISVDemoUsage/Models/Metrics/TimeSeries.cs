using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISVDemoUsage.Models.Metrics
{
    public class TimeSeries
    {
        public List<MetadataValues> metadatavalues { get; set; }
        public List<Data> data { get; set; }
    }
}