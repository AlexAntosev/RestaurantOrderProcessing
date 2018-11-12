using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantOrderProcessing.Domain.Entities;

namespace RestaurantOrderProcessing.Domain.Abstract
{
    public interface IOrderProcessor
    {
        void OrderProcess(Order order, ShippingDetails shippingDetails);
    }
}
