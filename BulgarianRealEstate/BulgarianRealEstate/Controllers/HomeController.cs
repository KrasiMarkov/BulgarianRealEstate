using BulgarianRealEstate.Data;
using BulgarianRealEstate.Models;
using BulgarianRealEstate.Models.Home;
using BulgarianRealEstate.Models.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Controllers
{
    public class HomeController : Controller
    {

        private readonly RealEstateDbContext data;

        public HomeController(RealEstateDbContext data)
        {
            this.data = data;
        }
        public IActionResult Index() 
        {
            var totalUsers = this.data
                                 .Users
                                 .Count();

            var totalProperties = this.data
                                      .Properties
                                      .Count();

            var allProperties = this.data
                                   .Properties
                                   .OrderByDescending(p => p.Id)
                                   .Select(x => new PropertyIndexViewModel
                                   {
                                       District = x.District.Name,
                                       PropertyType = x.PropertyType.Name,
                                       Price = x.Price,
                                       Images = x.PropertyImages
                                                               .Select(i => i.Image.Content)
                                                               .ToList()
                                    
                                   })
                                   .Take(3)
                                   .ToList();

            return View( new IndexViewModel 
            {
                TotalProperties = totalProperties,
                TotalUsers = totalUsers,
                Properties = allProperties
            });


        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

    }
}
