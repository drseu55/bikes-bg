using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bikes_bg.Models
{
    [Table("BIKE_BRANDS")]
    public class BikeBrand
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
