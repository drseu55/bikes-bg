using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bikes_bg.Models;
using bikes_bg.Repository.Base;
using bikes_bg.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace bikes_bg.Controllers
{
    public class AdvertisementController : Controller
    {
        private IGenericRepository<BikeModel> bikeModelRepo;
        private IGenericRepository<BikeBrand> bikeBrandRepo;
        private IGenericRepository<BikeCategory> bikeCategoryRepo;
        private IGenericRepository<BikeEngineType> bikeEngineTypeRepo;
        private IGenericRepository<BikeColor> bikeColorRepo;
        private IGenericRepository<Region> regionRepo;
        private IGenericRepository<City> cityRepo;

        public AdvertisementController(IGenericRepository<BikeModel> bikeModelRepo
            , IGenericRepository<BikeBrand> bikeBrandRepo
            , IGenericRepository<BikeCategory> bikeCategoryRepo
            , IGenericRepository<BikeEngineType> bikeEngineTypeRepo
            , IGenericRepository<BikeColor> bikeColorRepo
            , IGenericRepository<Region> regionRepo
            , IGenericRepository<City> cityRepo)
        {
            this.bikeModelRepo = bikeModelRepo;
            this.bikeBrandRepo = bikeBrandRepo;
            this.bikeCategoryRepo = bikeCategoryRepo;
            this.bikeEngineTypeRepo = bikeEngineTypeRepo;
            this.bikeColorRepo = bikeColorRepo;
            this.regionRepo = regionRepo;
            this.cityRepo = cityRepo;
        }

    public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Create()
        {
            CreateAdViewModel model = new CreateAdViewModel();
            model.bikeBrands = bikeBrandRepo.GetAll().ToList();
            model.bikeCategories = bikeCategoryRepo.GetAll().ToList();
            model.bikeEngineTypes = bikeEngineTypeRepo.GetAll().ToList();
            model.regions = regionRepo.GetAll().ToList();
            model.bikeColors = bikeColorRepo.GetAll().ToList();

            return View("CreateAd", model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(CreateAdViewModel model)
        {
            if (!ModelState.IsValid)
                return View("CreateAd");

            Advertisement advertisement = new Advertisement
            {
                modelId = model.selectedBikeModel,
                horsePower = model.bikeHorsePower,
                engineSize = model.bikeEngineSize,
                engineTypeId = model.selectedBikeEngineType,
                productionYear = model.bikeYear,
                mileage = model.bikeMileage,
                price = model.bikePrice,
                cityId = model.selectedCity,
                colorId = model.selectedBikeColor,
                categoryId = model.selectedBikeCategory,
                description = model.description
            };

            // Todo attach user and insert ad to database
            // remove anonymous access

            return View("CreateAd");
        }

        [HttpPost]
        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetModels(int id)
        {
            List<BikeModel> bikeModels = bikeModelRepo.GetAll().Where(model => (model.brandID == id)).ToList();
            IEnumerable<SelectListItem> selectList = bikeModels.Select(model => new SelectListItem
            {
                Value = model.id.ToString(),
                Text = model.name
            }).ToList() ;

            return Json(selectList);
        }

        [HttpPost]
        [HttpGet]
        [AllowAnonymous]
        public JsonResult GetCities(int id)
        {
            List<City> cities = cityRepo.GetAll().Where(model => (model.regionID == id)).ToList();
            IEnumerable<SelectListItem> selectList = cities.Select(model => new SelectListItem
            {
                Value = model.id.ToString(),
                Text = model.name
            }).ToList();

            return Json(selectList);
        }
    }
}
