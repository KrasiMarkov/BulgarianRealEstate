using BulgarianRealEstate.Data;
using BulgarianRealEstate.Data.Models;
using BulgarianRealEstate.Models.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly RealEstateDbContext data;

        public PropertiesController(RealEstateDbContext data)
        {
            this.data = data;
        }


        public IActionResult All(string keyword, int districtId, int buildingTypeId, int propertyTypeId,
           int minPrice, int maxPrice, int minSize) 
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
                p.Price >= minPrice );
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

            var properties = propertiesQuery
                                    .OrderByDescending(p => p.Id)
                                    .Select(x => new PropertyListingViewModel
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

            return View(new AllPropertyQueryModel
            {
                Keyword = keyword,
                Properties = properties,
                BuildingTypes = this.GetBuildingTypes(),
                PropertyTypes = this.GetPropertyTypes(),
                Districts = this.GetDistricts()
            });

        }


        public IActionResult Add() => View(new AddPropertyFormModel
        {
            
            PropertyTypes = this.GetPropertyTypes(),
            Districts = this.GetDistricts(),
            BuildingTypes = this.GetBuildingTypes()
        });

        [HttpPost]
        public IActionResult Add(AddPropertyFormModel property, List<IFormFile> images)
        {
            if (!this.data.PropertyTypes.Any(p => p.Id == property.PropertyTypeId)) 
            {
                this.ModelState.AddModelError(nameof(property.PropertyTypeId), "The category does not exist");
            }

            if (images == null || images.Any(x => x.Length > 2 * 1024 * 1024))
            {
                this.ModelState.AddModelError("Image", "The image is not valid. It is required and it should be less than 2 MB.");
            }

            if (!ModelState.IsValid) 
            {
                property.BuildingTypes = this.GetBuildingTypes();
                property.Districts = this.GetDistricts();
                property.PropertyTypes = this.GetPropertyTypes();

                return View(property);
            }

            var propertyData = new Property
            {
                Size = property.Size,
                Floor = property.Floor,
                TotalNumberOfFloor = property.TotalNumberOfFloor,
                Year = property.Year,
                DistrictId = property.DistrictId,
                PropertyTypeId = property.PropertyTypeId,
                BuildingTypeId = property.BuildingTypeId,
                Price = property.Price,
                Description = property.Description

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

            return RedirectToAction("Index", "Home");
        }

        private IEnumerable<BuildingTypeViewModel> GetBuildingTypes()
            => this.data
                .BuildingTypes
                .Select(b => new BuildingTypeViewModel
                {
                    Id = b.Id,
                    Name = b.Name
                })
                .ToList();

        private IEnumerable<DistrictViewModel> GetDistricts()
           => this.data
               .Districts
               .Select(d => new DistrictViewModel
               {
                   Id = d.Id,
                   Name = d.Name
               })
               .ToList();

        private IEnumerable<PropertyTypeViewModel> GetPropertyTypes()
           => this.data
               .PropertyTypes
               .Select(p => new PropertyTypeViewModel
               {
                   Id = p.Id,
                   Name = p.Name
               })
               .ToList();

       
    }
}
