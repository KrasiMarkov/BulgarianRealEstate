using BulgarianRealEstate.Data;
using BulgarianRealEstate.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app) 
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<RealEstateDbContext>();

            data.Database.Migrate();

            SeedData(data);

            return app;

        }

        private static void SeedData(RealEstateDbContext data)
        {
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

    }
}
