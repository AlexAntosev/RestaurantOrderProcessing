using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RestaurantOrderProcessing.Domain.Entities
{
    public class Dish
    {
        [HiddenInput(DisplayValue = false)]
        public int DishId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Enter name")]
        public string Name { get; set; }
        
        [Display(Name = "Category")]
        [Required(ErrorMessage = "Enter category")]
        public string Category { get; set; }
        
        [Display(Name = "Weight")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Enter povitive value for weight")]
        public decimal Weight { get; set; }
        
        [Display(Name = "Price (UAH)")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Enter povitive value for price")]
        public decimal Price { get; set; }
        
        [Display(Name = "Time")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Enter povitive value for time")]
        public int Time { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
