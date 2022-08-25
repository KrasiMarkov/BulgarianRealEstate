using AutoMapper;
using BulgarianRealEstate.Services.Dealers;
using BulgarianRealEstate.Services.Properties;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Areas.Admin.Controllers
{
    public class PropertiesController : AdminController
    {
        private readonly IPropertyService properties;
        

        public PropertiesController(IPropertyService properties)
        {

            this.properties = properties;
            
        }


        public IActionResult All() 
        {
            var properties = this.properties.All(publicOnly: false).Properties;
             
            return View(properties);
        }

        public IActionResult ChangeVisibility(int id) 
        {
            this.properties.ChangeVisibility(id);

            return RedirectToAction(nameof(All));
        }
    }
}
