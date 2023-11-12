using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolProject.Service.Implemintations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtSetting _jwtsettings;
        public AuthenticationService(JwtSetting jwtsettings)
        {
            _jwtsettings = jwtsettings;
        }

        // generate jwt token
        public Task<string> GetjwtToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimModel.UserName),user.UserName),
                new Claim(nameof(UserClaimModel.Email),user.Email),
                new Claim(nameof(UserClaimModel.phoneNumber),user.PhoneNumber),
            };
            var JwtToken = new JwtSecurityToken
            (
                issuer: _jwtsettings.Issuer,
                audience: _jwtsettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(3),
                signingCredentials: new SigningCredentials
                                    (new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtsettings.Secret)), SecurityAlgorithms.HmacSha256)
            );
            var AccessToken = new JwtSecurityTokenHandler().WriteToken(JwtToken);
            return Task.FromResult(AccessToken);
        }
    }
}
