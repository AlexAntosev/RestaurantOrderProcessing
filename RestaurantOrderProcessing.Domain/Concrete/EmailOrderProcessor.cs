using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using RestaurantOrderProcessing.Domain.Abstract;
using RestaurantOrderProcessing.Domain.Entities;

namespace RestaurantOrderProcessing.Domain.Concrete
{
    public class EmailOrderProcessor
    {
        EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void OrderProcess(Order order, ShippingDetails shippingDetails)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("New order was accepted")
                    .AppendLine("---")
                    .AppendLine("Dishes:");

                foreach (var line in order.Lines)
                {
                    var totalPrice = line.Dish.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (total: {2:c}",
                        line.Quantity, line.Dish.Name, totalPrice);
                }

                body.AppendFormat("Total Price: {0:c}", order.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("Back Card Details:")
                    .AppendLine(shippingDetails.CardNumber)
                    .AppendLine(shippingDetails.Table.ToString())
                    .AppendLine("---");

                MailMessage mailMessage = new MailMessage(
                                       emailSettings.MailFromAddress,	
                                       emailSettings.MailToAddress,		
                                       "New order was sended!",		
                                       body.ToString());

                smtpClient.Send(mailMessage);
            }

        }
    }
}
