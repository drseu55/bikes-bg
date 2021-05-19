﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using bikes_bg.Models.Base;

namespace bikes_bg.Models
{
    [Table("REGIONS")]
    public class Region : BaseEntity
    {
        public virtual List<City> cities { get; set; }
    }
}
