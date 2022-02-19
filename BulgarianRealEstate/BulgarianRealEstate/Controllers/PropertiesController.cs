using BulgarianRealEstate.Data;
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
            return View();
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
