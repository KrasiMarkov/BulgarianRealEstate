using BulgarianRealEstate.Data;
using BulgarianRealEstate.Data.Models;
using BulgarianRealEstate.Infrastructure;
using BulgarianRealEstate.Models.Properties;
using BulgarianRealEstate.Services.Properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly IPropertyService properties;
        private readonly RealEstateDbContext data;

        public PropertiesController(RealEstateDbContext data, IPropertyService properties)
        {
            this.data = data;
            this.properties = properties;
        }


        public IActionResult All([FromQuery] AllPropertyQueryModel query) 
        {

            var queryResults = this.properties.All(
            query.Keyword,
            query.DistrictId,
            query.BuildingTypeId,
            query.PropertyTypeId,
            query.MinPrice,
            query.MaxPrice,
            query.MinSize,
            query.MaxSize,
            query.MinYear,
            query.MaxYear,
            query.MinFloor,
            query.MaxFloor,
            query.CurrentPage,
            AllPropertyQueryModel.PropertiesPerPage);


                query.Properties = queryResults.Properties;
                query.TotalProperties = queryResults.TotalProperties;
                query.BuildingTypes = this.GetBuildingTypes();
                query.PropertyTypes = this.GetPropertyTypes();
                query.Districts = this.GetDistricts();

            return View(query);

        }

        [Authorize]
        public IActionResult Add()
        {

            if (!this.UserIsDealer()) 
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }
    

            return View(new AddPropertyFormModel
            {

                PropertyTypes = this.GetPropertyTypes(),
                Districts = this.GetDistricts(),
                BuildingTypes = this.GetBuildingTypes()
            });
        }

        
        [HttpPost]
        [Authorize]
        public IActionResult Add(AddPropertyFormModel property, List<IFormFile> images)
        {
            var dealerId = this.data
                               .Dealers
                               .Where(d => d.UserId == this.User.GetId())
                               .Select(d => d.Id)
                               .FirstOrDefault();

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

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
                Description = property.Description,
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

        private bool UserIsDealer() 
        => this.data.Dealers.Any(d => d.UserId == this.User.GetId());




    }
}
