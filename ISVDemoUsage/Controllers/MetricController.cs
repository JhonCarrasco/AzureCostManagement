using ISVDemoUsage.Forms;
using ISVDemoUsage.Models;
using ISVDemoUsage.Models.MetricDefinitions;
using ISVDemoUsage.Models.Metrics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ISVDemoUsage.Controllers
{
    public class MetricController : Controller
    {
        // GET: Metric
        public ActionResult Index()
        {
            return View(SubscriptionFormFactory.GetInitializing());
        }
        public JsonResult GetResourceGroupList(string subscriptionId, string organizationId)
        {
            List<Resourcer> resourceGroups = new List<Resourcer>();
            try
            {

                if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
                {
                    var resourcerString = AzureResourceManagerUtil.GetResourceGroups(subscriptionId, organizationId);
                    ResourcerLoad resourcerLoad = JsonConvert.DeserializeObject<ResourcerLoad>(resourcerString);
                    resourceGroups = resourcerLoad.value;
                }

                return Json(new { success = true, data = resourceGroups }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetResourceList(string subscriptionId, string organizationId, string resourceGroup)
        {
            List<Resourcer> resources = new List<Resourcer>();
            try
            {

                if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
                {
                    var resourcerString = AzureResourceManagerUtil.GetResourcersByResourcerGroup(subscriptionId, organizationId, resourceGroup);
                    ResourcerLoad resourcerLoad = JsonConvert.DeserializeObject<ResourcerLoad>(resourcerString);

                    //foreach(var item in resourcerLoad.value)
                    //{
                    //    if(item.Type.Equals(""))
                    //}

                    resources = resourcerLoad.value;
                }

                return Json(new { success = true, data = resources }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetMetricDefinitions(string organizationId, string resourceUri)
        {
            List<MetricDefinition> listado = new List<MetricDefinition>();
            try
            {

                if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
                {
                    var resourcerString = AzureResourceManagerUtil.GetMetricDefinitions(organizationId, resourceUri);
                    MetricDefinitionsLoad load = JsonConvert.DeserializeObject<MetricDefinitionsLoad>(resourcerString);
                    listado = load.value;
                }

                var jsonResult = Json(new { success = true, data = listado }, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;

            }
            catch (Exception e)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetMetricsResource(
            string organizationId, 
            string resourceUri, 
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
            MetricsResource metricResource = new MetricsResource();
            try
            {

                if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
                {
                    var resourcerString = AzureResourceManagerUtil.GetMetricsResource(
                        organizationId, 
                        resourceUri,
                        metricnames,
                        timespan,
                        interval,
                        aggregation,
                        top,
                        orderby,
                        filter,
                        resultType,
                        api_version,
                        metricnamespace);
                        metricResource = JsonConvert.DeserializeObject<MetricsResource>(resourcerString);


                }
                string json = JsonConvert.SerializeObject(metricResource.value[0].timeseries[0].data);
                string jsonFormatted = JValue.Parse(json).ToString(Formatting.Indented);

                return Json(new { success = true, data = metricResource, datajsonFormatted = jsonFormatted }, JsonRequestBehavior.AllowGet); 
                //return Json(new { success = true, data = metricResource }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}