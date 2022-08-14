using BulgarianRealEstate.Models.Home;
using BulgarianRealEstate.Services.Properties;
using BulgarianRealEstate.Services.Properties.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Infrastructure
{
    public static class ModelExtensions
    {
        public static string GetInformation(this IPropertyModel property)
            => property.Size + 
            "-" + 
            property.Year + 
            "-" + 
            property.BuildingTypeName + 
            "-" + 
            property.DistrictName + 
            "-" + 
            property.PropertyTypeName + 
            "-" + 
            property.Price;

    }
}
