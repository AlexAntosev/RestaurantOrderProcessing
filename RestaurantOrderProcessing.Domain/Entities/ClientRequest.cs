using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderProcessing.Domain.Entities
{
    public class ClientRequest
    {
        public Order Order { get; set; }
        public ShippingDetails ShippingDetails { get; set; }
    }
}
