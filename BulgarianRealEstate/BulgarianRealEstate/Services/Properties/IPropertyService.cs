using BulgarianRealEstate.Models.Home;
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
            string keyword = null,
            int districtId = 0,
            int buildingTypeId = 0,
            int propertyTypeId = 0,
            int minPrice = 0,
            int maxPrice = 0,
            int minSize = 0,
            int maxSize = 0,
            int minYear = 0,
            int maxYear = 0,
            int minFloor = 0,
            int maxFloor = 0,
            int currentPage = 1,
            int propertiesPerPage = int.MaxValue,
            bool publicOnly = true);



        IEnumerable<PropertyServiceModel> ByUsers(string userId);

        bool IsByDealer(int propertyId, int dealerId);

        bool PropertyTypeExists(int propertyTypeId);

        bool DistrictExists(int districtId);

        bool BuildingTypeExists(int buildingTypeId);


        void ChangeVisibility(int id);

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

        List<LatestPropertiesServiceModel> Latest();

        PropertyDetailsServiceModel Details(int propertyId);

        IEnumerable<PropertyTypeServiceModel> GetPropertyTypes();

        IEnumerable<DistrictServiceModel> GetDistricts();

        IEnumerable<BuildingTypeServiceModel> GetBuildingTypes();
    }
}
