using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderProcessing.Domain.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

        public User()
        {
            Role = "User";
            EmailConfirmed = false;
            Id = "userId1";
        }

        public void GiveAdminRole()
        {
            Role = "Admin";
        }
    }
}
