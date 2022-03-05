using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Data.Models
{
    public class PropertyImage
    {
        public int PropertyId { get; set; }

        public Property Property { get; set; }

        public int ImageId { get; set; }

        public Image Image { get; set; }

    }
}
