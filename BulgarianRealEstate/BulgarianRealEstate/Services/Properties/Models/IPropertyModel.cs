using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Services.Properties.Models
{
    public interface IPropertyModel
    {
         int Size { get; }

         int Year { get; }

         string BuildingTypeName { get; }

         string DistrictName { get; }

         string PropertyTypeName { get; }

         int Price { get; }

    }
}
