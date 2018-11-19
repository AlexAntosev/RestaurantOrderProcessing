using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantOrderProcessing.Domain.Entities;

namespace RestaurantOrderProcessing.Domain.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }
        void CreateUser(User user);
        User DeleteUser(int userId);
    }
}
