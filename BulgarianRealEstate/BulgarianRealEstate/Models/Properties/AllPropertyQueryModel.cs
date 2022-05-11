using BulgarianRealEstate.Services.Properties;
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
        public int MinFloor { get; set; }

        [Display(Name = "Max Floor")]
        public int MaxFloor { get; set; }

        [Display(Name = "Min Year")]
        public int MinYear { get; set; }

        [Display(Name = "Max Year")]
        public int MaxYear { get; set; }

        [Display(Name = "Min Size")]
        public int MinSize { get; set; }

        [Display(Name = "Max Size")]
        public int MaxSize { get; set; }

        [Display(Name = "Min Price")]
        public int MinPrice { get; set; }

        [Display(Name = "Max Price")]
        public int MaxPrice { get; set; }

        [Display(Name = "Building Type")]
        public int BuildingTypeId { get; init; }

        [Display(Name = "Property Type")]
        public int PropertyTypeId { get; init; }

        [Display(Name = "Location")]
        public int DistrictId { get; init; }

        public const int PropertiesPerPage = 2;

        public int CurrentPage { get; set; } = 1;

        public int TotalProperties { get; set; }

        public IEnumerable<PropertyServiceModel> Properties { get; set; }

        public IEnumerable<BuildingTypeViewModel> BuildingTypes { get; set; }

        public IEnumerable<PropertyTypeViewModel> PropertyTypes { get; set; }

        public IEnumerable<DistrictViewModel> Districts { get; set; }
    }
}
