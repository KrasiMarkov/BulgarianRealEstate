using BulgarianRealEstate.Data;
using BulgarianRealEstate.Models.Api;
using BulgarianRealEstate.Services.Statistics;
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
        private readonly IStatisticsService statistics;
        public StatisticsApiController(IStatisticsService statistics)
        {
            this.statistics = statistics;
        }

        [HttpGet]
        public StatisticsServiceModel GetStatistics()
        {
            var totalStatistics = this.statistics.Total();

            return totalStatistics;
        }


    }
}
