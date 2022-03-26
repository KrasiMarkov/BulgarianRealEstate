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
        public List<PropertyListingViewModel> Properties { get; init; }
    }
}
