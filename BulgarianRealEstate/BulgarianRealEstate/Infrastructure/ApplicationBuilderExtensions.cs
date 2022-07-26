using BulgarianRealEstate.Data;
using BulgarianRealEstate.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BulgarianRealEstate.WebConstants;

namespace BulgarianRealEstate.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app) 
        {
            using var serviseScope = app.ApplicationServices.CreateScope();

            var services = serviseScope.ServiceProvider;

            MigrateDatabase(services);

            SeedData(services);

            SeedAdministrator(services);

            return app;

        }


        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<RealEstateDbContext>();

            data.Database.Migrate();
        }

        private static void SeedData(IServiceProvider services)
        {
            var data = services.GetRequiredService<RealEstateDbContext>();


            if (!data.Districts.Any()) 
            {
                data.Districts.AddRange(new[]
                {
                    new District { Name = "Sofia"},
                    new District { Name = "Plovdiv"},
                    new District { Name = "Varna"},
                    new District { Name = "Burgas"},
                    new District { Name = "Ruse"},
                    new District { Name = "Stara Zagora"}

                });

                data.SaveChanges();
            }

            if (!data.BuildingTypes.Any()) 
            {
                data.BuildingTypes.AddRange(new[]
                {
                    new BuildingType { Name = "Brick"},
                    new BuildingType { Name = "Panel"},
                    new BuildingType { Name = "EPK"}
                });

                data.SaveChanges();
            }

            if (!data.PropertyTypes.Any()) 
            {
                data.PropertyTypes.AddRange(new[]
                {
                    new PropertyType { Name = "One Bedroom"},
                    new PropertyType { Name = "Two Bedroom"},
                    new PropertyType { Name = "Three Bedroom"}
                });

                data.SaveChanges();
            }


        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                {
                    return;
                }

                var role = new IdentityRole { Name = AdministratorRoleName };

                await roleManager.CreateAsync(role);

                const string adminEmail = "admin@krasi.com";
                const string adminPassword = "admin123";

                var user = new User
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FullName = "Admin"
                };

                await userManager.CreateAsync(user, adminPassword);

                await userManager.AddToRoleAsync(user, role.Name);
            })
            .GetAwaiter()
            .GetResult();

        }

    }
}
