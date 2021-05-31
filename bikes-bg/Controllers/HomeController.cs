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
        private readonly IGenericRepository<Advertisement> advertisementRepo;
        public HomeController(IGenericRepository<Advertisement> advertisementRepo)
        {
            this.advertisementRepo = advertisementRepo;
        }
        public IActionResult Index()
        {
            List<Advertisement> advertisements = advertisementRepo.GetTable()
                .Include(ad => ad.bikeModel)
                .Include(ad => ad.bikeModel.bikeBrand)
                .ToList();
           
            return View(advertisements);
        }
    }
}
