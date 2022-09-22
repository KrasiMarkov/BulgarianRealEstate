using AutoMapper;
using BulgarianRealEstate.Data;
using BulgarianRealEstate.Data.Models;
using BulgarianRealEstate.Infrastructure;
using BulgarianRealEstate.Models.Properties;
using BulgarianRealEstate.Services.Dealers;
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
using static BulgarianRealEstate.WebConstants;

namespace BulgarianRealEstate.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly IPropertyService properties;
        private readonly IDealerService dealers;
        private readonly IMapper mapper;


        public PropertiesController(IPropertyService properties, IDealerService dealers, IMapper mapper)
        {

            this.properties = properties;
            this.dealers = dealers;
            this.mapper = mapper;
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
            query.BuildingTypes = this.properties.GetBuildingTypes();
            query.PropertyTypes = this.properties.GetPropertyTypes();
            query.Districts = this.properties.GetDistricts();

            return View(query);

        }

        [Authorize]
        public IActionResult Add()
        {

            if (!this.dealers.IsDealer(this.User.GetId()))
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }


            return View(new PropertyFormModel
            {

                PropertyTypes = this.properties.GetPropertyTypes(),
                Districts = this.properties.GetDistricts(),
                BuildingTypes = this.properties.GetBuildingTypes()
            });
        }


        [HttpPost]
        [Authorize]
        public IActionResult Add(PropertyFormModel property, List<IFormFile> images)
        {
            var dealerId = dealers.IdByUser(this.User.GetId());

            if (dealerId == 0)
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (!this.properties.PropertyTypeExists(property.PropertyTypeId))
            {
                this.ModelState.AddModelError(nameof(property.PropertyTypeId), "The category does not exist");
            }

            if (!this.properties.DistrictExists(property.DistrictId))
            {
                this.ModelState.AddModelError(nameof(property.DistrictId), "The category does not exist");
            }

            if (!this.properties.BuildingTypeExists(property.BuildingTypeId))
            {
                this.ModelState.AddModelError(nameof(property.BuildingTypeId), "The category does not exist");
            }

            if (images == null || images.Any(x => x.Length > 2 * 1024 * 1024))
            {
                this.ModelState.AddModelError("Image", "The image is not valid. It is required and it should be less than 2 MB.");
            }

            if (!ModelState.IsValid)
            {
                property.BuildingTypes = this.properties.GetBuildingTypes();
                property.Districts = this.properties.GetDistricts();
                property.PropertyTypes = this.properties.GetPropertyTypes();

                return View(property);
            }

            var propertyId = this.properties.Create(
                property.Size,
                property.Floor,
                property.TotalNumberOfFloor,
                property.Year,
                property.DistrictId,
                property.PropertyTypeId,
                property.BuildingTypeId,
                property.Price,
                property.Description,
                dealerId,
                images);

            TempData[GlobalMessageKey] = "Your property was added and is awaiting approval!";

            return RedirectToAction(nameof(Details), new { id = propertyId, information = property.GetInformation()});
        }

        [Authorize]
        public IActionResult Mine()
        {

            var myProperties = this.properties.ByUsers(this.User.GetId());

            return View(myProperties);
        }

        public IActionResult Details(int id, string information)
        {
            var property = this.properties.Details(id);

            if (information != property.GetInformation()) 
            {
                return BadRequest();
            }

            return View(property);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!this.dealers.IsDealer(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            var property = this.properties.Details(id);

            if (property.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var propertyForm = this.mapper.Map<PropertyFormModel>(property);

            propertyForm.PropertyTypes = this.properties.GetPropertyTypes();
            propertyForm.Districts = this.properties.GetDistricts();
            propertyForm.BuildingTypes = this.properties.GetBuildingTypes();

            return View(propertyForm);
           
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, PropertyFormModel property, List<IFormFile> images)
        {
            var dealerId = dealers.IdByUser(this.User.GetId());

            if (dealerId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (!this.properties.PropertyTypeExists(property.PropertyTypeId))
            {
                this.ModelState.AddModelError(nameof(property.PropertyTypeId), "The category does not exist");
            }

            if (!this.properties.DistrictExists(property.DistrictId))
            {
                this.ModelState.AddModelError(nameof(property.DistrictId), "The category does not exist");
            }

            if (!this.properties.BuildingTypeExists(property.BuildingTypeId))
            {
                this.ModelState.AddModelError(nameof(property.BuildingTypeId), "The category does not exist");
            }

            if (images == null || images.Any(x => x.Length > 2 * 1024 * 1024))
            {
                this.ModelState.AddModelError("Image", "The image is not valid. It is required and it should be less than 2 MB.");
            }

            if (!ModelState.IsValid)
            {
                property.BuildingTypes = this.properties.GetBuildingTypes();
                property.Districts = this.properties.GetDistricts();
                property.PropertyTypes = this.properties.GetPropertyTypes();

                return View(property);
            }

            if (!this.properties.IsByDealer(id, dealerId) && !User.IsAdmin()) 
            {
                return BadRequest();
            }

            var edited = this.properties.Edit(id,
                property.Size,
                property.Floor,
                property.TotalNumberOfFloor,
                property.Year,
                property.DistrictId,
                property.PropertyTypeId,
                property.BuildingTypeId,
                property.Price,
                property.Description,
                images);

            if (!edited)
            {
                return BadRequest();
            }

            TempData[GlobalMessageKey] = $"Your property was edited {(this.User.IsAdmin() ? string.Empty : " and is awaiting approval")}!";

            return RedirectToAction(nameof(Details), new { id, information = property.GetInformation() });
        }

        public IActionResult Delete(int id)
        {
            var dealerId = dealers.IdByUser(this.User.GetId());

            if (dealerId == 0 && !User.IsAdmin())
            {
                return RedirectToAction(nameof(DealersController.Become), "Dealers");
            }

            if (!this.properties.IsByDealer(id, dealerId) && !User.IsAdmin())
            {
                return BadRequest();
            }

            var deletedProperty = this.properties.Delete(id);

            if (!deletedProperty) 
            {
                return BadRequest();
            }


            TempData[GlobalMessageKey] = $"Your property was deleted!";


            return RedirectToAction(nameof(PropertiesController.Mine), "Properties");

        }

    }
}
