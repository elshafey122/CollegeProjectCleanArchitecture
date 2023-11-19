using SchoolProject.Data.Dto;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Requests;
using SchoolProject.Data.Results;

namespace SchoolProject.Service.Abstractions
{
    public interface IAuthorizationService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsRoleExist(string roleName);
        public Task<string> EditRoleAsync(EditRoleRequest request);
        public Task<string> UpdateUserRoles(UpdateUserRolesRequest request);
        public Task<string> UpdateUserClaims(UpdateUserClaimsRequest request);

        public Task<string> DeleteRoleAsync(int roleId);
        public Task<List<Role>> MnageUserRoles();
        public Task<Role> GetRoleById(int id);
        public Task<ManageUserRolesResult> ManageuserRolesData(User user);
        public Task<ManageUserClaimsResult> ManageuserClaimsData(User user);
    }
}
