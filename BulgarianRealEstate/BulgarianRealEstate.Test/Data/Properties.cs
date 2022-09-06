using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulgarianRealEstate.Data.Models;


namespace BulgarianRealEstate.Test.Data
{
    public static class Properties
    {
        public static IEnumerable<Property> TenPublicProperties
            => Enumerable.Range(0, 10).Select(i => new Property
            {
                IsPublic = true
            });

    }
}
