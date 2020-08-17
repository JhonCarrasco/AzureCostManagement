using ISVDemoUsage.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISVDemoUsage.Forms
{
    public class SubscriptionForm
    {
        public string SubscriptionId { get; set; }
        public string ResourceGroupId { get; set; }
        public string ResourceId { get; set; }
        public string DefinitionId { get; set; } 
        public virtual SubscriptionControl subscriptionControl { get; set; }
        public List<SelectListItem> ListadoResourceGroup { get; set; }
        public List<SelectListItem> ListadoResource { get; set; }
        public List<SelectListItem> ListadoDefinitions { get; set; } 
    } 
    public class SubscriptionFormFactory
    {
        public static SubscriptionForm GetInitializing()
        {
            SubscriptionForm form = new SubscriptionForm();
            return LoadDLL(form);
        }

        public static SubscriptionForm LoadDLL(SubscriptionForm form)
        {
            form.subscriptionControl = SubscriptionControl.Instancia;
            form.ListadoResourceGroup = new List<SelectListItem>();
            form.ListadoResource = new List<SelectListItem>();
            form.ListadoDefinitions = new List<SelectListItem>();
            return form;
        }
    }
}