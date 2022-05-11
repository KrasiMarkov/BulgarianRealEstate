using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Services.Properties
{
    public class PropertyQueryServiceModel
    {
        public int CurrentPage { get; set; }

        public int TotalProperties { get; set; }

        public int PropertiesPerPage { get; set; }

        public IEnumerable<PropertyServiceModel> Properties { get; set; }


    }
}
