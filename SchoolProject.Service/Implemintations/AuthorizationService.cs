using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Dto;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstractions;

namespace SchoolProject.Service.Implemintations
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public AuthorizationService(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<string> AddRoleAsync(string roleName)
        {
            var identityRole = new Role();
            identityRole.Name = roleName;
            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
                return "Success";
            return "Fail";
        }

        public async Task<string> DeleteRoleAsync(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
                return "NotFound";

            var users = await _userManager.GetUsersInRoleAsync(role.Name);

            if (users != null && users.Count() > 0)
                return "Used";

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
                return "Success";

            var errors = string.Join("-", result.Errors);
            return errors;
        }

        public async Task<string> EditRoleAsync(EditRoleDto request)
        {
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role == null)
                return "NotFound";

            role.Name = request.RoleName;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
                return "Success";

            var errors = string.Join("-", result.Errors);
            return errors;
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
        }

        public async Task<List<Role>> GetRolesList()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<bool> IsRoleExist(string roleName)
        {
            //var role = await _roleManager.FindByNameAsync(roleName);
            //if (role == null) return false;
            //return true;

            return await _roleManager.RoleExistsAsync(roleName);
        }
    }
}
