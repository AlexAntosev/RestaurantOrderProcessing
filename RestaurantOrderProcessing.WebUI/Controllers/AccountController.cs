using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantOrderProcessing.Domain.Abstract;
using RestaurantOrderProcessing.Domain.Concrete;
using RestaurantOrderProcessing.Domain.Entities;
using RestaurantOrderProcessing.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using System.Security.Claims;

namespace RestaurantOrderProcessing.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider;
        IUserRepository repository;

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public AccountController(IAuthProvider provider, IUserRepository repo)
        {
            authProvider = provider;
            repository = repo;
        }               

        [HttpGet]
        public ViewResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                User user = await UserManager.FindAsync(model.Email, model.Password);
                if(user == null)
                {
                    ModelState.AddModelError("", "Wrong username or password");
                    return View();
                }
                else
                {
                    
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if(UserManager.IsInRole(user.Id, "admin"))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    if (String.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("List", "Dish");
                    return Redirect(returnUrl);
                }
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = new User() { UserName = model.Email, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, model.AsAdmin ? "admin" : "user");
                    //repository.CreateUser(user);
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }     
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> DeleteConfirm()
        {
            User user = await UserManager.FindByEmailAsync(User.Identity.Name);
            if(user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if(result.Succeeded)
                {
                    return RedirectToAction("Logout");
                }
            }
            return RedirectToAction("List", "Dish");
        }
    }
}