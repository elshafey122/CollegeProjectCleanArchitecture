using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrustructure.Seeders
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<User> _userManager)
        {
            var users = await _userManager.Users.CountAsync();
            if (users <= 0)
            {
                var defaultUser = new User()
                {
                    FullName = "AdminProject",
                    Email = "Admin@gmail.com",
                    Country = "Egypt",
                    UserName = "Admin",
                    PhoneNumber = "01089327486",
                    Address = "Cairo",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                };
                await _userManager.CreateAsync(defaultUser, "Admin@123");
                await _userManager.AddToRoleAsync(defaultUser, "Admin");
            }
        }
    }
}
