using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;
using RestaurantOrderProcessing.Domain.Entities;
using RestaurantOrderProcessing.Domain.Concrete;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using RestaurantOrderProcessing.WebUI.Models;

[assembly: OwinStartup(typeof(RestaurantOrderProcessing.WebUI.App_Start.Startup))]

namespace RestaurantOrderProcessing.WebUI.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // настраиваем контекст и менеджер
            app.CreatePerOwinContext<UsersDbContext>(UsersDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}