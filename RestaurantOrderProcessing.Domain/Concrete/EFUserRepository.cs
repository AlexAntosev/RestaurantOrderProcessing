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
        EFDbContext context = new EFDbContext();

        public IEnumerable<User> Users
        {
            get { return context.Users; }
        }

        public void CreateUser(User user)
        {
            //User dbEntry = context.Users.Find(user.UserId);
            //if(dbEntry == null)
            //{
                context.Users.Add(user);
            //}            
            context.SaveChanges();
        }

        public User DeleteUser(int userId)
        {
            User dbEntry = context.Users.Find(userId);
            if(dbEntry != null)
            {
                context.Users.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
