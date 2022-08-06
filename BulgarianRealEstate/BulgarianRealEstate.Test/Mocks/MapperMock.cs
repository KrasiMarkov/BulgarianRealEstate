using AutoMapper;
using BulgarianRealEstate.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Test.Mocks
{
    public static class MapperMock
    {
        public static IMapper Instance
        {
            get 
            {
                var mapperConfiguration = new MapperConfiguration(config =>
                {
                    config.AddProfile<MappingProfile>();
                });

                return new Mapper(mapperConfiguration);
            }
        }

    }
}
