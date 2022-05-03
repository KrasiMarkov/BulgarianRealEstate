using BulgarianRealEstate.Data;
using BulgarianRealEstate.Models.Api.Statistics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Controllers.Api
{
    [ApiController]
    [Route("api/statistics")]
    public class StatisticsApiController : ControllerBase
    {
        private readonly RealEstateDbContext data;
        public StatisticsApiController(RealEstateDbContext data)
        {
            this.data = data;
        }

        [HttpGet]
        public StatisticsResponseModel GetStatistics()
        {
            var totalProperties = this.data.Properties.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsResponseModel
            {
                TotalProperties = totalProperties,
                TotalUsers = totalUsers,
                TotalSales = 0
            };
        }


    }
}
