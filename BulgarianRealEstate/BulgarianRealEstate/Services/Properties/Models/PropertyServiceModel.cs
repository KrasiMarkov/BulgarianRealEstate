using BulgarianRealEstate.Services.Properties.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Services.Properties
{
    public class PropertyServiceModel : IPropertyModel
    {
        public int Id { get; init; }

        public int Size { get; init; }

        public int Floor { get; init; }

        public int TotalNumberOfFloor { get; init; }

        public int Year { get; init; }

        public string DistrictName { get; init; }

        public string PropertyTypeName { get; init; }

        public string BuildingTypeName { get; init; }

        public int Price { get; init; }

        public List<byte[]> Images { get; set; }

        public string Description { get; init; }

    }
}
