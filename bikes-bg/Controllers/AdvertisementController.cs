using System.Collections.Generic;
using System.Linq;
using bikes_bg.Models;
using bikes_bg.Repository.Base;
using bikes_bg.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;

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
        private IGenericRepository<Advertisement> advertisementRepo;

        public IHostingEnvironment hostingEnvironment { get; }

        public AdvertisementController(IGenericRepository<BikeModel> bikeModelRepo
            , IGenericRepository<BikeBrand> bikeBrandRepo
            , IGenericRepository<BikeCategory> bikeCategoryRepo
            , IGenericRepository<BikeEngineType> bikeEngineTypeRepo
            , IGenericRepository<BikeColor> bikeColorRepo
            , IGenericRepository<Region> regionRepo
            , IGenericRepository<City> cityRepo
            , IGenericRepository<Advertisement> advertisementRepo
            , IHostingEnvironment hostingEnvironment)
        {
            this.bikeModelRepo = bikeModelRepo;
            this.bikeBrandRepo = bikeBrandRepo;
            this.bikeCategoryRepo = bikeCategoryRepo;
            this.bikeEngineTypeRepo = bikeEngineTypeRepo;
            this.bikeColorRepo = bikeColorRepo;
            this.regionRepo = regionRepo;
            this.cityRepo = cityRepo;
            this.advertisementRepo = advertisementRepo;
            this.hostingEnvironment = hostingEnvironment;
        }

    public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
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
        public ActionResult Create(CreateAdViewModel model)
        {
            if (!ModelState.IsValid)
                return View("CreateAd");

            string uniqueFileName = ProcessUploadedFile(model);

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
                description = model.description,
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                photoPath = uniqueFileName
            };

            advertisementRepo.Insert(advertisement);

            return RedirectToAction("view", new { id = advertisement.id });
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult View(int id)
        {
            // This loading can be improved using eager loading
            var model = advertisementRepo.GetById(id);
            model.bikeModel = bikeModelRepo.GetById(model.modelId);
            model.bikeModel.bikeBrand = bikeBrandRepo.GetById(model.bikeModel.brandID);
            model.bikeCategory = bikeCategoryRepo.GetById(model.categoryId);
            model.city = cityRepo.GetById(model.cityId);
            model.bikeColor = bikeColorRepo.GetById(model.colorId);


            if (model == null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("index", "home");
            }

            return View("ViewAd", model);
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

        private string ProcessUploadedFile(CreateAdViewModel model)
        {
            string uniqueFileName = null;
            if (model.photo != null)
            {
                string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.photo.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
