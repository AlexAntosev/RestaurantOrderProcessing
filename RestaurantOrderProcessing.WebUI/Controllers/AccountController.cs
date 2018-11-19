using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantOrderProcessing.Domain.Abstract;
using RestaurantOrderProcessing.Domain.Concrete;
using RestaurantOrderProcessing.Domain.Entities;
using RestaurantOrderProcessing.WebUI.Models;

namespace RestaurantOrderProcessing.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider;
        IUserRepository repository;

        public AccountController(IAuthProvider provider, IUserRepository repo)
        {
            authProvider = provider;
            repository = repo;
        }

        [HttpGet]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                if(authProvider.Authenticate(model.Username, model.Password))
                {
                    return Redirect(Url.Action("Index", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Wrong username or password");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ViewResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                User user = new User() { Email = model.Email, PasswordHash = model.Password, UserName = model.Email};
                authProvider.Authenticate(user.Email, user.PasswordHash);
                repository.CreateUser(user);
                TempData["message"] = string.Format("You create new account as \"{0}\". You will receive a message on your email with confirm", user.Email);
                return Redirect(Url.Action("List", "Dish"));
            }
            else
            {
                return View();
            }
        }
    }
}