using AutoMapper;
using BulgarianRealEstate.Models.Properties;
using BulgarianRealEstate.Services.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            this.CreateMap<PropertyDetailsServiceModel, PropertyFormModel>();
        }

    }
}
