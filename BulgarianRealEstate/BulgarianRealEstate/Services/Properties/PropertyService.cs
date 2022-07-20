using BulgarianRealEstate.Data;
using BulgarianRealEstate.Data.Models;
using BulgarianRealEstate.Models.Properties;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
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

        public bool BuildingTypeExists(int buildingTypeId)
           => this.data.BuildingTypes.Any(b => b.Id == buildingTypeId);


        public bool DistrictExists(int districtId)
           => this.data.Districts.Any(d => d.Id == districtId);

        public bool PropertyTypeExists(int propertyTypeId)
           => this.data.PropertyTypes.Any(p => p.Id == propertyTypeId);


        public IEnumerable<PropertyServiceModel> ByUsers(string userId)
        => this.GetProperties(this.data
            .Properties
            .Where(p => p.Dealer.UserId == userId));

       
        

        public IEnumerable<BuildingTypeServiceModel> GetBuildingTypes()
       => this.data
                .BuildingTypes
                .Select(b => new BuildingTypeServiceModel
                {
                    Id = b.Id,
                    Name = b.Name
                })
                .ToList();

        public IEnumerable<DistrictServiceModel> GetDistricts()
        => this.data
               .Districts
               .Select(d => new DistrictServiceModel
               {
                   Id = d.Id,
                   Name = d.Name
               })
               .ToList();

        public IEnumerable<PropertyTypeServiceModel> GetPropertyTypes()
        => this.data
                .PropertyTypes
                .Select(p => new PropertyTypeServiceModel
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToList();

      
        

        private IEnumerable<PropertyServiceModel> GetProperties(IQueryable<Property> propertyQuery)
        => propertyQuery
                         .Select(x => new PropertyServiceModel
                         {
                             Id = x.Id,
                             Size = x.Size,
                             Floor = x.Floor,
                             TotalNumberOfFloor = x.TotalNumberOfFloor,
                             Year = x.Year,
                             DistrictName = x.District.Name,
                             PropertyTypeName = x.PropertyType.Name,
                             BuildingTypeName = x.BuildingType.Name,
                             Price = x.Price,
                             Description = x.Description,
                             Images = x.PropertyImages
                                                                .Select(i => i.Image.Content)
                                                                .ToList()

                         }).ToList();

        public int Create(
            int size, 
            int floor, 
            int totalNumberOfFloor, 
            int year, int districtId, 
            int propertyTypeId, 
            int buildingTypeId, 
            int price, 
            string description, 
            int dealerId,
            List<IFormFile> images)
        {

            var propertyData = new Property
            {
                Size = size,
                Floor = floor,
                TotalNumberOfFloor = totalNumberOfFloor,
                Year = year,
                DistrictId = districtId,
                PropertyTypeId = propertyTypeId,
                BuildingTypeId = buildingTypeId,
                Price = price,
                Description = description,
                DealerId = dealerId

            };


            foreach (var image in images)
            {
                var imageInMemory = new MemoryStream();
                image.CopyTo(imageInMemory);
                var imageBytes = imageInMemory.ToArray();

                var imageData = new Image
                {
                    Content = imageBytes
                };

                this.data.Images.Add(imageData);
                this.data.SaveChanges();


                propertyData.PropertyImages.Add(new PropertyImage
                {
                    ImageId = imageData.Id
                });
            }

            this.data.Properties.Add(propertyData);
            this.data.SaveChanges();

            return propertyData.Id;
        }

        public PropertyDetailsServiceModel Details(int id)
           => this.data
                  .Properties
                  .Where(p => p.Id == id)
                  .Select(p => new PropertyDetailsServiceModel
                  {
                      Size = p.Size,
                      Floor = p.Floor,
                      TotalNumberOfFloor = p.TotalNumberOfFloor,
                      Year = p.Year,
                      DistrictName = p.District.Name,
                      PropertyTypeName = p.PropertyType.Name,
                      BuildingTypeName = p.BuildingType.Name,
                      Description = p.Description,
                      Price = p.Price,
                      DealerId = p.DealerId,
                      DealerName = p.Dealer.Name,
                      UserId = p.Dealer.UserId,
                      Images = p.PropertyImages
                                              .Select(i => i.Image.Content)
                                              .ToList()

                  })
                  .FirstOrDefault();

        public bool Edit(int id,
            int size,
            int floor,
            int totalNumberOfFloor,
            int year,
            int districtId,
            int propertyTypeId,
            int buildingTypeId,
            int price,
            string description,
            List<IFormFile> images)
        {

            var propertyData = this.data.Properties.Find(id);

            if (propertyData == null) 
            {
                return false;
            }

           

            propertyData.Size = size;
            propertyData.Floor = floor;
            propertyData.TotalNumberOfFloor = totalNumberOfFloor;
            propertyData.Year = year;
            propertyData.DistrictId = districtId;
            propertyData.PropertyTypeId = propertyTypeId;
            propertyData.BuildingTypeId = buildingTypeId;
            propertyData.Price = price;
            propertyData.Description = description;


            foreach (var image in images)
            {
                var imageInMemory = new MemoryStream();
                image.CopyTo(imageInMemory);
                var imageBytes = imageInMemory.ToArray();

                var imageData = new Image
                {
                    Content = imageBytes
                };

               
                this.data.SaveChanges();


                propertyData.PropertyImages.Add(new PropertyImage
                {
                    ImageId = imageData.Id
                });
            }

            
            this.data.SaveChanges();

            return true;
            
        }

        public bool IsByDealer(int propertyId, int dealerId)
          => this.data
                 .Properties
                 .Any(p => p.Id == propertyId && p.DealerId == dealerId);
    }
}
