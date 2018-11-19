using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using RestaurantOrderProcessing.Domain.Entities;

namespace RestaurantOrderProcessing.Domain.Concrete
{
    public class UsersDbContext : IdentityDbContext<User>
    {
        public UsersDbContext() : base("IdentityDb") { }

        public static UsersDbContext Create()
        {
            return new UsersDbContext();
        }
    }
}
