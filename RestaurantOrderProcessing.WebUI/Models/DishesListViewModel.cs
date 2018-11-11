using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestaurantOrderProcessing.Domain.Entities;

namespace RestaurantOrderProcessing.WebUI.Models
{
    public class DishesListViewModel
    {
        public IEnumerable<Dish> Dishes { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}