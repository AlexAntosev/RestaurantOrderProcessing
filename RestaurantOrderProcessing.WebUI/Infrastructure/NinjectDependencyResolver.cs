using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Mvc;
using Moq;
using Ninject;
using RestaurantOrderProcessing.Domain.Abstract;
using RestaurantOrderProcessing.Domain.Entities;
using RestaurantOrderProcessing.Domain.Concrete;

namespace RestaurantOrderProcessing.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            //Mock<IDishRepository> mock = new Mock<IDishRepository>();
            //mock.Setup(m => m.Dishes).Returns(new List<Dish>
            //{
            //    new Dish { Name = "Pizza", Price = 50 },
            //    new Dish { Name = "Tea", Price = 10 },
            //    new Dish { Name = "Meat", Price = 120 }
            //});
            //kernel.Bind<IDishRepository>().ToConstant(mock.Object);
            kernel.Bind<IDishRepository>().To<EFDishRepository>();
        }
    }
}
