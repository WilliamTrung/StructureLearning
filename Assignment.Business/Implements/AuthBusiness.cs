using Assignment.Business.Abstractions;
using Assignment.Data.UnitOfWork;
using Assignment.Shared.Provider.Abstractions;
using Assignment.Shared.Requests.Auth;
using Assignment.Shared.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Business.Implements
{
    public class AuthBusiness : BusinessBase, IAuthBusiness
    {
        private readonly JwtSetting _jwtSetting;
        public AuthBusiness(IUnitOfWorkService unitOfWorkService, ICoreProvider coreProvider, IOptions<JwtSetting> jwtSetting) : base(unitOfWorkService, coreProvider)
        {
            _jwtSetting = jwtSetting.Value;
        }

        public async Task<string> Login(LoginRequest request)
        {
            using (var _userManager = _coreProvider.IdentityProvider.UserManager)
            {
                var user = await _userManager.FindByNameAsync(request.Username);
                if (user == null)
                {
                    throw new KeyNotFoundException("Wrong credentials");
                }
                var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
                if (isValidPassword)
                {
                    var roles = await _coreProvider.IdentityProvider.UserManager.GetRolesAsync(user);
                    return GenerateToken(user, roles.First());
                }
                else
                {
                    throw new KeyNotFoundException("Wrong credentials");
                }
            }
        }
        internal string GenerateToken(IdentityUser user, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key));

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, role),
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Audience = _jwtSetting.Audience,
                Issuer = _jwtSetting.Issuer,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
