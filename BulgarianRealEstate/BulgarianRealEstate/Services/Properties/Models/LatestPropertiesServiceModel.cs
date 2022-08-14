using BulgarianRealEstate.Services.Properties.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Models.Home
{
    public class LatestPropertiesServiceModel : IPropertyModel
    {
        public int Size { get; init; }

        public int Year { get; init; }

        public int Id { get; init; }

        public string BuildingTypeName { get; init; }

        public string DistrictName { get; init; }

        public string PropertyTypeName { get; init; }

        public int Price { get; init; }

        public List<byte[]> Images { get; set; }

    }
}
