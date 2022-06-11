using BulgarianRealEstate.Data;
using BulgarianRealEstate.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Services.Properties
{
    public class PropertyService : IPropertyService
    {

        private readonly RealEstateDbContext data;

        public PropertyService(RealEstateDbContext data)
        {
            this.data = data;
        }

        public PropertyQueryServiceModel All(
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
            int propertiesPerPage)
        {
            var propertiesQuery = this.data.Properties.AsQueryable();



            if (!string.IsNullOrWhiteSpace(keyword))
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.Description.ToLower().Contains(keyword.ToLower()));
            }

            if (districtId != 0)
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.DistrictId == districtId);
            }

            if (buildingTypeId != 0)
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.BuildingTypeId == buildingTypeId);
            }

            if (propertyTypeId != 0)
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.PropertyTypeId == propertyTypeId);
            }

            if (minPrice != 0)
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.Price >= minPrice);
            }

            if (maxPrice != 0)
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.Price <= maxPrice);
            }

            if (minSize != 0)
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.Size >= minSize);
            }

            if (maxSize != 0)
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.Size <= maxSize);
            }

            if (minYear != 0)
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.Year >= minYear);
            }

            if (maxYear != 0)
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.Year <= maxYear);
            }

            if (minFloor != 0)
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.Floor >= minFloor);
            }

            if (maxFloor != 0)
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.Floor <= maxFloor);
            }

            var totalProperties = propertiesQuery.Count();


            var properties = GetProperties(propertiesQuery
                             .OrderByDescending(p => p.Id)
                             .Skip((currentPage - 1) * propertiesPerPage)
                             .Take(propertiesPerPage));
                                  

            return new PropertyQueryServiceModel
            {
                TotalProperties = totalProperties,
                CurrentPage = currentPage,
                PropertiesPerPage = propertiesPerPage,
                Properties = properties
            };
        }

        public IEnumerable<PropertyServiceModel> ByUsers(string userId)
        => this.GetProperties(this.data
            .Properties
            .Where(p => p.Dealer.UserId == userId));


        private IEnumerable<PropertyServiceModel> GetProperties(IQueryable<Property> propertyQuery)
        => propertyQuery
                         .Select(x => new PropertyServiceModel
                         {

                             Size = x.Size,
                             Floor = x.Floor,
                             TotalNumberOfFloor = x.TotalNumberOfFloor,
                             Year = x.Year,
                             District = x.District.Name,
                             PropertyType = x.PropertyType.Name,
                             BuildingType = x.BuildingType.Name,
                             Price = x.Price,
                             Description = x.Description,
                             Images = x.PropertyImages
                                                                .Select(i => i.Image.Content)
                                                                .ToList()

                         }).ToList();
    }
}
