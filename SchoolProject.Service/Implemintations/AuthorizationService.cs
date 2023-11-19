using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Dto;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Data.Requests;
using SchoolProject.Data.Results;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Service.Abstractions;
using System.Security.Claims;

namespace SchoolProject.Service.Implemintations
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public AuthorizationService(RoleManager<Role> roleManager, UserManager<User> userManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
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

        public async Task<string> EditRoleAsync(EditRoleRequest request)
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

        public async Task<List<Role>> MnageUserRoles()
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

        public async Task<ManageUserRolesResult> ManageuserRolesData(User user)
        {
            var roles = await _roleManager.Roles.ToListAsync();

            var reponse = new ManageUserRolesResult();
            var UserRolesList = new List<UserRoles>();

            reponse.UserId = user.Id;
            foreach (var role in roles)
            {
                var nweUserRole = new UserRoles();
                nweUserRole.RoleId = role.Id;
                nweUserRole.RoleName = role.Name;

                if (await _userManager.IsInRoleAsync(user, role.Name))
                    nweUserRole.HasRole = true;
                else
                    nweUserRole.HasRole = false;

                UserRolesList.Add(nweUserRole);
            }
            reponse.UserRoles = UserRolesList;
            return reponse;
        }

        public async Task<string> UpdateUserRoles(UpdateUserRolesRequest request)
        {
            var trans = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                    return "UserIsNull";

                var userRoles = await _userManager.GetRolesAsync(user);          // get roles of user//
                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);     // remove roles of user//
                if (!removeResult.Succeeded)
                    return "FailedToDeleteoldRoles";

                var selectedRoles = request.UserRoles.Where(x => x.HasRole == true).Select(x => x.RoleName);
                var addedRolesResult = await _userManager.AddToRolesAsync(user, selectedRoles);    // add roles to user 
                if (!addedRolesResult.Succeeded)
                    return "FailedToAddNewRoles";

                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "FailedToUpdateUserRoles";
            }
        }

        public async Task<ManageUserClaimsResult> ManageuserClaimsData(User user)
        {

            var response = new ManageUserClaimsResult();
            var userClaimsList = new List<UserClaims>();
            response.UserId = user.Id;

            //get claims of user
            var userclaims = await _userManager.GetClaimsAsync(user);

            // get all claims in db
            var Claims = ClaimStore.claims;
            foreach (var claim in Claims)
            {
                var userClaim = new UserClaims();
                userClaim.Type = claim.Type;
                if (userclaims.Any(x => x.Type == claim.Type))
                    userClaim.Value = true;
                else
                    userClaim.Value = false;
                userClaimsList.Add(userClaim);
            }
            response.userClaims = userClaimsList;
            return response;
        }

        public async Task<string> UpdateUserClaims(UpdateUserClaimsRequest request)
        {
            var trans = await _context.Database.BeginTransactionAsync();
            try
            {
                var user = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (user == null)
                    return "UserIsNull";
                var userClaims = await _userManager.GetClaimsAsync(user);
                var removeResult = await _userManager.RemoveClaimsAsync(user, userClaims);
                if (!removeResult.Succeeded)
                    return "FailedToDeleteoldClaims";

                var selectedClaims = request.userClaims.Where(x => x.Value == true).Select(x => new Claim(x.Type, x.Value.ToString()));
                var AddClaimsResult = await _userManager.AddClaimsAsync(user, selectedClaims);
                if (!AddClaimsResult.Succeeded)
                    return "FailedToAddNewClaims";

                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "FailedToUpdateUserClaims";
            }
        }
    }
}
