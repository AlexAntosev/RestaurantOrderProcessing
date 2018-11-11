using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderProcessing.Domain.Entities
{
    public class Order
    {
        public List<OrderLine> lineCollection = new List<OrderLine>();

        public void AddDish(Dish dish, int quantity)
        {
            OrderLine line = lineCollection
                .Where(d => d.Dish.DishId == dish.DishId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new OrderLine
                {
                    Dish = dish,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveDish(Dish dish)
        {
            lineCollection.RemoveAll(d => d.Dish.DishId == dish.DishId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(d => d.Dish.Price * d.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<OrderLine> Lines
        {
            get { return lineCollection; }
        }
    }
    
}
