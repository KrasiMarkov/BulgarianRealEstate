using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MyTested.AspNetCore.Mvc;
using BulgarianRealEstate.Controllers;
using BulgarianRealEstate.Models.Dealers;

namespace BulgarianRealEstate.Test.Routing
{
    public class DealersControllerTest
    {
        [Fact]
        public void GetBecomeShouldBeMapped()
            => MyRouting
                    .Configuration()
                    .ShouldMap("/Dealers/Become")
                    .To<DealersController>(c => c.Become());


        [Fact]
        public void PostBecomeShouldBeMapped()
            => MyRouting
                   .Configuration()
                   .ShouldMap(request => request
                       .WithPath("/Dealers/Become")
                       .WithMethod(HttpMethod.Post))
                   .To<DealersController>(c => c.Become(With.Any<BecomeDealerFormModel>()));

    }
}
