using BulgarianRealEstate.Services.Statistics;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Test.Mocks
{
    public static class StatisticsServiceMock
    {
        public static IStatisticsService Instance 
        {
            get 
            {
                var statisticsServiceMock = new Mock<IStatisticsService>();

                statisticsServiceMock
                    .Setup(s => s.Total())
                    .Returns(new StatisticsServiceModel
                    {
                        TotalProperties = 5,
                        TotalSales = 10,
                        TotalUsers = 15
                    });

                return statisticsServiceMock.Object;
            }
        }
    }
}
