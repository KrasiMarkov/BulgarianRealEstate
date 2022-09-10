using BulgarianRealEstate.Controllers;
using BulgarianRealEstate.Data.Models;
using BulgarianRealEstate.Models.Home;
using FluentAssertions;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static BulgarianRealEstate.Test.Data.Properties;

namespace BulgarianRealEstate.Test.Pipeline
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewWithCorrectModelAndData()
           => MyMvc
                  .Pipeline()
                  .ShouldMap("/")
                  .To<HomeController>(p => p.Index())
                  .Which(controller => controller
                      .WithData(TenPublicProperties))
                  .ShouldReturn()
                  .View(view => view
                       .WithModelOfType<List<LatestPropertiesServiceModel>>()
                       .Passing(m => m.Should().HaveCount(3)));


        [Fact]
        public void ErrorShouldReturnView()
            => MyMvc
                  .Pipeline()   
                  .ShouldMap("/Home/Error")
                  .To<HomeController>(c => c.Error())
                  .Which()
                  .ShouldReturn()
                  .View();
        

    }
}
