using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Services.Properties
{
    public class PropertyDetailsServiceModel : PropertyServiceModel
    {
       
        public int DealerId { get; init; }

        public string DealerName { get; init; }

        public int PropertyTypeId { get; init; }

        public int BuildingTypeId { get; init; }

        public int DistrictId { get; init; }

        public string UserId { get; init; }
    }
}
