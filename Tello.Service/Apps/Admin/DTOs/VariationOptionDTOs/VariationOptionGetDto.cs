﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tello.Core.Entities;
using Tello.Service.Apps.Admin.DTOs.VariationCategoryDTOs;

namespace Tello.Service.Apps.Admin.DTOs.VariationOptionDTOs
{
    public class VariationOptionGetDto
    {
        public string Value { get; set; }
        //public Variation Variation { get; set; }
        //public Category Category { get; set; }
        public VariationCategoryGetDto VariationCategory { get; set; }
    }
}
