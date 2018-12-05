using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantOrderProcessing.Domain.Abstract;
using RestaurantOrderProcessing.Domain.Entities;

namespace RestaurantOrderProcessing.Domain.Concrete
{
    public class OrderProcessor : IOrderProcessor
    {
        public void OrderProcess(Order order, ShippingDetails shippingDetails)
        {
            order.Timer.Start();
        }
    }
}
