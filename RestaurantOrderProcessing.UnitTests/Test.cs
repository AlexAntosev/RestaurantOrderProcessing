using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RestaurantOrderProcessing.Domain.Abstract;
using RestaurantOrderProcessing.Domain.Entities;
using RestaurantOrderProcessing.WebUI.Controllers;
using System.Web.Mvc;
using RestaurantOrderProcessing.WebUI.HtmlHelpers;
using RestaurantOrderProcessing.WebUI.Models;

namespace RestaurantOrderProcessing.UnitTests
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Arrange
            Mock<IDishRepository> mock = new Mock<IDishRepository>();
            mock.Setup(m => m.Dishes).Returns(new List<Dish>
            {
                new Dish { DishId = 1, Name = "Dish1"},
                new Dish { DishId = 2, Name = "Dish2"},
                new Dish { DishId = 3, Name = "Dish3"},
                new Dish { DishId = 4, Name = "Dish4"},
                new Dish { DishId = 5, Name = "Dish5"}
            });
            DishController controller = new DishController(mock.Object);
            controller.pageSize = 3;

            // Act
            DishesListViewModel result = (DishesListViewModel)controller.List(null, 2).Model;

            // Asserts
            List<Dish> dishes = result.Dishes.ToList();
            Assert.IsTrue(dishes.Count == 2);
            Assert.AreEqual(dishes[0].Name, "Dish4");
            Assert.AreEqual(dishes[1].Name, "Dish5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            //Arrange
            HtmlHelper myHelper = null;

            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // Act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // Asserts
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange
            Mock<IDishRepository> mock = new Mock<IDishRepository>();
            mock.Setup(m => m.Dishes).Returns(new List<Dish>
            {
                new Dish { DishId = 1, Name = "Dish1"},
                new Dish { DishId = 2, Name = "Dish2"},
                new Dish { DishId = 3, Name = "Dish3"},
                new Dish { DishId = 4, Name = "Dish4"},
                new Dish { DishId = 5, Name = "Dish5"}
            });
            DishController controller = new DishController(mock.Object);
            controller.pageSize = 3;

            // Act
            DishesListViewModel result = (DishesListViewModel)controller.List(null, 2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.AreEqual(pageInfo.CurrentPage, 2);
            Assert.AreEqual(pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(pageInfo.TotalItems, 5);
            Assert.AreEqual(pageInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Category()
        {
            //Arrange
            Mock<IDishRepository> mock = new Mock<IDishRepository>();
            mock.Setup(m => m.Dishes).Returns(new List<Dish>
            {
                new Dish { DishId = 1, Name = "Dish1", Category="Drink"},
                new Dish { DishId = 2, Name = "Dish2", Category="Food"},
                new Dish { DishId = 3, Name = "Dish3", Category="Food"},
                new Dish { DishId = 4, Name = "Dish4", Category="Drink"},
                new Dish { DishId = 5, Name = "Dish5", Category="Food"}
            });

            DishController controler = new DishController(mock.Object);

            //Act
            DishesListViewModel result = (DishesListViewModel)controler.List("Drink").Model;

            //Assert
            List<Dish> dishes = result.Dishes.ToList();
            Assert.AreEqual(result.CurrentCategory, "Drink");
            Assert.AreEqual(dishes.Count, 2);
            Assert.IsTrue(dishes[0].Name == "Dish1" && dishes[0].Category == "Drink");
            Assert.IsTrue(dishes[1].Name == "Dish4" && dishes[1].Category == "Drink");
        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            //Arrange
            Mock<IDishRepository> mock = new Mock<IDishRepository>();
            mock.Setup(m => m.Dishes).Returns(new List<Dish> {
                new Dish { DishId = 1, Name = "Dish1", Category="Food"},
                new Dish { DishId = 2, Name = "Dish2", Category="Drink"},
                new Dish { DishId = 3, Name = "Dish3", Category="Smth"},
                new Dish { DishId = 4, Name = "Dish4", Category="Food"},
             });

            NavController target = new NavController(mock.Object);

            //Act
            List<string> results = ((IEnumerable<string>)target.Menu().Model).ToList();

            //Assert
            Assert.AreEqual(results.Count(), 3);
            Assert.AreEqual(results[0], "Drink");
            Assert.AreEqual(results[1], "Food");
            Assert.AreEqual(results[2], "Smth");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            //Arrange
            Mock<IDishRepository> mock = new Mock<IDishRepository>();
            mock.Setup(m => m.Dishes).Returns(new Dish[] {
                new Dish { DishId = 1, Name = "Dish1", Category="Food"},
                new Dish { DishId = 2, Name = "Dish2", Category="Drink"}
            });

            NavController target = new NavController(mock.Object);


            string categoryToSelect = "Drink";

            //Act
            string result = target.Menu(categoryToSelect).ViewBag.SelectedCategory;

            //Assert
            Assert.AreEqual(categoryToSelect, result);
        }

        [TestMethod]
        public void Generate_Category_Specific_Game_Count()
        {
            //Arrange
            Mock<IDishRepository> mock = new Mock<IDishRepository>();
            mock.Setup(m => m.Dishes).Returns(new List<Dish>
            {
                new Dish { DishId = 1, Name = "Dish1", Category="Drink"},
                new Dish { DishId = 2, Name = "Dish2", Category="Food"},
                new Dish { DishId = 3, Name = "Dish3", Category="Food"},
                new Dish { DishId = 4, Name = "Dish4", Category="Drink"},
                new Dish { DishId = 5, Name = "Dish5", Category="Food"}
            });
            DishController controler = new DishController(mock.Object);

            //Act
            int res1 = ((DishesListViewModel)controler.List("Drink").Model).PagingInfo.TotalItems;
            int res2 = ((DishesListViewModel)controler.List("Food").Model).PagingInfo.TotalItems;
            int resAll = ((DishesListViewModel)controler.List(null).Model).PagingInfo.TotalItems;

            //Assert
            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 3);
            Assert.AreEqual(resAll, 5);
        }

        [TestMethod]
        public void Add_New_Line_To_Order()
        {
            //Arrange
            Dish dish1 = new Dish { DishId = 1, Name = "Dish1", Price = 10 };
            Dish dish2 = new Dish { DishId = 2, Name = "Dish2", Price = 20 };
            Dish dish3 = new Dish { DishId = 3, Name = "Dish3", Price = 30 };

            Order target = new Order();

            //Act
            target.AddDish(dish1, 2);
            target.AddDish(dish3, 1);
            target.AddDish(dish2, 3);
            List<OrderLine> result = target.Lines.ToList();
            //Asserts
            Assert.AreEqual(result.Count(), 3);
            Assert.AreEqual(result[0].Dish, dish1);
            Assert.AreEqual(result[1].Dish, dish3);
            Assert.AreEqual(result[2].Dish, dish2);
            Assert.AreEqual(result[0].Quantity, 2);
            Assert.AreEqual(result[1].Quantity, 1);
            Assert.AreEqual(result[2].Quantity, 3);
        }

        [TestMethod]
        public void Can_Add_Quantity_To_Existing_Line()
        {
            //Arrange
            Dish dish1 = new Dish { DishId = 1, Name = "Dish1", Price = 10 };
            Dish dish2 = new Dish { DishId = 2, Name = "Dish2", Price = 20 };
            Dish dish3 = new Dish { DishId = 3, Name = "Dish3", Price = 30 };

            Order order = new Order();

            //Act
            order.AddDish(dish1, 1);
            order.AddDish(dish2, 3);
            order.AddDish(dish1, 3);
            List<OrderLine> result = order.Lines.OrderBy(d => d.Dish.DishId).ToList();

            //Asserts
            Assert.AreEqual(result.Count(), 2);
            Assert.AreEqual(result[0].Quantity, 4);
            Assert.AreEqual(result[1].Quantity, 3);
        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            //Arrange
            Dish dish1 = new Dish { DishId = 1, Name = "Dish1", Price = 10 };
            Dish dish2 = new Dish { DishId = 2, Name = "Dish2", Price = 20 };
            Dish dish3 = new Dish { DishId = 3, Name = "Dish3", Price = 30 };

            Order order = new Order();

            order.AddDish(dish1, 1);
            order.AddDish(dish2, 2);
            order.AddDish(dish3, 3);

            //Act
            order.RemoveDish(dish2);
            List<OrderLine> result = order.Lines.ToList();

            //Asserts
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(order.Lines.Where(d => d.Dish == dish2).Count() == 0);
        }

        [TestMethod]
        public void Can_Compute_Total_Value()
        {
            //Arrange
            Dish dish1 = new Dish { DishId = 1, Name = "Dish1", Price = 10 };
            Dish dish2 = new Dish { DishId = 2, Name = "Dish2", Price = 20 };
            Dish dish3 = new Dish { DishId = 3, Name = "Dish3", Price = 30 };

            Order order = new Order();

            //Act
            order.AddDish(dish1, 1);
            order.AddDish(dish2, 2);
            order.AddDish(dish3, 3);
            decimal result = order.ComputeTotalValue();

            //Asserts
            Assert.AreEqual(result, 140);
        }

        [TestMethod]
        public void Can_Clear_Order()
        {
            //Arrange
            Dish dish1 = new Dish { DishId = 1, Name = "Dish1", Price = 10 };
            Dish dish2 = new Dish { DishId = 2, Name = "Dish2", Price = 20 };
            Dish dish3 = new Dish { DishId = 3, Name = "Dish3", Price = 30 };

            Order order = new Order();


            //Act
            order.AddDish(dish1, 1);
            order.AddDish(dish2, 2);
            order.AddDish(dish3, 3);
            order.Clear();

            //Asserts
            Assert.AreEqual(order.Lines.Count(), 0);
        }

        [TestMethod]
        public void Can_Add_To_Order()
        {
            //Arrange
            Mock<IDishRepository> mock = new Mock<IDishRepository>();
            mock.Setup(d => d.Dishes).Returns(new List<Dish>
            {
                new Dish { DishId = 1, Name = "Dish1"},
                new Dish { DishId = 2, Name = "Dish2"},
                new Dish { DishId = 3, Name = "Dish3"}
            });

            OrderController orderController = new OrderController(mock.Object,null);
            Order order = new Order();

            //Act
            orderController.AddToOrder(order, 2, null);

            //Asserts
            Assert.AreEqual(order.Lines.Count(), 1);
            Assert.AreEqual(order.Lines.ToList()[0].Dish.DishId, 2);
        }

        [TestMethod]
        public void Can_Goes_To_Order_Screen_After_Adding_Order()
        {
            //Arrange
            Mock<IDishRepository> mock = new Mock<IDishRepository>();
            mock.Setup(d => d.Dishes).Returns(new List<Dish>
            {
                new Dish { DishId = 1, Name = "Dish1"},
                new Dish { DishId = 2, Name = "Dish2"},
                new Dish { DishId = 3, Name = "Dish3"}
            });

            OrderController orderController = new OrderController(mock.Object, null);
            Order order = new Order();
            string returnUrl = "Url";

            //Act
            RedirectToRouteResult result = orderController.AddToOrder(order, 2, returnUrl);

            //Asserts
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "Url");
        }

        [TestMethod]
        public void Order_Is_Not_Empty()
        {
            //Arrange
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Order order = new Order();
            ShippingDetails shippingDetails = new ShippingDetails();
            OrderController controller = new OrderController(null, mock.Object);

            //Act
            ViewResult result = controller.CheckoutOrder(order, shippingDetails);

            //Asserts
            mock.Verify(m => m.OrderProcess(It.IsAny<Order>(), It.IsAny<ShippingDetails>()), Times.Never());
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            // Arrange
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            Order order = new Order();
            order.AddDish(new Dish(), 1);

            OrderController controller = new OrderController(null, mock.Object);
            
            controller.ModelState.AddModelError("error", "error");

            //Act
            ViewResult result = controller.CheckoutOrder(order, new ShippingDetails());

            // Asserts
            mock.Verify(m => m.OrderProcess(It.IsAny<Order>(), It.IsAny<ShippingDetails>()), Times.Never());

            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Can_Checkout_And_Submit_Order()
        {
            // Arrange
            Mock<IOrderProcessor> mock = new Mock<IOrderProcessor>();
            
            Order order = new Order();
            order.AddDish(new Dish(), 1);

            OrderController controller = new OrderController(null, mock.Object);

            // Act
            ViewResult result = controller.CheckoutOrder(order, new ShippingDetails());

            // Assets
            mock.Verify(m => m.OrderProcess(It.IsAny<Order>(), It.IsAny<ShippingDetails>()), Times.Once());
            
            Assert.AreEqual("Completed", result.ViewName);
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Can_Register()
        {
            //Arrange
            Mock<IAuthProvider> mockProvider = new Mock<IAuthProvider>();
            Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
            AccountController controler = new AccountController(mockProvider.Object, mockRepository.Object);
            User user = new User();
            user.Email = "email@email.com";
            user.PasswordHash = "GA3a3saFsa";
            //Act
            controler.Register(user);
            //Asserts
        }
    }
}
