using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MyTested.AspNetCore.Mvc;
using BulgarianRealEstate.Controllers;
using BulgarianRealEstate.Models.Dealers;
using BulgarianRealEstate.Data.Models;
using static BulgarianRealEstate.WebConstants;
using BulgarianRealEstate.Models.Properties;

namespace BulgarianRealEstate.Test.Controller
{
    public class DealersControllerTest
    {
        [Fact]

        public void GetBecomeShouldBeForAuthorizedUsersAndReturnView()
          => MyController<DealersController>
                    .Instance()
                    .Calling(b => b.Become())
                    .ShouldHave()
                    .ActionAttributes(attributes => attributes
                         .RestrictingForAuthorizedRequests())
                    .AndAlso()
                    .ShouldReturn()
                    .View();



        [Theory]
        [InlineData("Dealer", "+359888888888")]
        public void PostBecomeShouldBeForAuthorizedUsersAndReturnRedirectWithValidModel(
            string dealerName,
            string phoneNumber)
            => MyController<DealersController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Become(new BecomeDealerFormModel
                {
                    Name = dealerName,
                    PhoneNumber = phoneNumber
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post)
                    .RestrictingForAuthorizedRequests())
                .ValidModelState()
                .Data(data => data
                    .WithSet<Dealer>(dealer => dealer
                        .Any(d =>
                            d.Name == dealerName &&
                            d.PhoneNumber == phoneNumber &&
                            d.UserId == TestUser.Identifier)))
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(GlobalMessageKey))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect
                    .To<PropertiesController>(c => c.All(With.Any<AllPropertyQueryModel>())));
    }
}
