using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Data.Models
{
    public class ImageUrl
    {
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        public IEnumerable<PropertyImageUrl> PropertyImageUrls { get; set; } = new List<PropertyImageUrl>();

    }
}
