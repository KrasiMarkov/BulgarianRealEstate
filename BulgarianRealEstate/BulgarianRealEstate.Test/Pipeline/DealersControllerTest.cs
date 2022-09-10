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

namespace BulgarianRealEstate.Test.Pipeline
{
    public class DealersControllerTest
    {
        [Fact]
        public void GetBecomeShouldForAuthorizedUsersAndReturnView()
            => MyMvc
                       .Pipeline()
                       .ShouldMap(request => request
                             .WithPath("/Dealers/Become")
                             .WithUser())
                       .To<DealersController>(d => d.Become())
                       .Which()
                       .ShouldHave()
                       .ActionAttributes(attributes => attributes
                               .RestrictingForAuthorizedRequests())
                       .AndAlso()
                       .ShouldReturn()
                       .View();



        [Theory]
        [InlineData("Dealer", "+3590886456745")]
        public void PostBecomeShouldForAuthorizedUsersAndRedirectWithValidModel(
            string dealerName,
            string phoneNumber)
            => MyMvc
                    .Pipeline()
                    .ShouldMap(request => request
                           .WithPath("/Dealers/Become")
                           .WithUser()
                           .WithMethod(HttpMethod.Post)
                           .WithAntiForgeryToken()
                           .WithFormFields(new
                           {
                               Name = dealerName,
                               PhoneNumber = phoneNumber
                           }))
                    .To<DealersController>(d => d.Become(new BecomeDealerFormModel
                    {
                        Name = dealerName,
                        PhoneNumber = phoneNumber
                    }))
                    .Which()
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
                             .To<PropertiesController>(p => p.All(With.Any<AllPropertyQueryModel>())));

    }
}
