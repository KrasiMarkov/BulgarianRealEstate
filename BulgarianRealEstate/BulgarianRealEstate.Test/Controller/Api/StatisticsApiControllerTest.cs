using BulgarianRealEstate.Controllers.Api;
using BulgarianRealEstate.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BulgarianRealEstate.Test.Controller.Api
{
    public  class StatisticsApiControllerTest
    {
        [Fact]
        public void GetStatisticsShouldReturnTotalStatistics() 
        {
            //Arrange

            var statisticsController = new StatisticsApiController(StatisticsServiceMock.Instance);

            //Act

            var result = statisticsController.GetStatistics();

            //Assert

            Assert.NotNull(result);
            Assert.Equal(5, result.TotalProperties);
            Assert.Equal(10, result.TotalSales);
            Assert.Equal(15, result.TotalUsers);

        }

    }
}
