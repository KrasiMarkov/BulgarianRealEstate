using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Models.Api.Properties
{
    public class AllPropertiesApiResponseModel
    {
        public int CurrentPage { get; set; } 

        public int TotalProperties { get; set; }


        public IEnumerable<PropertyResponseModel> Properties { get; set; }

    }
}
