using System;
using bikes_bg.Controllers;
using bikes_bg.Models;
using bikes_bg.Repository.Base;
using bikes_bg.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace bikes_bg.Tests
{
    [TestClass]
    public class SearchFilter
    {
        IServiceProvider GetServiceProvider()
        {
            // Currently using data from database
            // Use mock in future

            var services = new ServiceCollection();
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer("server=HOMESERVER;database=bikes_bg;User id=sa;password=sa;"));
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services.BuildServiceProvider();
        }

        [TestMethod]
        public void PriceFromTo()
        {
            // Arrange
            int expectedPriceFrom = 1000;
            int expectedPriceTo = 2000;

            AdSearchFilter filter = new AdSearchFilter
            {
                priceFrom = expectedPriceFrom,
                priceTo = expectedPriceTo
            };

            var serviceProvider = GetServiceProvider();
            var AdRepository = serviceProvider.GetService<IGenericRepository<Advertisement>>();
            AdFilterService adFilterService = new AdFilterService(AdRepository);

            // Act
            var advertisementsList = adFilterService.GetAdsByFilter(filter);

            // Assert
            for (int i = 0; i < advertisementsList.Count; i++)
            {
                Assert.IsTrue(advertisementsList[i].price >= expectedPriceFrom);
                Assert.IsTrue(advertisementsList[i].price <= expectedPriceTo);
            }
        }

        [TestMethod]
        public void BrandSelected()
        {
            // Arrange
            int expectedBrandId = 2;

            AdSearchFilter filter = new AdSearchFilter
            {
                brandId = expectedBrandId
            };

            var serviceProvider = GetServiceProvider();
            var AdRepository = serviceProvider.GetService<IGenericRepository<Advertisement>>();
            AdFilterService adFilterService = new AdFilterService(AdRepository);

            // Act
            var advertisementsList = adFilterService.GetAdsByFilter(filter);

            // Assert
            for (int i = 0; i < advertisementsList.Count; i++)
            {
                Assert.IsTrue(advertisementsList[i].bikeModel.brandID == expectedBrandId);
            }
        }
    }
}
