﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static BulgarianRealEstate.Data.DataConstants.User;

namespace BulgarianRealEstate.Data.Models
{
    public class User : IdentityUser
    {
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }


    }
}
