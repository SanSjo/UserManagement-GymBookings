using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement_GymBookings.Models;

namespace UserManagement_GymBookings.Data
{
    public class SeedData
    {
        public static async Task InitAsync(IServiceProvider services, string adminPW)
        {
            using (var context = new ApplicationDbContext(services.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (await context.GymClass.AnyAsync()) return;

                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                var fake = new Faker("sv");

                var gymClasses = new List<GymClass>();

                for (int i = 0; i < 20; i++)
                {
                    var gymClass = new GymClass
                    {
                        Name = fake.Company.CatchPhrase(),
                        Description = fake.Hacker.Verb(),
                        Duration = new TimeSpan(0, 55, 0),
                        StartTime = DateTime.Now.AddDays(fake.Random.Int(-2, 2))

                    };

                    gymClasses.Add(gymClass);

                }
                await context.AddRangeAsync(gymClasses);

                var roleNames = new[] { "Admin", "Member" };

                foreach (var roleName in roleNames)
                {
                    if (await roleManager.RoleExistsAsync(roleName))
                    {
                        continue;
                    }
                    var role = new IdentityRole { Name = roleName };
                    var result = await roleManager.CreateAsync(role);

                    if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
                }

                var adminEmail = "admin@gym.se";

                var foundAdmin = await userManager.FindByEmailAsync(adminEmail);
                if (foundAdmin != null) return;

                var admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                };

                var addAdminResult = await userManager.CreateAsync(admin, adminPW);
                if (!addAdminResult.Succeeded) throw new Exception(string.Join("\n", addAdminResult.Errors));

                var adminUser = await userManager.FindByNameAsync(adminEmail);

                foreach (var role in roleNames)
                {
                    if (await userManager.IsInRoleAsync(adminUser, role)) continue;

                    var addToRoleResult = await userManager.AddToRoleAsync(adminUser, role);

                    if (!addToRoleResult.Succeeded) throw new Exception(string.Join("\n", addToRoleResult.Errors));

                }
                await context.SaveChangesAsync();



            }
        }
    }
}
