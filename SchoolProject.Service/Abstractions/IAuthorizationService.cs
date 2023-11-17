using SchoolProject.Data.Dto;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Service.Abstractions
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsRoleExist(string roleName);
        public Task<string> EditRoleAsync(EditRoleDto request);
        public Task<string> DeleteRoleAsync(int roleId);
        public Task<List<Role>> GetRolesList();
        public Task<Role> GetRoleById(int id);

    }
}
