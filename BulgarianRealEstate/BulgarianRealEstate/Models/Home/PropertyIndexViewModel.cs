using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Models.Home
{
    public class PropertyIndexViewModel
    {
        public int Id { get; init; }

        public string District { get; init; }

        public string PropertyType { get; init; }

        public int Price { get; init; }

        public List<byte[]> Images { get; set; }

    }
}
