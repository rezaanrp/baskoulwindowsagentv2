using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Classes
{
    public class JwtTokenHelper
    {
       private readonly IConfiguration _config;

        public JwtTokenHelper(IConfiguration config)
        {
            _config = config;
        }
        public JwtTokenHelper()
        { }
        public string CreateToken(IdentityUser user)
        {
            // 1. Read Jwt section from configuration (User Secrets)
            var jwtSection = _config.GetSection("Jwt");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!)
            };

            // 2. Generate signing key
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSection["Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 3. Create token
            var token = new JwtSecurityToken(
                issuer: jwtSection["Issuer"],
                audience: jwtSection["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(12),
                signingCredentials: creds
            );

            // 4. Return token string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
