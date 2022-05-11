using BulgarianRealEstate.Data;
using BulgarianRealEstate.Models.Api.Properties;
using BulgarianRealEstate.Services.Properties;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Controllers.Api
{
    [ApiController]
    [Route("api/properties")]
    public class PropertiesApiController
    {
        private readonly IPropertyService properties;
        public PropertiesApiController(IPropertyService properties)
        {
            this.properties = properties;
        }

        [HttpGet]
        public PropertyQueryServiceModel All([FromQuery] AllPropertiesApiRequestModel query)
        {
            return this.properties.All(
            query.Keyword,
            query.DistrictId,
            query.BuildingTypeId,
            query.PropertyTypeId,
            query.MinPrice,
            query.MaxPrice,
            query.MinSize,
            query.MaxSize,
            query.MinYear,
            query. MaxYear,
            query.MinFloor,
            query.MaxFloor,
            query.CurrentPage,
            query.PropertiesPerPage);

        }

       
    }
}
