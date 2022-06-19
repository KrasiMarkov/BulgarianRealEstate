using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static BulgarianRealEstate.Data.DataConstants.Property;

namespace BulgarianRealEstate.Models.Properties
{
    public class PropertyFormModel
    {
        [Range(SizeMinValue, SizeMaxValue)]
        public int Size { get; init; }

        [Range(FloorMinValue, FloorMaxValue)]
        public int Floor { get; init; }

        [Range(TotalNumberOfFloorMinValue, TotalNumberOfFloorMaxValue)]
        [Display(Name = "Total number of floor")]
        public int TotalNumberOfFloor { get; init; }

        [Range(YearMinValue, YearMaxValue)]
        public int Year { get; init; }

        [Display(Name = "District")]
        public int DistrictId { get; init; }

        [Display(Name = "Property type")]
        public int PropertyTypeId { get; init; }

        [Display(Name = "Building type")]
        public int BuildingTypeId { get; init; }

        [Range(PriceMinValue, PriceMaxValue)]
        public int Price { get; init; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; init; }

        public IEnumerable<PropertyTypeServiceModel> PropertyTypes { get; set; }

        public IEnumerable<DistrictServiceModel> Districts { get; set; }

        public IEnumerable<BuildingTypeServiceModel> BuildingTypes { get; set; }


        public List<byte[]> Images { get; set; }
    }
}
