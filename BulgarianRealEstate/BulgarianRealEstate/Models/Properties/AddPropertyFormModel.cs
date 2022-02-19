using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Models.Properties
{
    public class AddPropertyFormModel
    { 
        public int? Size { get; init; }

        public int? Floor { get; init; }

        [Display(Name = "Total number of floor")]
        public int? TotalNumberOfFloor { get; init; }

        public int? Year { get; init; }

        [Display(Name = "District")]
        public int DistrictId { get; init; }

        [Display(Name = "Property type")]
        public int PropertyTypeId { get; init; }

        [Display(Name = "Building type")]
        public int BuildingTypeId { get; init; }

        public int? Price { get; init; }

        public string Description { get; init; }

        public IEnumerable<PropertyTypeViewModel> PropertyTypes { get; set; }

        public IEnumerable<DistrictViewModel> Districts { get; set; }

        public IEnumerable<BuildingTypeViewModel> BuildingTypes { get; set; }

        

    }
}
