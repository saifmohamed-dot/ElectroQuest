using ElectroQuest.Application.Users.Authentication;
using ElectroQuest.Domain.Entities;
using ElectroQuest.Infrastructure.Analytics.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ElectroQuest.Infrastructure.Users
{
    public class JWTAuthentication(JWTSettings settings) : IAuthentication
    {
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = settings.Issuer,
                Audience = settings.Audience,
                SigningCredentials = new SigningCredentials
                    (
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key)),
                        SecurityAlgorithms.HmacSha256
                    ),
                Subject = new ClaimsIdentity(new[] 
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier , user.Id.ToString())
                })
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
