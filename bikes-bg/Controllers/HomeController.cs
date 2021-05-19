using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bikes_bg.Models;
using bikes_bg.Repository.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bikes_bg.Controllers
{
    
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IGenericRepository<BikeModel> bikeRepo;
        public HomeController(IGenericRepository<BikeModel> bikeRepo)
        {
            this.bikeRepo = bikeRepo;
        }
        public IActionResult Index()
        {
            List<BikeModel> bikes = bikeRepo.GetTable().Include(b => b.bikeBrand).ToList();
            
            var bike = bikes.ElementAt(0);
            var brand = bike.bikeBrand;

            return View(bikes);
        }
    }
}
