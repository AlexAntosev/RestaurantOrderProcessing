using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderProcessing.Domain.Entities
{
    public class OrderLine
    {
        public Dish Dish { get; set; }
        public int Quantity { get; set; }
    }
}
