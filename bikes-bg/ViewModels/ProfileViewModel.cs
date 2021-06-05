using bikes_bg.Models;
using bikes_bg.Repository.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace bikes_bg.ViewModels
{
    public class ProfileViewModel
    {
        public User user { get; set; }
        public IEnumerable<Advertisement> advertisements { get; set; }
        public IFormFile photo { get; set; }
    }
}
