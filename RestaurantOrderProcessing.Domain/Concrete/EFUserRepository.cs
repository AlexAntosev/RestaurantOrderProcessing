using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantOrderProcessing.Domain.Abstract;
using RestaurantOrderProcessing.Domain.Entities;

namespace RestaurantOrderProcessing.Domain.Concrete
{
    public class EFUserRepository : IUserRepository
    {
        UsersDbContext context = new UsersDbContext();

        public IEnumerable<User> Users
        {
            get { return context.Users; }
        }
    }
}
