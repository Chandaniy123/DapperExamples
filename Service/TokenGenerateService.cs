using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace CQRsAndMEdiatorsEXample.Service
{
    public class TokenGenerateService:ITokenGenerateService
    {
        public TokenGenerateService() { }

        public string GenerateToken(string?userName, string? userEmail,string userSecretKey, string userIssuer, string userAudience)
        {
            var claims = new[]
           {
                new Claim(ClaimTypes.Email, userEmail),
            new Claim(ClaimTypes.Name, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(userSecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                userIssuer,
                userAudience,
                claims,
                expires: DateTime.UtcNow.AddDays(7), // Token expiration time (you can adjust as needed)
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
