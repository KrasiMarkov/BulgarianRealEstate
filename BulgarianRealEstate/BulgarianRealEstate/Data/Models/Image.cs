using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Data.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        public byte[] Content { get; set; }

        public ICollection<PropertyImage> PropertyImages { get; set; } = new List<PropertyImage>();

    }
}
