using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Models.Home
{
    public class IndexViewModel
    {
        public int TotalProperties { get; init; }

        public int TotalUsers { get; init; }

        public int TotalSales { get; init; }

        public List<PropertyIndexViewModel> Properties { get; init; }
        
    }
}
