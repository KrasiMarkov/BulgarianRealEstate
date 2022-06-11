using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Services.Properties
{
    public interface IPropertyService
    {
        PropertyQueryServiceModel All(
            string keyword,
            int districtId,
            int buildingTypeId,
            int propertyTypeId,
            int minPrice,
            int maxPrice,
            int minSize,
            int maxSize,
            int minYear,
            int maxYear,
            int minFloor,
            int maxFloor,
            int currentPage,
            int propertiesPerPage);


        IEnumerable<PropertyServiceModel> ByUsers(string userId);
    }
}
