using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Service.Implemintations
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public int GetUserId()
        {
            // get current user id from request it uses  
            var userId = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == nameof(UserClaimModel.UserId)).Value;
            if (userId == null)
                throw new UnauthorizedAccessException();
            return int.Parse(userId);
        }

        public async Task<User> GetUserAsync()
        {
            int userId = GetUserId();
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new UnauthorizedAccessException();
            return user;
        }

        public async Task<List<string>> GetCurrentUserRolesasync()
        {
            var user = await GetUserAsync();
            var userRoles = await _userManager.GetRolesAsync(user);
            return userRoles.ToList();
        }
    }
}
