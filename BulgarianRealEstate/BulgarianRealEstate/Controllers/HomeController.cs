using BulgarianRealEstate.Data;
using BulgarianRealEstate.Models;
using BulgarianRealEstate.Models.Home;
using BulgarianRealEstate.Models.Properties;
using BulgarianRealEstate.Services.Properties;
using BulgarianRealEstate.Services.Statistics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache cache;

        public HomeController(IPropertyService properties, IMemoryCache cache)
        {
            this.properties = properties;
            this.cache = cache;
        }
        public IActionResult Index() 
        {
            const string latestPropertiesCacheKey = "LatestPropertiesCacheKey";

            var latestProperties = this.cache.Get<List<LatestPropertiesServiceModel>>(latestPropertiesCacheKey);

            if (latestProperties == null) 
            {
                latestProperties = this.properties
                                       .Latest();

                var chacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this.cache.Set(latestPropertiesCacheKey, latestProperties, chacheOptions);
            }

            return View(latestProperties);

        }


       
        public IActionResult Error() => View();

    }
}
