using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using bikes_bg.Models.Base;

namespace bikes_bg.Models
{
    [Table("BIKE_MODELS")]
    public class BikeModel : BaseEntity
    {
        [Column("BRAND_ID")]
        public int brandID { get; set; }
        
        public virtual BikeBrand bikeBrand { get; set; }
    }
}
