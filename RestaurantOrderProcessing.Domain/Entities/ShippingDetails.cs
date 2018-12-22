using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RestaurantOrderProcessing.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Заполните поле номера карты")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Укажите номер вашего столика")]
        public int Table { get; set; }
    }
}
