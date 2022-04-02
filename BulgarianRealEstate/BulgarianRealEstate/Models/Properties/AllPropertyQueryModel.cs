using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Models.Properties
{
    public class AllPropertyQueryModel
    {
       
        public string Keyword { get; init; }

        [Display(Name = "Min Floor")]
        public string MinFloor { get; set; }

        [Display(Name = "Max Floor")]
        public string MaxFloor { get; set; }

        [Display(Name = "Min Year")]
        public string MinYear { get; set; }

        [Display(Name = "Max Year")]
        public string MaxYear { get; set; }

        [Display(Name = "Min Size")]
        public string MinSize { get; set; }

        [Display(Name = "Max Size")]
        public string MaxSize { get; set; }

        [Display(Name = "Min Price")]
        public string MinPrice { get; set; }

        [Display(Name = "Max Price")]
        public string MaxPrice { get; set; }

        [Display(Name = "Building Type")]
        public int BuildingTypeId { get; init; }

        [Display(Name = "Property Type")]
        public int PropertyTypeId { get; init; }

        [Display(Name = "Location")]
        public int DistrictId { get; init; }
        public List<PropertyListingViewModel> Properties { get; init; }

        public IEnumerable<BuildingTypeViewModel> BuildingTypes { get; set; }

        public IEnumerable<PropertyTypeViewModel> PropertyTypes { get; set; }

        public IEnumerable<DistrictViewModel> Districts { get; set; }
    }
}
