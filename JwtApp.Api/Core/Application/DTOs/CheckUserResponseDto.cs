namespace JwtApp.Api.Core.Application.DTOs
{
    public class CheckUserResponseDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Role { get; set; }
        public bool IsExist { get; set; }
    }
}
