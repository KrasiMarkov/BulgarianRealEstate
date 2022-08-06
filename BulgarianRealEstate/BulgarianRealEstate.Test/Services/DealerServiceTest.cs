using BulgarianRealEstate.Data;
using BulgarianRealEstate.Data.Models;
using BulgarianRealEstate.Services.Dealers;
using BulgarianRealEstate.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BulgarianRealEstate.Test.Services
{
    public class DealerServiceTest
    {
        private const string userId = "Krasi";

        [Fact]
        public void IsDealerShouldReturnTrueWhenUserIdIsDealer()
        {
            //Arrange

            var dealerService = GetDealerService();

            //Act

            var result = dealerService.IsDealer(userId);

            //Assert

            Assert.True(result);

        }

        [Fact]
        public void IsDealerShouldReturnFalseWhenUserIdIsNotDealer()
        { 
            //Arrange

            var dealerService = GetDealerService();

            //Act

            var result = dealerService.IsDealer("Milko");

            //Assert

            Assert.False(result);

        }

        private IDealerService GetDealerService()
        {
            var data = DatabaseMock.Instance;

            data.Dealers.Add(new Dealer { UserId = userId });
            data.SaveChanges();

            return new DealerService(data);

            
        }

    }
}
