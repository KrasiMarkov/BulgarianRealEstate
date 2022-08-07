using BulgarianRealEstate.Data;
using BulgarianRealEstate.Data.Models;
using BulgarianRealEstate.Infrastructure;
using BulgarianRealEstate.Models.Dealers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BulgarianRealEstate.WebConstants;

namespace BulgarianRealEstate.Controllers
{
    public class DealersController : Controller
    {
        private readonly RealEstateDbContext data;

        public DealersController(RealEstateDbContext data)
        {
            this.data = data; 
        }

        [Authorize]
        public IActionResult Become() 
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Become(BecomeDealerFormModel dealer) 
        {
            var userId = this.User.GetId();

            var userIsAlreadyDealer = this.data.Dealers.Any(d => d.UserId == userId);

            if (userIsAlreadyDealer) 
            {
                return BadRequest();
            }

            if (!ModelState.IsValid) 
            {
                return View(dealer);
            }

            var dealerData = new Dealer
            {
                Name = dealer.Name,
                PhoneNumber = dealer.PhoneNumber,
                UserId = userId
            };

            this.data.Dealers.Add(dealerData);
            this.data.SaveChanges();

            TempData[GlobalMessageKey] = "Thank you for becomming a dealer!";

            return RedirectToAction("All", "Properties");
        }

    }
}
