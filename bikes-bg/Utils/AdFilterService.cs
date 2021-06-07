using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bikes_bg.Models;
using bikes_bg.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace bikes_bg.Utils
{
    public class AdFilterService
    {
        private IGenericRepository<Advertisement> advertisementRepo;

        public AdFilterService(IGenericRepository<Advertisement> advertisementRepo)
        {
            this.advertisementRepo = advertisementRepo;
        }

        public List<Advertisement> GetAdsByFilter(AdSearchFilter searchFilter)
        {
            var result = advertisementRepo.GetTable()
                .Include(ad => ad.bikeModel).ThenInclude(model => model.bikeBrand)
                .AsQueryable();

            if (searchFilter != null)
            {
                if (searchFilter.brandId.HasValue)
                {
                    result = result.Where(ad => ad.bikeModel.brandID == searchFilter.brandId);
                }

                if (searchFilter.priceFrom.HasValue)
                {
                    result = result.Where(ad => ad.price >= searchFilter.priceFrom);
                }

                if (searchFilter.priceTo.HasValue)
                {
                    result = result.Where(ad => ad.price <= searchFilter.priceTo);
                }

                if (searchFilter.regionId.HasValue)
                {
                    result = result.Where(ad => ad.city.regionID == searchFilter.regionId);
                }
            }

            return result.ToList();
        }
    }
}
