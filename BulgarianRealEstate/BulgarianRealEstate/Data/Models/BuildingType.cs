using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static BulgarianRealEstate.Data.DataConstants;


namespace BulgarianRealEstate.Data.Models
{
    public class BuildingType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(BuildingTypeNameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Property> Properties { get; set; } = new List<Property>();

    }
}
