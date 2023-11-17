using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrustructure.Seeders
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<Role> _roleManager)
        {
            var roles = await _roleManager.Roles.CountAsync();
            if (roles <= 0)
            {
                await _roleManager.CreateAsync(new Role()
                {
                    Name = "Admin",
                });

                await _roleManager.CreateAsync(new Role()
                {
                    Name = "User",
                });
            }
        }
    }
}
