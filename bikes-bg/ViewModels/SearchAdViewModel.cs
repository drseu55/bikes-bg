using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bikes_bg.Models;

namespace bikes_bg.ViewModels
{
    public class SearchAdViewModel
    {
        public SearchAdViewModel()
        {
            searchFilter = new AdSearchFilter();
        }

        public AdSearchFilter searchFilter { get; set; }
        public List<Advertisement> advertisements { get; set; }
    }

}
