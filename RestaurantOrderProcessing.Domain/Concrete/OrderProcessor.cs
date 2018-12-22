using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using RestaurantOrderProcessing.Domain.Abstract;
using RestaurantOrderProcessing.Domain.Entities;

namespace RestaurantOrderProcessing.Domain.Concrete
{
    public static class OrderProcessor
    {
        public static List<ClientRequest> ClientRequests { get; set; }
        

        static OrderProcessor()
        {
            ClientRequests = new List<ClientRequest>();
        }

        public static void OrderProcess(Order order, ShippingDetails shippingDetails)
        {
            order.Submited = true;
            ClientRequests.Add(new ClientRequest { Order = order, ShippingDetails = shippingDetails });
            
            order.Timer = new Timer(60000);
            order.Timer.Enabled = true;
            order.Timer.Elapsed += (object sender, System.Timers.ElapsedEventArgs e) => order.TimeLeft--;
            order.Timer.Start();
        }

    }
}
