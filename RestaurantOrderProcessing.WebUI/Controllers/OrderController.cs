﻿using System;
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
    public class OrderController : Controller
    {
        IDishRepository repository;

        public OrderController(IDishRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(Order order, string returnUrl)
        {
            return View(new OrderIndexViewModel
            {
                Order = order,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToOrder(Order order, int dishId, string returnUrl)
        {
            Dish dish = repository.Dishes.FirstOrDefault(d => d.DishId == dishId);

            if(dish != null)
                order.AddDish(dish, 1);

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromOrder(Order order, int dishId, string returnUrl)
        {
            Dish dish = repository.Dishes.FirstOrDefault(d => d.DishId == dishId);

            if(dish != null)
                order.RemoveDish(dish);

            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Order order)
        {
            return PartialView(order);
        }
        
        public ViewResult CheckoutOrder()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        [Authorize]
        public ViewResult CheckoutOrder(Order order, ShippingDetails shippingDetails)
        {
            if (order.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Ваш заказ пуст");
            }
            if (ModelState.IsValid)
            {
                OrderProcessor.OrderProcess(order, shippingDetails);
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}