using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderProcessing.Domain.Entities
{
    public class UserRole : IdentityRole
    {
        public UserRole() { }

        public string Description { get; set; }
    }
}
