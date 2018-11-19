using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantOrderProcessing.Domain.Entities;
using System.Data.Entity;

namespace RestaurantOrderProcessing.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Dish> Dishes { get; set; }

        public static EFDbContext Create()
        {
            return new EFDbContext();
        }
    }
}
