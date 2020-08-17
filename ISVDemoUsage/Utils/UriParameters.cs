using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISVDemoUsage.Utils
{
    public class UriParameters
    {
        public static string GetMetricName(string metricnames)
        {
            string metricName = nameof(metricnames);
            return "&"+ metricName + "=" + metricnames.Replace(" ", "%20");
        }
    }
}