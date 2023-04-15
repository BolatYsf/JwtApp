namespace JwtApp.Api.Infrastructure.Tools
{
    public class JwtCustomDefaults
    {
        /*ValidAudience = ,
        ValidIssuer = "https://localhost",
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ysfysf1907")),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,*/

        public const string ValidAudience = "https://localhost";
        public const string ValidIssuer = "https://localhost";
        public const string Key = "ysfysfysyfysfyysyddsysdsydsdysfener1907";
        public const int Expire = 4;

    }
}
