using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulgarianRealEstate.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace BulgarianRealEstate.Test.Routing
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexRouteShoudBeMapper()
            => MyRouting
                    .Configuration()
                    .ShouldMap("/")
                    .To<HomeController>(c => c.Index());


        [Fact]
        public void ErrorRouteShouldBeMapper()
            => MyRouting
                  .Configuration()
                  .ShouldMap("/Home/Error")
                  .To<HomeController>(c => c.Error());
             

    }
}
