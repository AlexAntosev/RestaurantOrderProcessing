using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using RestaurantOrderProcessing.Domain.Concrete;
using RestaurantOrderProcessing.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantOrderProcessing.WebUI.Models
{
    public class ApplicationRoleManager : RoleManager<UserRole>
    {
        public ApplicationRoleManager(RoleStore<UserRole> store) : base(store) { }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            UsersDbContext db = context.Get<UsersDbContext>();
            ApplicationRoleManager manager = new ApplicationRoleManager(new RoleStore<UserRole>(db));
            return manager;
        }
    }
}