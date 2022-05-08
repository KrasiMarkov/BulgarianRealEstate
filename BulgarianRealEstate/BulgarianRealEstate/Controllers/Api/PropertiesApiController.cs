using BulgarianRealEstate.Data;
using BulgarianRealEstate.Models.Api.Properties;
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
        private readonly RealEstateDbContext data;
        public PropertiesApiController(RealEstateDbContext data)
        {
            this.data = data;
        }

        [HttpGet]
        public ActionResult<AllPropertiesApiResponseModel> All([FromQuery] AllPropertiesApiRequestModel query)
        {

            var propertiesQuery = this.data.Properties.AsQueryable();



            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.Description.ToLower().Contains(query.Keyword.ToLower()));
            }

            if (query.DistrictId != 0)
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.DistrictId == query.DistrictId);
            }

            if (query.BuildingTypeId != 0)
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.BuildingTypeId == query.BuildingTypeId);
            }

            if (query.PropertyTypeId != 0)
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.PropertyTypeId == query.PropertyTypeId);
            }

            if (query.MinPrice != 0)
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.Price >= query.MinPrice);
            }

            if (query.MaxPrice != 0)
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.Price <= query.MaxPrice);
            }

            if (query.MinSize != 0)
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.Size >= query.MinSize);
            }

            if (query.MaxSize != 0)
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.Size <= query.MaxSize);
            }

            if (query.MinYear != 0)
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.Year >= query.MinYear);
            }

            if (query.MaxYear != 0)
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.Year <= query.MaxYear);
            }

            if (query.MinFloor != 0)
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.Floor >= query.MinFloor);
            }

            if (query.MaxFloor != 0)
            {
                propertiesQuery = propertiesQuery.Where(p =>
                p.Floor <= query.MaxFloor);
            }

            var totalProperties = propertiesQuery.Count();

            var properties = propertiesQuery
                                    .OrderByDescending(p => p.Id)
                                    .Skip((query.CurrentPage - 1) * query.PropertiesPerPage)
                                    .Take(query.PropertiesPerPage)
                                    .Select(x => new PropertyResponseModel
                                    {

                                        Size = x.Size,
                                        Floor = x.Floor,
                                        TotalNumberOfFloor = x.TotalNumberOfFloor,
                                        Year = x.Year,
                                        District = x.District.Name,
                                        PropertyType = x.PropertyType.Name,
                                        BuildingType = x.BuildingType.Name,
                                        Price = x.Price,
                                        Description = x.Description,
                                      
                                    }).ToList();


            return new AllPropertiesApiResponseModel
            {
                CurrentPage = query.CurrentPage,
                PropertiesPerPage = query.PropertiesPerPage,
                TotalProperties = totalProperties,
                Properties = properties
            };

        }

       
    }
}
