using BulgarianRealEstate.Data;
using BulgarianRealEstate.Models;
using BulgarianRealEstate.Models.Home;
using BulgarianRealEstate.Models.Properties;
using BulgarianRealEstate.Services.Statistics;
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
        private readonly IStatisticsService statistics;

        public HomeController(IStatisticsService statistics, RealEstateDbContext data)
        {
            this.data = data;
            this.statistics = statistics;
        }
        public IActionResult Index() 
        {

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

            var totalStatistics = this.statistics.Total();

            return View( new IndexViewModel 
            {
                TotalProperties = totalStatistics.TotalProperties,
                TotalUsers = totalStatistics.TotalUsers,
                Properties = allProperties
            });


        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

    }
}
