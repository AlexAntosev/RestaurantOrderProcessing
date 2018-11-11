using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestaurantOrderProcessing.Domain.Entities;

namespace RestaurantOrderProcessing.WebUI.Models
{
    public class OrderIndexViewModel
    {
        public Order Order { get; set; }
        public string ReturnUrl { get; set; }
    }
}