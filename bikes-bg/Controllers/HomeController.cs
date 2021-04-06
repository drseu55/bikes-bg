using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bikes_bg.Models;
using bikes_bg.Repository.Base;
using Microsoft.AspNetCore.Mvc;

namespace bikes_bg.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IGenericRepository<BikeBrand> bikeRepo;
        public HomeController(IGenericRepository<BikeBrand> bikeRepo)
        {

            this.bikeRepo = bikeRepo;
        }
        public IActionResult Index()
        {
            BikeBrand bike = bikeRepo.GetAll().First();
            return Content(bike.Name);
        }
    }
}
