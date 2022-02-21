using BulgarianRealEstate.Data;
using BulgarianRealEstate.Data.Models;
using BulgarianRealEstate.Models.Properties;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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



        public IActionResult Add() => View(new AddPropertyFormModel
        {
            
            PropertyTypes = this.GetPropertyTypes(),
            Districts = this.GetDistricts(),
            BuildingTypes = this.GetBuildingTypes()
        });

        [HttpPost]
        public IActionResult Add(AddPropertyFormModel property)
        {
            if (!this.data.PropertyTypes.Any(p => p.Id == property.PropertyTypeId)) 
            {
                this.ModelState.AddModelError(nameof(property.PropertyTypeId), "The category does not exist");
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
