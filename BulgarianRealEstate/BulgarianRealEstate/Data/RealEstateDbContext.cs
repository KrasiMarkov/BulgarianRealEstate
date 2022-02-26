using BulgarianRealEstate.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulgarianRealEstate.Data
{
    public class RealEstateDbContext : IdentityDbContext
    {
        public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options)
            : base(options)
        {
        }

        public DbSet<Property> Properties { get; set; }

        public DbSet<BuildingType> BuildingTypes { get; set; }

        public DbSet<PropertyType> PropertyTypes { get; set; }

        public DbSet<District> Districts { get; set; }

        public DbSet<ImageUrl> ImageUrls { get; set; }

        public DbSet<PropertyImageUrl> PropertyImageUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                  .Entity<Property>()
                  .HasOne(p => p.BuildingType)
                  .WithMany(b => b.Properties)
                  .HasForeignKey(p => p.BuildingTypeId)
                  .OnDelete(DeleteBehavior.Restrict);


            builder
                 .Entity<Property>()
                 .HasOne(p => p.District)
                 .WithMany(d => d.Properties)
                 .HasForeignKey(p => p.DistrictId)
                 .OnDelete(DeleteBehavior.Restrict);


            builder
                 .Entity<Property>()
                 .HasOne(p => p.PropertyType)
                 .WithMany(p => p.Properties)
                 .HasForeignKey(p => p.PropertyTypeId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<PropertyImageUrl>(e =>
                {
                    e.HasKey(k => new { k.PropertyId, k.ImageUrlId });
                });

            base.OnModelCreating(builder);
        }
    }
}
