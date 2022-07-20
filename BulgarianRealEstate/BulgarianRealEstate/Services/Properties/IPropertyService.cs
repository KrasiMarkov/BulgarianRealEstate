using BulgarianRealEstate.Models.Properties;
using Microsoft.AspNetCore.Http;
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

        bool IsByDealer(int propertyId, int dealerId);

        bool PropertyTypeExists(int propertyTypeId);

        bool DistrictExists(int districtId);

        bool BuildingTypeExists(int buildingTypeId);

        int Create( int size,
                int floor,
                int totalNumberOfFloor,
                int year,
                int districtId,
                int propertyTypeId,
                int buildingTypeId,
                int price,
                string description,
                int dealerId,
                List<IFormFile> images);

        bool Edit(int propertyId,
                int size,
                int floor,
                int totalNumberOfFloor,
                int year,
                int districtId,
                int propertyTypeId,
                int buildingTypeId,
                int price,
                string description,
                List<IFormFile> images);

        PropertyDetailsServiceModel Details(int propertyId);

        IEnumerable<PropertyTypeServiceModel> GetPropertyTypes();

        IEnumerable<DistrictServiceModel> GetDistricts();

        IEnumerable<BuildingTypeServiceModel> GetBuildingTypes();
    }
}
