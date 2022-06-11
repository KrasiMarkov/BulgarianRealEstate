using BulgarianRealEstate.Data;
using BulgarianRealEstate.Data.Models;
using BulgarianRealEstate.Infrastructure;
using BulgarianRealEstate.Services.Dealers;
using BulgarianRealEstate.Services.Properties;
using BulgarianRealEstate.Services.Statistics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RealEstateDbContext>(options => options
            .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<RealEstateDbContext>();

            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>(); 
            });
            services.AddTransient<IStatisticsService, StatisticsService>();
            services.AddTransient<IPropertyService, PropertyService>();
            services.AddTransient<IDealerService, DealerService>();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection()
               .UseStaticFiles()
               .UseRouting()
               .UseAuthentication()
               .UseAuthorization()
               .UseEndpoints(endpoints =>
               {
                   endpoints.MapDefaultControllerRoute();
                   endpoints.MapRazorPages();
               });
        }
    }
}
