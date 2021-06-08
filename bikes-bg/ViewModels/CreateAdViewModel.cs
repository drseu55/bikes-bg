using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using bikes_bg.Models;
using Microsoft.AspNetCore.Http;

namespace bikes_bg.ViewModels
{
    public class CreateAdViewModel
    {
        public List<BikeBrand> bikeBrands { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select bike brand")]
        [DisplayName("Brand")]
        public int selectedBikeBrand { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select bike model")]
        [DisplayName("Model")]
        public int selectedBikeModel { get; set; }

        public List<BikeCategory> bikeCategories { get; set; }
        [DisplayName("Category")]
        public int selectedBikeCategory { get; set; }

        public List<BikeEngineType> bikeEngineTypes { get; set; }
        [DisplayName("Engine type")]
        public int selectedBikeEngineType { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Engine size must be greater than 0")]
        [DisplayName("Engine size")]
        public int bikeEngineSize { get; set; }

        public List<BikeColor> bikeColors { get; set; }
        [DisplayName("Color")]
        public int selectedBikeColor { get; set; }

        public List<Region> regions { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select region")]
        [DisplayName("Region")]
        public int selectedRegion { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select city")]
        [DisplayName("City")]
        public int selectedCity { get; set; }

        [DisplayName("Production year")]
        public int bikeYear { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Mileage must be greater than 0")]
        [DisplayName("Mileage (in Kilometers)")]
        public int bikeMileage { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Price must be 0 or greater than 0")]
        [DisplayName("Price")]
        public double bikePrice { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Horsepower must be greater than 0")]
        [DisplayName("Horse Power")]
        public int bikeHorsePower { get; set; }

        [Required]
        [DisplayName("Description")]
        public string description { get; set; }

        [DisplayName("Photo")]
        public IFormFile photo { get; set; }

        public string advertisementUserId { get; set; }
    }
}
