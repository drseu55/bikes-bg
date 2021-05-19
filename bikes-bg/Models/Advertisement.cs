using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace bikes_bg.Models
{
    [Table("ADVERTISEMENTS")]
    public class Advertisement
    {
        [Column ("ID")]
        public int id { get; set; }
        [Column ("MODEL_ID")]
        public int modelId { get; set; }
        [Column ("ID")]
        public virtual BikeModel bikeModel { get; set; }
        [Column ("PHOTO_PATH")]
        public string photoPath { get; set; }
        [Column ("EXTRAS")]
        public byte[] extras { get; set; }
        [Column ("HORSEPOWER")]
        public int horsePower { get; set; }
        [Column ("ENGINE_SIZE")]
        public int engineSize { get; set; }
        [Column ("ENGINE_TYPE_ID")]
        public int engineTypeId { get; set; }
        public virtual BikeEngineType bikeEngineType { get; set; }
        [Column ("PRODUCTION_YEAR")]
        public int productionYear { get; set; }
        [Column ("MILEAGE")]
        public int mileage { get; set; }
        [Column ("PRICE")]
        public double price { get; set; }
        [Column ("CITY_ID")]
        public int cityId { get; set; }
        public virtual City city { get; set; }
        [Column ("COLOR_ID")]
        public int colorId { get; set; }
        public virtual BikeColor bikeColor { get; set; }
        [Column ("CATEGORY_ID")]
        public int categoryId { get; set; }
        public virtual BikeCategory bikeCategory { get; set; }
        [Column ("DESCRIPTION")]
        public string description { get; set; }
        [Column ("USER_ID")]
        public string userId { get; set; }
    }
}
