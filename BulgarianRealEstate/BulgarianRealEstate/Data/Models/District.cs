using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Data.Models
{
    public class District
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Property> Properties { get; set; } = new List<Property>();

    }
}
