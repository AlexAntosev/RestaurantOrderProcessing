using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderProcessing.Domain.Entities
{
    public class User : IdentityUser
    {
        public User()
        {
            //Role = "User";
            //EmailConfirmed = false;
            //Id = "userId1";
        }
    }
}
