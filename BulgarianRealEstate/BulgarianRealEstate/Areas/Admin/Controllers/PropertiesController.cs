using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Areas.Admin.Controllers
{
    public class PropertiesController : AdminController
    {
        public IActionResult Index() 
        {
            return View();
        }

    }
}
