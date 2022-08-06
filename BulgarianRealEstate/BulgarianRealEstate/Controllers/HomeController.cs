using BulgarianRealEstate.Data;
using BulgarianRealEstate.Models;
using BulgarianRealEstate.Models.Home;
using BulgarianRealEstate.Models.Properties;
using BulgarianRealEstate.Services.Properties;
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

        private readonly IPropertyService properties;
        private readonly IStatisticsService statistics;

        public HomeController(IPropertyService properties, IStatisticsService statistics)
        {
            this.properties = properties;
            this.statistics = statistics;
        }
        public IActionResult Index() 
        {

            var latestProperties = this.properties.Latest();

            var totalStatistics = this.statistics.Total();

            return View(new IndexViewModel
            {
                TotalProperties = totalStatistics.TotalProperties,
                TotalUsers = totalStatistics.TotalUsers,
                Properties = latestProperties
            });


        }


       
        public IActionResult Error() => View();

    }
}
