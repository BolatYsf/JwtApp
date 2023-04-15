namespace JwtApp.Mvc.Models
{
    public class JwtResponseVM
    {
        public string? Token { get; set; }

        public DateTime ExpireDate { get; set; }
    }
}
