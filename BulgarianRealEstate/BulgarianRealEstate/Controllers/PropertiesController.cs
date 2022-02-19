using BulgarianRealEstate.Views.RealEstates;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Controllers
{
    public class PropertiesController : Controller
    {
        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddPropertyFormModel property) => View();

    }
}
