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
    }
}
