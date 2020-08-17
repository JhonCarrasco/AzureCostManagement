using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace ISVDemoUsage.Models.MetricDefinitions
{
    public class MetricDefinition
    {
        public string id { get; set; }
        public string resourceId { get; set; }
        public string @namespace { get; set; }
        public string category { get; set; }
        public NameDefinition name { get; set; }
        public string displayDescription { get; set; }
        public bool isDimensionRequired { get; set; }
        public string unit { get; set; }
        public string primaryAggregationType { get; set; }
        public List<string> supportedAggregationTypes { get; set; }
        public List<MetricAvailability> metricAvailabilities { get; set; }
        public List<Dimension> dimensions { get; set; }
        
    }
}