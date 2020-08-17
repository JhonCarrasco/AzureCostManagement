﻿using ISVDemoUsage.Forms;
using ISVDemoUsage.Models;
using ISVDemoUsage.Models.MetricDefinitions;
using ISVDemoUsage.Models.Metrics;
using ISVDemoUsage.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ISVDemoUsage.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
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
         
        public JsonResult GetMetricsResource(string organizationId, string resourceUri, string metricnames) 
        {
            MetricsResource metricResource = new MetricsResource();
            try
            {

                if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
                {
                    var resourcerString = AzureResourceManagerUtil.GetMetricsResource(organizationId, resourceUri, metricnames);
                    metricResource = JsonConvert.DeserializeObject<MetricsResource>(resourcerString);

                    
                }

                return Json(new { success = true, data = metricResource }, JsonRequestBehavior.AllowGet);

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
    }
}