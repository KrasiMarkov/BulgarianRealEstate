using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Services.Dealers
{
    public interface IDealerService
    {
       public bool IsDealer(string userId);

        public int IdByUser(string userId);

    }
}
