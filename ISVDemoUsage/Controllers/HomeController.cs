using ISVDemoUsage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Collections;
using ISVDemoUsage.Models.Monitor;
using ISVDemoUsage.Models.CostManagement;

namespace ISVDemoUsage.Controllers
{
    public class HomeController : Controller
    {
        private DataAccess db = new DataAccess();

        public ActionResult Index()
        {
            HomeIndexViewModel model = null;

            if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
            {
                model = new HomeIndexViewModel();
                model.UserOrganizations = new Dictionary<string,Organization>();
                model.UserSubscriptions = new Dictionary<string,Subscription>();
                model.UserCanManageAccessForSubscriptions = new List<string>();
                model.DisconnectedUserOrganizations = new List<string>();

                var orgnaizations = AzureResourceManagerUtil.GetUserOrganizations();
                                
                foreach (Organization org in orgnaizations)
                {
                    model.UserOrganizations.Add(org.Id, org);
                    var subscriptions = AzureResourceManagerUtil.GetUserSubscriptions(org.Id);
                    
                    if (subscriptions != null)
                    {
                        foreach (var subscription in subscriptions)
                        {

                            Subscription s = db.Subscriptions.Find(subscription.Id);
                            if (s != null)
                            {
                                subscription.IsConnected = true;
                                subscription.ConnectedOn = s.ConnectedOn;
                                subscription.ConnectedBy = s.ConnectedBy;
                                subscription.AzureAccessNeedsToBeRepaired = AzureResourceManagerUtil.ServicePrincipalHasReadAccessToSubscription(subscription.Id, org.Id);
                                subscription.UsageString = AzureResourceManagerUtil.GetUsage(s.Id, org.Id);
                                //Deserialize the usage response into the usagePayload object
                                subscription.usagePayload = JsonConvert.DeserializeObject<UsagePayload>(subscription.UsageString);
                                List<UsageAggregate> UsageAggregateList = subscription.usagePayload.value;

                                
                            }
                            else 
                            {
                                subscription.IsConnected = false;
                            }
                            model.UserSubscriptions.Add(subscription.Id, subscription);

                            if (AzureResourceManagerUtil.UserCanManageAccessForSubscription(subscription.Id, org.Id))
                                model.UserCanManageAccessForSubscriptions.Add(subscription.Id);
                        }
                    }
                    else
                        model.DisconnectedUserOrganizations.Add(org.Id);
                }
            }
            return View(model);
        }

        public ActionResult ResourcerAllBySubscription(string subscriptionId, string organizationId)
        {
            List<Resourcer> ResourcerList = new List<Resourcer>();
            if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
            {
                var resourcerString = AzureResourceManagerUtil.GetResourcerAllBySubscription(subscriptionId, organizationId);
                ResourcerLoad resourcerLoad = JsonConvert.DeserializeObject<ResourcerLoad>(resourcerString);
                ResourcerList = resourcerLoad.value;

            }
            return View(ResourcerList);
        }

        public ActionResult ResourcerGroup(string subscriptionId, string organizationId)
        {
            string resoucerGroup = "RGPRODEUS2IGNCOREINFRA001";
            List<Resourcer> ResourcerList = new List<Resourcer>();
            if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
            {
                var resourcerString = AzureResourceManagerUtil.GetResourcersByResourcerGroup(subscriptionId, organizationId, resoucerGroup);
                ResourcerLoad resourcerLoad = JsonConvert.DeserializeObject<ResourcerLoad>(resourcerString);
                ResourcerList = resourcerLoad.value;

            }
            return View(ResourcerList);
        }

        public ActionResult Group(string subscriptionId, string organizationId)
        {
            
            List<Resourcer> ResourcerList = new List<Resourcer>();
            if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
            {
                var resourcerString = AzureResourceManagerUtil.GetResourceGroups(subscriptionId, organizationId);
                ResourcerLoad resourcerLoad = JsonConvert.DeserializeObject<ResourcerLoad>(resourcerString);
                ResourcerList = resourcerLoad.value;

            }
            return View(ResourcerList);
        }

        public ActionResult MetricsResource(string organizationId, string resourceUri)
        {
            //string resoucerGroup = "Default-Ignite-Test";
            List<Resourcer> ResourcerList = new List<Resourcer>();
            if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
            {
                //var resourcerString = AzureResourceManagerUtil.GetMetricsResource(organizationId, resourceUri);
                //ResourcerLoad resourcerLoad = JsonConvert.DeserializeObject<ResourcerLoad>(resourcerString);
                //ResourcerList = resourcerLoad.value;

            }
            return View(ResourcerList);
        }

        public ActionResult GetSubscriptionQueryGrouping(string subscriptionId, string organizationId)
        {
            //string resoucerGroup = "Default-Ignite-Test";
            CostManagement costManagement = new CostManagement();
            if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
            {
                var costString = AzureResourceManagerUtil.GetSubscriptionQueryGrouping(subscriptionId, organizationId);
                CostManagement costLoad = JsonConvert.DeserializeObject<CostManagement>(costString);
                costManagement = costLoad;

            }
            return View(costManagement);
        }

        public ActionResult GetResourceGroupQueryGrouping(string subscriptionId, string organizationId, string resoucerGroup) 
        {
            
            CostManagement costManagement = new CostManagement();
            if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
            {
                var costString = AzureResourceManagerUtil.GetResourceGroupQueryGrouping(subscriptionId, organizationId, resoucerGroup);
                CostManagement costLoad = JsonConvert.DeserializeObject<CostManagement>(costString);
                costManagement = costLoad;

            }
            return View(costManagement);
        }





        public ActionResult Monitor(string organizationId)
        {
            List<Operation> monitorOperationList = new List<Operation>();
            if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
            {
                var monitorOperationString = AzureResourceManagerUtil.GetMonitorOperationsList(organizationId);
                MonitorOperationLoad monitorOperationLoad = JsonConvert.DeserializeObject<MonitorOperationLoad>(monitorOperationString);
                monitorOperationList = monitorOperationLoad.value;

            }
            return View(monitorOperationList);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}