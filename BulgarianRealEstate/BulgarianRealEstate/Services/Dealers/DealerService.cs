using BulgarianRealEstate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Services.Dealers
{
    public class DealerService : IDealerService
    {
        private readonly RealEstateDbContext data;

        public DealerService(RealEstateDbContext data)
        {
            this.data = data;
        }
        public bool IsDealer(string userId)
            => this.data
                   .Dealers
                   .Any(d => d.UserId == userId);
                   
       
    }
}
