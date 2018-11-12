using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantOrderProcessing.Domain.Entities;
using RestaurantOrderProcessing.Domain.Abstract;
using Moq;
using System.Collections.Generic;
using System.Linq;
using RestaurantOrderProcessing.WebUI.Controllers;
using System.Web.Mvc;

namespace RestaurantOrderProcessing.UnitTests
{
    [TestClass]
    public class ImageTests
    {
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            // Arrange
            Dish dish = new Dish
            {
                DishId = 2,
                Name = "dish2",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };
            
            Mock<IDishRepository> mock = new Mock<IDishRepository>();
            mock.Setup(m => m.Dishes).Returns(new List<Dish>
            {
                new Dish {DishId = 1, Name = "dish1"},
                dish,
                new Dish {DishId = 3, Name = "dish2"}
            }.AsQueryable());

            DishController controller = new DishController(mock.Object);

            // Act
            ActionResult result = controller.GetImage(2);

            // Asserts
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(dish.ImageMimeType, ((FileResult)result).ContentType);
        }

        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {
            // Arrange
            Mock<IDishRepository> mock = new Mock<IDishRepository>();
            mock.Setup(m => m.Dishes).Returns(new List<Dish> {
                new Dish {DishId = 1, Name = "dish1"},
                new Dish {DishId = 2, Name = "dish2"}
            }.AsQueryable());
            
            DishController controller = new DishController(mock.Object);

            // Act
            ActionResult result = controller.GetImage(10);

            // Asserts
            Assert.IsNull(result);
        }
    }
}
