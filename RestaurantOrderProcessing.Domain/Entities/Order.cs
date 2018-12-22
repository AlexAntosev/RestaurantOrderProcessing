using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RestaurantOrderProcessing.Domain.Entities
{
    public class Order : ICloneable
    {
        public Timer Timer = new Timer();
        public List<OrderLine> LineCollection = new List<OrderLine>();
        public bool Submited { get; set; } = false;
        public int TimeLeft { get; set; } = 0;
        
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
                TimeLeft += dish.Time * quantity;
            }
            else
            {
                line.Quantity += quantity;
            }
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

        public object Clone()
        {
            List<OrderLine> lineCollection = new List<OrderLine>();
            foreach (var line in LineCollection)
            {
                lineCollection.Add((OrderLine)line.Clone());
            }
            return new Order { LineCollection = lineCollection, Submited = this.Submited, Timer = this.Timer };
        }
    }
    
}
