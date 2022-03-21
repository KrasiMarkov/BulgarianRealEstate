using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Models.Properties
{
    public class PropertyListingViewModel
    {
        public int Id { get; init; }
        public int Size { get; init; }

        public int Floor { get; init; }

        public int TotalNumberOfFloor { get; init; }

        public int Year { get; init; }

        public string District { get; init; }

        public string PropertyType { get; init; }

        public string BuildingType { get; init; }

        public int Price { get; init; }

        public List<byte[]> Images { get; set; }

        public string Description { get; init; }


    }
}
