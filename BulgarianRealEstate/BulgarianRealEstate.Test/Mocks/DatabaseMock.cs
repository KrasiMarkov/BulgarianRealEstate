using BulgarianRealEstate.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Test.Mocks
{
    public static class DatabaseMock
    {
        public static RealEstateDbContext Instance 
        {
            get 
            {
                var dbContextOptions = new DbContextOptionsBuilder<RealEstateDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new RealEstateDbContext(dbContextOptions);
            }
        }

            

    }
}
