using ISVDemoUsage.DAO;
using ISVDemoUsage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISVDemoUsage.Controls
{
    public class SubscriptionControl
    {
        public List<Subscription> _listado = null;
        private static SubscriptionControl instancia;
        public List<SelectListItem> Listado { get; set; }
        public static SubscriptionControl Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new SubscriptionControl();
                }
                return instancia;
            }
        }
        public SubscriptionControl()
        {
            LoadList();
        }
        public void LoadList()
        {
            Listado = new List<SelectListItem>();
            _listado = SubscriptionDAO.GetSubscriptionsUser();

            foreach(Subscription item in _listado)
            {
                Listado.Add(new SelectListItem
                {
                    Text = item.DisplayName,
                    Value = item.Id
                });
            }
        }
    }
}