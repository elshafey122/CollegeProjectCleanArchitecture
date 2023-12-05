using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Service.Abstractions
{
    public interface ICurrentUserService
    {
        public int GetUserId();
        public Task<User> GetUserAsync();
        public Task<List<string>> GetCurrentUserRolesasync();
    }
}
