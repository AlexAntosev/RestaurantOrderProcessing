using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantOrderProcessing.Domain.Entities;

namespace RestaurantOrderProcessing.WebUI.Infrastructure.Binders
{
    public class OrderModelBinder : IModelBinder
    {
        private const string SESSION_KEY = "Order";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Order order = null;
            if(controllerContext.HttpContext.Session != null)
            {
                order = (Order)controllerContext.HttpContext.Session[SESSION_KEY];
            }

            if(order == null)
            {
                order = new Order();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[SESSION_KEY] = order;
                }                
            }

            return order;
        }
    }
}