using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantOrderProcessing.Domain.Entities;
using RestaurantOrderProcessing.Domain.Abstract;


namespace RestaurantOrderProcessing.Domain.Concrete
{
    public class EFDishRepository : IDishRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Dish> Dishes
        {
            get { return context.Dishes; }
        }

        public void SaveDish(Dish dish)
        {
            if (dish.DishId == 0)
                context.Dishes.Add(dish);
            else
            {
                Dish dbEntry = context.Dishes.Find(dish.DishId);
                if (dbEntry != null)
                {
                    dbEntry.Name = dish.Name;
                    dbEntry.Weight = dish.Weight;
                    dbEntry.Price = dish.Price;
                    dbEntry.Category = dish.Category;
                    dbEntry.Time = dish.Time;
                    dbEntry.ImageData = dish.ImageData;
                    dbEntry.ImageMimeType = dish.ImageMimeType;
                    dbEntry.Description = dish.Description;
                }
            }
            context.SaveChanges();
        }

        public Dish DeleteDish(int dishId)
        {
            Dish dbEntry = context.Dishes.Find(dishId);
            if(dbEntry != null)
            {
                context.Dishes.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
