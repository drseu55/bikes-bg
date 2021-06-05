using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using bikes_bg.Models;
using bikes_bg.Repository.Base;
using bikes_bg.Utils;
using bikes_bg.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bikes_bg.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        private IGenericRepository<User> userDetailsRepo;

        private readonly IGenericRepository<Advertisement> advertisementRepo;
        private IGenericRepository<BikeModel> bikeModelRepo;
        private IGenericRepository<BikeBrand> bikeBrandRepo;
        private IGenericRepository<BikeCategory> bikeCategoryRepo;
        private IGenericRepository<BikeEngineType> bikeEngineTypeRepo;
        private IGenericRepository<BikeColor> bikeColorRepo;
        private IGenericRepository<Region> regionRepo;
        private IGenericRepository<City> cityRepo;

        private IGenericRepository<CreateAdViewModel> createAdViewModelRepo;

        private IHostingEnvironment hostingEnvironment { get; }

        public AccountController(UserManager<User> userManager
            , SignInManager<User> signInManager
            , IGenericRepository<Advertisement> advertisementRepo
            , IGenericRepository<User> userDetailsRepo
            , IHostingEnvironment hostingEnvironment
            , IGenericRepository<BikeModel> bikeModelRepo
            , IGenericRepository<BikeBrand> bikeBrandRepo
            , IGenericRepository<BikeCategory> bikeCategoryRepo
            , IGenericRepository<BikeEngineType> bikeEngineTypeRepo
            , IGenericRepository<BikeColor> bikeColorRepo
            , IGenericRepository<Region> regionRepo
            , IGenericRepository<City> cityRepo
            , IGenericRepository<CreateAdViewModel> createAdViewModelRepo)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.advertisementRepo = advertisementRepo;
            this.userDetailsRepo = userDetailsRepo;
            this.hostingEnvironment = hostingEnvironment;
            this.bikeModelRepo = bikeModelRepo;
            this.bikeBrandRepo = bikeBrandRepo;
            this.bikeCategoryRepo = bikeCategoryRepo;
            this.bikeEngineTypeRepo = bikeEngineTypeRepo;
            this.bikeColorRepo = bikeColorRepo;
            this.regionRepo = regionRepo;
            this.cityRepo = cityRepo;
            this.createAdViewModelRepo = createAdViewModelRepo;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }

                    return RedirectToAction("index", "home");
                }


                ModelState.AddModelError(string.Empty, "Invalid login atempt");

            }

            return View(model);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(String email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use");
            }
        }

        [HttpGet]
        public ActionResult Profile(string id)
        {
            var model = new ProfileViewModel();

            User user = userDetailsRepo.GetById(id);

            List<Advertisement> advertisements = advertisementRepo.GetTable()
                .Where(e => (string)e.userId == id)
                .Include(ad => ad.bikeModel)
                .Include(ad => ad.bikeModel.bikeBrand)
                .ToList();

            model.user = user;
            model.advertisements = advertisements;

            if (model == null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("index", "home");
            }

            if(model.advertisements == null)
            {
                return View(model.user);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProfile(ProfileViewModel profileViewModel)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

                user.firstName = profileViewModel.user.firstName;
                user.lastName = profileViewModel.user.lastName;
                user.PhoneNumber = profileViewModel.user.PhoneNumber;

                string uniqueFileName = FileUpload.ProcessUploadedFile(profileViewModel.photo, hostingEnvironment, "images/profile");

                user.photoPath = uniqueFileName;

                var result = await userManager.UpdateAsync(user);

                if(result.Succeeded)
                {
                    return RedirectToAction("profile", new { id = user.Id });
                }
            }

            return View(profileViewModel);
        }

        [HttpPost]
        public ActionResult EditAd(int id)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("home", "index");

            return RedirectToAction("ViewEditAd", new { id = id });
        }

        [HttpPost]
        public ActionResult DeleteAd(int id)
        {
            if(ModelState.IsValid)
            {
                advertisementRepo.Delete(id);
            }

            return RedirectToAction("profile", new { id = User.FindFirstValue(ClaimTypes.NameIdentifier)});
        }

        [HttpGet]
        public ActionResult ViewEditAd(int id)
        {
            //var model = advertisementRepo.GetById(id);
            //model.bikeModel = bikeModelRepo.GetById(model.modelId);
            //model.bikeModel.bikeBrand = bikeBrandRepo.GetById(model.bikeModel.brandID);
            //model.bikeCategory = bikeCategoryRepo.GetById(model.categoryId);
            //model.city = cityRepo.GetById(model.cityId);
            //model.bikeColor = bikeColorRepo.GetById(model.colorId);

            var model = createAdViewModelRepo.GetById(id);

            if (model == null)
            {
                Response.StatusCode = 404;
                return RedirectToAction("index", "home");
            }

            return View("~/Views/Advertisement/EditAd.cshtml", model);
        }

        //[HttpPost]
        //public ActionResult ViewEditAd(CreateAdViewModel)
        //{

        //}
    }
}
