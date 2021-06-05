using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace bikes_bg.Models
{
    public class AdSearchFilter
    {
        public List<BikeBrand> bikeBrands { get; set; }
        public List<Region> regions { get; set; }


        public int ?modelId { get; set; }
        [DisplayName("Brand")]
        public int ?brandId { get; set; }
        [DisplayName("Price from")]
        public int ?priceFrom { get; set; }
        [DisplayName("Price to")]
        public int ?priceTo { get; set; }
        [DisplayName("Region")]
        public int ?regionId { get; set; }
        [DisplayName("City")]
        public int ?cityId { get; set; }
    }
}
