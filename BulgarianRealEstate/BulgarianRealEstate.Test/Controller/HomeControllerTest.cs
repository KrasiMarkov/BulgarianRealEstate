using BulgarianRealEstate.Controllers;
using BulgarianRealEstate.Data.Models;
using BulgarianRealEstate.Models.Home;
using BulgarianRealEstate.Services.Properties;
using BulgarianRealEstate.Services.Statistics;
using BulgarianRealEstate.Test.Mocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MyTested.AspNetCore.Mvc;
using FluentAssertions;
using static BulgarianRealEstate.WebConstants.Cache;
using static BulgarianRealEstate.Test.Data.Properties;


namespace BulgarianRealEstate.Test.Controller
{
    public class HomeControllerTest
    {
        //[Fact]
        //public void IndexShouldReturnViewWithCorrectModel() 
        //{
        //    //Arrange

        //    var data = DatabaseMock.Instance;


        //    var properties = Enumerable
        //        .Range(0, 10)
        //        .Select(i => new Property());

        //    data.Properties.AddRange(properties);
        //    data.Users.Add(new User());
        //    data.SaveChanges();

        //    var propertyService = new PropertyService(data);


        //    var homeController = new HomeController(propertyService, null);

        //    //Act

        //    var result = homeController.Index();

        //    //Assert

        //    Assert.NotNull(result);

        //    var viewResult = Assert.IsType<ViewResult>(result);

        //    var model = viewResult.Model;

        //    var indexViewModel = Assert.IsType<IndexViewModel>(model);


        //    Assert.Equal(10, indexViewModel.TotalProperties);
        //    Assert.Equal(1, indexViewModel.TotalUsers);
        //    //Assert.Equal(3, indexViewModel.Properties.Count);


        //}

        //[Fact]
        //public void ErrorShouldReturnView()
        //{
        //    //Arrang

        //    var homeController = new HomeController(null, null);

        //    //Act

        //    var result = homeController.Error();

        //    //Assert

        //    Assert.NotNull(result);
        //    Assert.IsType<ViewResult>(result);
        //}

        [Fact]
        public void IndexShouldReturnCorrectViewWithModel()
            => MyController<HomeController>
                         .Instance(instance => instance
                             .WithData(TenPublicProperties))
                         .Calling(p => p.Index())
                         .ShouldHave()
                         .MemoryCache(cache => cache
                             .ContainingEntry(entry => entry
                             .WithKey(LatestPropertiesCacheKey)
                             .WithAbsoluteExpirationRelativeToNow(TimeSpan.FromMinutes(15))
                             .WithValueOfType<List<LatestPropertiesServiceModel>>()))
                         .AndAlso()
                         .ShouldReturn()
                         .View(view => view
                              .WithModelOfType<List<LatestPropertiesServiceModel>>()
                              .Passing(model => model.Should().HaveCount(3)));

        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
            .Instance()
            .Calling(e => e.Error())
            .ShouldReturn()
            .View();
        
       
              

    }
}
