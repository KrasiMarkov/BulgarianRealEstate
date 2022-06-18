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

        public int GetIdByUser(string userId)
          => this.data
                 .Dealers
                 .Where(d => d.UserId == userId)
                 .Select(d => d.Id)
                 .FirstOrDefault();


        public bool IsDealer(string userId)
            => this.data
                   .Dealers
                   .Any(d => d.UserId == userId);
                   
       
    }
}
