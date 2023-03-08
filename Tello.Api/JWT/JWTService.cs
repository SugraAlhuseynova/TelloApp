using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tello.Core.Entities;

namespace Tello.Api.JWT
{
    public class JWTService : IJWTSerivce
    {
        private readonly IConfiguration _configuration;

        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateJWTToken(AppUser user, IList<string> roles)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("Fullname", user.Fullname),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList());
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:symmetricKey").Value));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddDays(10),
                issuer: _configuration.GetSection("JWT:issuer").Value,
                audience: _configuration.GetSection("JWT:audience").Value
                );
            string tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenStr;
        }
    }
}
