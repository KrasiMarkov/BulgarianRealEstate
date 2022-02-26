using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Data.Models
{
    public class PropertyImageUrl
    {
        public int PropertyId { get; set; }

        public Property Property { get; set; }

        public int ImageUrlId { get; set; }

        public ImageUrl ImageUrl { get; set; }

    }
}
