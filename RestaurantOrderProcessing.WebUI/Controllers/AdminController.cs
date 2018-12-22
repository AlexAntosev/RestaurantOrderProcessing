using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantOrderProcessing.Domain.Abstract;
using RestaurantOrderProcessing.Domain.Concrete;
using RestaurantOrderProcessing.Domain.Entities;

namespace RestaurantOrderProcessing.WebUI.Controllers
{
    [Authorize(Roles= "admin")]
    public class AdminController : Controller
    {
        IDishRepository repository;

        public AdminController(IDishRepository repo)
        {
            repository = repo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DishesSummary()
        {
            return View(repository.Dishes);
        }

        public ViewResult EditDish(int dishId)
        {
            Dish dish = repository.Dishes.FirstOrDefault(d => d.DishId == dishId);
            return View(dish);
        }

        [HttpPost]
        public ActionResult EditDish(Dish dish, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    dish.ImageMimeType = image.ContentType;
                    dish.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(dish.ImageData, 0, image.ContentLength);
                }
                repository.SaveDish(dish);
                TempData["message"] = string.Format("Editing in dish \"{0}\" was saved", dish.Name);
                return RedirectToAction("DishesSummary");
            }
            else
            {
                // smth wrong with editing data
                return View(dish);
            }
        }

        public ViewResult Create()
        {
            return View("EditDish", new Dish());
        }

        [HttpPost]
        public ActionResult Delete(int dishId)
        {
            Dish dish = repository.DeleteDish(dishId);
            if(dish != null)
            {
                TempData["message"] = string.Format("Editing in dish \"{0}\" was deleted", dish.Name);
            }
            return RedirectToAction("DishesSummary");
        }

        public ViewResult OrdersSummary()
        {
           // ViewBag.Table = 
            return View(OrderProcessor.ClientRequests);
        }
    }
}