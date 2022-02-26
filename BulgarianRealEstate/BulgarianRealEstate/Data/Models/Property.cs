using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static BulgarianRealEstate.Data.DataConstants;

namespace BulgarianRealEstate.Data.Models
{
    public class Property
    {
        public int Id { get; set; }

        [Range(SizeMinValue, SizeMaxValue)]
        public int Size { get; set; }

        [Range(FloorMinValue, FloorMaxValue)]
        public int Floor { get; set; }

        [Range(TotalNumberOfFloorMinValue, TotalNumberOfFloorMaxValue)]
        public int TotalNumberOfFloor { get; set; }

        [Range(YearMinValue, YearMaxValue)]
        public int Year { get; set; }

        public int DistrictId { get; set; }

        public District District { get; set; }

        public int PropertyTypeId { get; set; }
        
        public PropertyType PropertyType { get; set; }

        public int BuildingTypeId { get; set; }

        public BuildingType BuildingType { get; set; }

        [Range(PriceMinValue, PriceMaxValue)]
        public int Price { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public IEnumerable<PropertyImageUrl> PropertyImageUrls { get; set; } = new List<PropertyImageUrl>();

    }
}
