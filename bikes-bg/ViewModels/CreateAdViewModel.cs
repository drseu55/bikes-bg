using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using bikes_bg.Models;

namespace bikes_bg.ViewModels
{
    public class CreateAdViewModel
    {
        public List<BikeBrand> bikeBrands { get; set; }
        [DisplayName("Brand")]
        public int selectedBikeBrand { get; set; }

        [DisplayName("Model")]
        public int selectedBikeModel { get; set; }

        public List<BikeCategory> bikeCategories { get; set; }
        [DisplayName("Category")]
        public int selectedBikeCategory { get; set; }

        public List<BikeEngineType> bikeEngineTypes { get; set; }
        [DisplayName("Engine type")]
        public int selectedBikeEngineType { get; set; }

        [DisplayName("Engine size")]
        public int bikeEngineSize { get; set; }

        public List<BikeColor> bikeColors { get; set; }
        [DisplayName("Color")]
        public int selectedBikeColor { get; set; }

        public List<Region> regions { get; set; }
        [DisplayName("Region")]
        public int selectedRegion { get; set; }

        [DisplayName("City")]
        public int selectedCity { get; set; }

        [DisplayName("Production year")]
        public int bikeYear { get; set; }

        [DisplayName("Mileage (in Kilometers)")]
        public int bikeMileage { get; set; }

        [DisplayName("Price")]
        public double bikePrice { get; set; }

        [DisplayName("Horse Power")]
        public int bikeHorsePower { get; set; }

        [DisplayName("Description")]
        public string description { get; set; }
    }
}
