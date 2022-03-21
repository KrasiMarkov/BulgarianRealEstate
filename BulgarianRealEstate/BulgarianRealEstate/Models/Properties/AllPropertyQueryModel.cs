using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Models.Properties
{
    public class AllPropertyQueryModel
    {
        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }
        public List<PropertyListingViewModel> Properties { get; init; }
    }
}
