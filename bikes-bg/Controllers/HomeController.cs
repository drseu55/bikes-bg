using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bikes_bg.Models;
using bikes_bg.Repository.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bikes_bg.Controllers
{
    
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IGenericRepository<BikeBrand> bikeRepo;
        public HomeController(IGenericRepository<BikeBrand> bikeRepo)
        {

            this.bikeRepo = bikeRepo;
        }
        public IActionResult Index()
        {
            var model = bikeRepo.GetAll();
            return View(model);
        }
    }
}
