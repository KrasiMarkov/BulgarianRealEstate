using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Models.Api.Properties
{
    public class AllPropertiesApiRequestModel
    {
        public string Keyword { get; init; }

        public int MinFloor { get; set; }

        public int MaxFloor { get; set; }

        public int MinYear { get; set; }

        public int MaxYear { get; set; }

        public int MinSize { get; set; }

        public int MaxSize { get; set; }

        public int MinPrice { get; set; }

        public int MaxPrice { get; set; }

        public int BuildingTypeId { get; init; }

        public int PropertyTypeId { get; init; }

        public int DistrictId { get; init; }

        public int PropertiesPerPage { get; set; } = 10;

        public int CurrentPage { get; set; } = 1;

       

    }
}
