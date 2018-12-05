using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RestaurantOrderProcessing.Domain.Entities
{
    public class Order
    {
        public Timer Timer = new Timer();
        public List<OrderLine> LineCollection = new List<OrderLine>();
        
        public void AddDish(Dish dish, int quantity)
        {
            var line = LineCollection
                .Where(d => d.Dish.DishId == dish.DishId)
                .FirstOrDefault();

            if (line == null)
            {
                LineCollection.Add(new OrderLine
                {
                    Dish = dish,
                    Quantity = quantity
                });
                
            }
            else
            {
                line.Quantity += quantity;
            }

            Timer.Interval += dish.Time * 1000;
        }

        public void RemoveDish(Dish dish)
        {
            LineCollection.RemoveAll(d => d.Dish.DishId == dish.DishId);
        }

        public decimal ComputeTotalValue()
        {
            return LineCollection.Sum(d => d.Dish.Price * d.Quantity);
        }

        public void Clear()
        {
            LineCollection.Clear();
        }

        public IEnumerable<OrderLine> Lines
        {
            get { return LineCollection; }
        }

        public int CountDishTime(Dish dish, int quantity)
        {
            return dish.Time * quantity;
        }
    }
    
}
