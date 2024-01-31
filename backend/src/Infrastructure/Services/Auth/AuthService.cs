using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Models.Token;
using Ecommerce.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.Auth
{
    public class AuthService : IAuthService
    {
        private JwtSettings _jwtSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IHttpContextAccessor httpContextAccessor, IOptions<JwtSettings> jwtSettings)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtSettings = jwtSettings;
        }

        public string createToken(User user, IList<string> roles)
        {
            var claims = new List<Claim>{
                // new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
                new Claim("id", user.Id),
                new Claim("username", user.UserName),
                new Claim("email", user.Email)
            };

            foreach(var role in roles){
                var claim = new Claim(ClaimTypes.Role, role);
                claims.Add(claim);
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescription = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.ExpireTime),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);

        }

        public string getSessionUser()
        {
            //obtener datos de usuario en la session
            var username = _httpContextAccessor
                .HttpContext
                .User
                .Claims
                .FirstOrDefault(x=>x.Type == ClaimTypes.NameIdentifier)
                .Value;

                return username;
        }
    }
}