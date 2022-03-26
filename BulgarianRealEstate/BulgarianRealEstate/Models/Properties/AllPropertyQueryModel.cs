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

        public string Location { get; init; }

        [Display(Name = "BuildingType")]
        public int BuildingTypeId { get; init; }
        public List<PropertyListingViewModel> Properties { get; init; }

        public IEnumerable<BuildingTypeViewModel> BuildingTypes { get; set; }
    }
}
