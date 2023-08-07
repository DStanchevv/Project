using Microsoft.EntityFrameworkCore;
using SportWave.Data;
using SportWave.Data.Models;
using SportWave.Services.Contracts;
using SportWave.ViewModels.StoreViewModels;
using System.Globalization;

namespace SportWave.Services
{
    public class StoreService : IStoreService
    {
        private readonly SportWaveDbContext dbContext;

        public StoreService(SportWaveDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<StoreViewModel> FindClosestOne(double lat1, double lon1)
        {
            double lon2 = 0, lat2 = 0;
            Dictionary<StoreViewModel, double> stores = new Dictionary<StoreViewModel, double>();
            
            var allStores = await dbContext.Stores.Select(s => new StoreViewModel
            {
                Country = s.Country,
                Region = s.Region,
                City = s.City,
                Location = s.Location
            }).ToListAsync();

            foreach (var s in allStores)
            {
                lon2 = double.Parse(s.Location.Split(",").ToArray()[0], CultureInfo.InvariantCulture);
                lat2 = double.Parse(s.Location.Split(",").ToArray()[1], CultureInfo.InvariantCulture);

                if ((lat1 == lat2) && (lon1 == lon2))
                {
                    return null;
                }
                else
                {
                    double theta = lon1 - lon2;
                    double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
                    dist = Math.Acos(dist);
                    dist = rad2deg(dist);
                    dist = dist * 60 * 1.1515;

                    dist = dist * 1.609344;

                    stores.Add(s, dist);
                }
            }

            if (stores.Count > 0)
            {
                var store = stores.MinBy(kvp => kvp.Value).Key;
                return store;
            }

            return null;
        }

        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

        public async Task<IEnumerable<StoreViewModel>> GetAllStoresAsync()
        {
            return await dbContext.Stores.Select(s => new StoreViewModel
            {
                Country = s.Country,
                Region = s.Region,
                City = s.City,
                Location = s.Location
            }).ToListAsync();
        }

        public async Task<AllStoresViewModel> GetStoreByCityOrRegionAsync(string city, string region)
        {
            var stores =  await dbContext.Stores.Where(s => s.City == city || s.Region == region).Select(s => new StoreViewModel
            {
                Country = s.Country,
                Region = s.Region,
                City = s.City,
                Location = s.Location
            }).ToListAsync();

            AllStoresViewModel model = new AllStoresViewModel()
            {
                Stores = stores
            };

            return model;
        }
    }
}
