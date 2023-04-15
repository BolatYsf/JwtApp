using System.ComponentModel.DataAnnotations;

namespace JwtApp.Mvc.Models
{
    public class UserLoginVM
    {
        [Required(ErrorMessage ="Username required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }
    }
}
