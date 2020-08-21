using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ISVDemoUsage.Utils
{
    public class UriParameters
    {
        public static string GetMetricParameterFormat(
            string metricnames,
            string timespan,
            string interval,
            string aggregation,
            string top,
            string orderby,
            string filter,
            string resultType,
            string api_version,
            string metricnamespace)
        {
            //string metricName = nameof(metricnames);
            //return "&"+ metricName + "=" + metricnames.Replace(" ", "%20");
            string uriParametersString = "?";
            List<string> parameterList = new List<string>();
            parameterList.Add((!timespan.Equals("")) ? "timespan=" + (timespan.Replace(":", "%3A")).Replace("/", "%2F") : "");
            parameterList.Add((!interval.Equals("")) ? "interval=" + interval : "");
            parameterList.Add((!metricnames.Equals("")) ? "metricnames=" + (metricnames.Replace(":", "%20")).Replace("/", "%2F") : "");
            parameterList.Add((!aggregation.Equals("")) ? "aggregation=" + aggregation.Replace(",", "%2C") : "");
            parameterList.Add((!top.Equals("")) ? "top=" + top : "");
            parameterList.Add((!orderby.Equals("")) ? "orderby=" + orderby.Replace(" ", "%20") : "");
            parameterList.Add("");
            parameterList.Add("");
            parameterList.Add((!api_version.Equals("")) ? "api-version=" + api_version : "");
            parameterList.Add((!metricnamespace.Equals("")) ? "metricnamespace=" + metricnamespace.Replace("/", "%2F") : "");

            foreach(var item in parameterList)
            {
                if (!item.Equals(""))
                {
                    if (!uriParametersString.Equals("?"))
                    {
                        uriParametersString += "&" + item;
                    }
                    else
                    {
                        uriParametersString += item;
                    }
                }
            }

            return uriParametersString;
        }
    }
}