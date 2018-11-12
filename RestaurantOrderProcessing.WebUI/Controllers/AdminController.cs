using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantOrderProcessing.Domain.Abstract;
using RestaurantOrderProcessing.Domain.Entities;

namespace RestaurantOrderProcessing.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IDishRepository repository;

        public AdminController(IDishRepository repo)
        {
            repository = repo;
        }
        
        public ActionResult Index()
        {
            return View(repository.Dishes);
        }

        public ViewResult Edit(int dishId)
        {
            Dish dish = repository.Dishes.FirstOrDefault(d => d.DishId == dishId);
            return View(dish);
        }

        [HttpPost]
        public ActionResult Edit(Dish dish, HttpPostedFileBase image = null)
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
                return RedirectToAction("Index");
            }
            else
            {
                // smth wrong with editing data
                return View(dish);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Dish());
        }

        [HttpPost]
        public ActionResult Delete(int dishId)
        {
            Dish dish = repository.DeleteDish(dishId);
            if(dish != null)
            {
                TempData["message"] = string.Format("Editing in dish \"{0}\" was deleted", dish.Name);
            }
            return RedirectToAction("Index");
        }
    }
}