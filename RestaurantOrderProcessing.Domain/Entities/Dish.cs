using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderProcessing.Domain.Entities
{
    public class Dish
    {
        public int DishId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public int Time { get; set; }
    }
}
