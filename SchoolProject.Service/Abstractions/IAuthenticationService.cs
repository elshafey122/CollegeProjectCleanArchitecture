using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using System.IdentityModel.Tokens.Jwt;

namespace SchoolProject.Service.Abstractions
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResult> GetjwtToken(User user);
        public JwtSecurityToken ReadJwtToken(string accessToken);
        public Task<(string, DateTime?)> ValidateTokenDetails(JwtSecurityToken token, string accessToken, string refreshToken);
        public Task<JwtAuthResult> GenerateRefreshToken(User user, JwtSecurityToken jwtToken, string refreshToken, DateTime? expiryDate);
        public Task<string> ValidateToken(string accessToken);
    }
}