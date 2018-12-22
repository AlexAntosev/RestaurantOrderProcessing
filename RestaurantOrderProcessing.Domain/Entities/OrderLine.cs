using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderProcessing.Domain.Entities
{
    public class OrderLine : ICloneable
    {
        public Dish Dish { get; set; }
        public int Quantity { get; set; }

        public object Clone()
        {
            Dish dish = new Dish
            {
                DishId = this.Dish.DishId,
                Name = this.Dish.Name,
                Category = this.Dish.Category,
                Weight = this.Dish.Weight,
                Description = this.Dish.Description,
                Price = this.Dish.Price,
                Time = this.Dish.Time,
                ImageData = this.Dish.ImageData,
                ImageMimeType = this.Dish.ImageMimeType
            };
            return new OrderLine {Dish = dish, Quantity = this.Quantity};
        }
    }
}
