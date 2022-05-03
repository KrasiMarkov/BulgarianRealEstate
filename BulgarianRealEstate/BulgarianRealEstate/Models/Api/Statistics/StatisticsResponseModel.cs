using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Models.Api.Statistics
{
    public class StatisticsResponseModel
    {
        public int TotalProperties { get; init; }

        public int TotalUsers { get; init; }

        public int TotalSales { get; init; }

    }
}
