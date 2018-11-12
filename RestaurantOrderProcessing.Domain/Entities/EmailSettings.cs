using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderProcessing.Domain.Entities
{
    public class EmailSettings
    {
        public string MailToAddress = "restaurant@example.com";
        public string MailFromAddress = "client@example.com";
        public bool UseSsl = true;
        public string Username = "Username";
        public string Password = "Password";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"c:\restaurant_orders_emails";
    }
}
