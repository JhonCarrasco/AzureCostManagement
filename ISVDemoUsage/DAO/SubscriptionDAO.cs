using ISVDemoUsage.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace ISVDemoUsage.DAO
{
    public class SubscriptionDAO
    {
        private static DataAccess db = new DataAccess();
        public static List<Subscription> GetSubscriptionsUser()
        {
            List<Subscription> listado = new List<Subscription>();
            
            if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
            {
                var orgnaizations = AzureResourceManagerUtil.GetUserOrganizations();
                                
                foreach (Organization org in orgnaizations)
                {
                    List<Subscription> subscriptions = AzureResourceManagerUtil.GetUserSubscriptions(org.Id);

                    if (subscriptions != null)
                    {
                        foreach (var subscription in subscriptions)
                        {
                            listado.Add(subscription);                            
                        }
                    }                    
                }
            }
            return listado;
        }
        public static List<Resourcer> GetResourcerGroups(string subscriptionId, string organizationId)
        {
            List<Resourcer> Listado = new List<Resourcer>();
            if (ClaimsPrincipal.Current.Identity.IsAuthenticated)
            {
                var resourcerString = AzureResourceManagerUtil.GetResourceGroups(subscriptionId, organizationId);
                ResourcerLoad resourcerLoad = JsonConvert.DeserializeObject<ResourcerLoad>(resourcerString);
                Listado = resourcerLoad.value;

            }
            return Listado;
        }
    }
}