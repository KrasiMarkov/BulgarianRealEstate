using BulgarianRealEstate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Services.Statistics
{
    public class StatisticsService : IStatisticsService
    {
        private readonly RealEstateDbContext data;
        public StatisticsService(RealEstateDbContext data)
        {
            this.data = data;
        }

        public StatisticsServiceModel Total()
        {
            var totalProperties = this.data.Properties.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalProperties = totalProperties,
                TotalUsers = totalUsers
            };
        }
    }
}
