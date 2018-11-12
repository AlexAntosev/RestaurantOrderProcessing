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
        [Required(ErrorMessage = "Enter number your bank card")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Enter Month")]
        public int Month { get; set; }
        [Required(ErrorMessage = "Enter Year")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Enter CVC")]
        public int Cvc { get; set; }
    }
}
