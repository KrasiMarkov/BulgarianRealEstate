using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Data.Models
{
    public class PropertyType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        public IEnumerable<Property> Properties { get; set; } = new List<Property>();
    }
}
