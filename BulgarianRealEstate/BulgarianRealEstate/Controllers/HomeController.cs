using BulgarianRealEstate.Data;
using BulgarianRealEstate.Models;
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

            var allProperties = this.data
                                   .Properties
                                   .Select(x => new AllPropertyViewModel
                                   {

                                       Size = x.Size,
                                       Floor = x.Floor,
                                       TotalNumberOfFloor = x.TotalNumberOfFloor,
                                       Year = x.Year,
                                       District = x.District.Name,
                                       PropertyType = x.PropertyType.Name,
                                       BuildingType = x.BuildingType.Name,
                                       Price = x.Price,
                                       Description = x.Description,
                                       Images = x.PropertyImages
                                                               .Select(i => i.Image.Content)
                                                               .ToList()

                                   })
                                   .ToList();

            return View(allProperties);


        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

    }
}
