using JwtApp.Api.Core.Application.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtApp.Api.Infrastructure.Tools
{
    public class JwtGenerator
    {
        public static TokenResponseDto GenerateToken(CheckUserResponseDto dto)
        {


            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(dto.Role))
            {
                claims.Add(new Claim(ClaimTypes.Role, dto.Role));
            }

            claims.Add(new Claim(ClaimTypes.NameIdentifier,dto.Id.ToString()));
            if (!string.IsNullOrEmpty(dto.UserName))
            {
                claims.Add(new Claim("Username", dto.UserName));
            }


           

            


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtCustomDefaults.Key));

            SigningCredentials credentials=new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var expireDate = DateTime.UtcNow.AddMinutes(JwtCustomDefaults.Expire);

            JwtSecurityToken token = new JwtSecurityToken(issuer:JwtCustomDefaults.ValidIssuer,audience:JwtCustomDefaults.ValidAudience,claims: claims, notBefore:DateTime.UtcNow,expires: expireDate, signingCredentials:credentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

             

               return new TokenResponseDto(handler.WriteToken(token), expireDate);

        }
    }
}
