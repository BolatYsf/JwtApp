using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace JwtApp.Mvc.Models
{
    public class CreateProductVM
    {
        [Required(ErrorMessage ="Name Required")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Stock Required")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "Price Required")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Make A Category Selection")]
        public int CategoryId { get; set; }

        public SelectList? Categories { get; set; }

    }
}
