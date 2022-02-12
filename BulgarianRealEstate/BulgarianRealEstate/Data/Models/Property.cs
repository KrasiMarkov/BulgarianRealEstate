using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Data.Models
{
    public class Property
    {
        public int Id { get; set; }

        public int Size { get; set; }

        public int Floor { get; set; }

        public int TotalNumberOfFloor { get; set; }

        public int Year { get; set; }

        public int DistrictId { get; set; }

        public District District { get; set; }

        public int PropertyTypeId { get; set; }
        
        public PropertyType PropertyType { get; set; }

        public int BuildingTypeId { get; set; }

        public BuildingType BuildingType { get; set; }

        public int Price { get; set; }

        [Required]
        public string Description { get; set; }

    }
}
