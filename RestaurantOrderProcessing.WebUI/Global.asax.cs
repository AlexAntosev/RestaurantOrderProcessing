using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RestaurantOrderProcessing.WebUI.Infrastructure.Binders;
using RestaurantOrderProcessing.Domain.Entities;

namespace RestaurantOrderProcessing.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(Order), new OrderModelBinder());
        }
    }
}
