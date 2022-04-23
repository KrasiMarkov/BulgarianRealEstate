using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulgarianRealEstate.Data
{
    public static class DataConstants
    {
        public class Property 
        {
            public const int SizeMinValue = 1;
            public const int SizeMaxValue = 500;
            public const int FloorMinValue = 1;
            public const int FloorMaxValue = 25;
            public const int TotalNumberOfFloorMinValue = 1;
            public const int TotalNumberOfFloorMaxValue = 25;
            public const int YearMinValue = 1950;
            public const int YearMaxValue = 2022;
            public const int PriceMinValue = 50000;
            public const int PriceMaxValue = 400000;
            public const int DescriptionMinLength = 10;
            public const int DescriptionMaxLength = 2000;
           
        }

        public class PropertyType 
        {
            public const int NameMaxLength = 25;
        }

        public class BuildingType 
        {
            public const int NameMaxLength = 25;
        }
        public class District 
        {
            public const int NameMaxLength = 25;
        }
        public class Dealer 
        {
            public const int NameMaxLength = 25;
            public const int PhoneNumberMaxLength = 30;
        }

        

    }
}
