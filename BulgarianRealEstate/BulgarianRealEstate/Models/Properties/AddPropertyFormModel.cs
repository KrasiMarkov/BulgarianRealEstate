using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Views.RealEstates
{
    public class AddPropertyFormModel
    {
        public int Id { get; set; }

        public int Size { get; set; }

        public int Floor { get; set; }

        public int TotalNumberOfFloor { get; set; }

        public int Year { get; set; }

        public int DistrictId { get; set; }

        public int PropertyTypeId { get; set; }

        public int BuildingTypeId { get; set; }

        public int Price { get; set; }

        public string Description { get; set; }

    }
}
