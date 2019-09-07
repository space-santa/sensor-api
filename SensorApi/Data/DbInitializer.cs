using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SensorApi.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace SensorApi.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider, IWebHostEnvironment environment)
        {
            using (var context = serviceProvider.GetRequiredService<TemperatureContext>())
            {
                context.Database.Migrate();
                await CreateFirstUserAsync(serviceProvider);

                if (environment.IsDevelopment())
                {
                    DbInitializer.SeedTestData(context);
                }
            }
        }

        public static async Task CreateFirstUserAsync(IServiceProvider serviceProvider)
        {
            await EnsureRoleAsync(serviceProvider, Constants.AdministratorRole);
            await EnsureRoleAsync(serviceProvider, Constants.DeviceRole);
            await EnsureRoleAsync(serviceProvider, Constants.UserRole);
            // The password must contain at least contain one non alphanumeric character.
            // Else the seeding fails silently.
            var adminID = await EnsureUserAsync(serviceProvider, "Password123*", "admin@clau.space");
            await AddUserToRole(serviceProvider, adminID, Constants.AdministratorRole);
        }

        private static async Task<string> EnsureUserAsync(IServiceProvider serviceProvider, string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser { UserName = UserName, Email = UserName };
                await userManager.CreateAsync(user, testUserPw);
            }

            return user.Id;
        }

        private static async Task EnsureRoleAsync(IServiceProvider serviceProvider, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private static async Task<IdentityResult> AddUserToRole(IServiceProvider serviceProvider, string uid, string role)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            var user = await userManager.FindByIdAsync(uid);
            return await userManager.AddToRoleAsync(user, role);
        }

        public static void SeedTestData(TemperatureContext context)
        {
            if (context.Devices.Any())
            {
                return;
            }

            var devices = new Device[]
            {
                new Device{Location="Garage", Name="bt1234"},
                new Device{Location="Living Room", Name="burglgrub"},
                new Device{Location="Stairs", Name="BOULder4"},
            };
            foreach (Device d in devices)
            {
                context.Devices.Add(d);
            }
            context.SaveChanges();

            var temperatureItems = new TemperatureItem[]
            {
                new TemperatureItem{Temperature=12.34, Device=devices[0], Timestamp=DateTime.Parse("2017-12-01 12:34:56")},
                new TemperatureItem{Temperature=13.34, Device=devices[1], Timestamp=DateTime.Parse("2017-12-01 12:35:56")},
                new TemperatureItem{Temperature=14.34, Device=devices[2], Timestamp=DateTime.Parse("2017-12-01 12:36:56")},
                new TemperatureItem{Temperature=15.34, Device=devices[0], Timestamp=DateTime.Parse("2017-12-01 12:37:56")},
                new TemperatureItem{Temperature=16.34, Device=devices[1], Timestamp=DateTime.Parse("2017-12-01 12:38:56")},
                new TemperatureItem{Temperature=17.34, Device=devices[2], Timestamp=DateTime.Parse("2017-12-01 12:39:56")},
                new TemperatureItem{Temperature=18.34, Device=devices[0], Timestamp=DateTime.Parse("2017-12-01 12:40:56")},
                new TemperatureItem{Temperature=19.34, Device=devices[1], Timestamp=DateTime.Parse("2017-12-01 12:41:56")},
                new TemperatureItem{Temperature=20.34, Device=devices[2], Timestamp=DateTime.Parse("2017-12-01 12:42:56")},
            };
            foreach (TemperatureItem t in temperatureItems)
            {
                context.TemperatureItems.Add(t);
            }
            context.SaveChanges();
        }
    }
}
