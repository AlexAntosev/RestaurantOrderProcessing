using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantOrderProcessing.Domain.Abstract;
using RestaurantOrderProcessing.Domain.Entities;
using RestaurantOrderProcessing.WebUI.Models;

namespace RestaurantOrderProcessing.WebUI.Controllers
{
    public class DishController : Controller
    {
        private IDishRepository repository;
        public int pageSize = 4;

        public DishController(IDishRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page = 1)
        {
            DishesListViewModel model = new DishesListViewModel
            {
                Dishes = repository.Dishes
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(dish => dish.DishId)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ? repository.Dishes.Count() : repository.Dishes.Where(dish => dish.Category == category).Count()
                },
                CurrentCategory = category
                
            };
            return View(model);
        }
    }
}