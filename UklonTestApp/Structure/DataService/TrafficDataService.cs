using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UklonTestApp.Models;
using Microsoft.EntityFrameworkCore;
using UklonTestApp.Structure.TrafficStructure.Services;

namespace UklonTestApp.Structure.DataService
{
    /// <summary>
    /// Service to provide data service functionality
    /// </summary>
    public class TrafficDataService : ITrafficDataService
    {
        public TrafficDataService(ITrafficDataServiceProvider trafficDataServiceProvider)
        {
            TrafficDataServiceProvider = trafficDataServiceProvider ?? throw new ArgumentNullException(nameof(trafficDataServiceProvider));
        }

        public ITrafficDataServiceProvider TrafficDataServiceProvider { get; }

        public async Task<RegionTrafficStatusModel> AddRegionTrafficStatusAsync(RegionTrafficStatus result)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            using (var context = this.TrafficDataServiceProvider.GetDataService())
            {
                var regionToUpdate = context.Regions.FirstOrDefault(region => region.RegionCode == result.RegionCode);

                var regionTrafficStatusModel = new RegionTrafficStatusModel();

                if (regionToUpdate == null)
                {
                    regionToUpdate = new RegionModel();
                    regionToUpdate.Id = Guid.NewGuid();
                    regionToUpdate.RegionCode = result.RegionCode;
                    regionToUpdate.RegionName = result.Title;
                    regionToUpdate.RegionTrafficStatuses = new List<RegionTrafficStatusModel> { regionTrafficStatusModel };
                }

                regionTrafficStatusModel.Id = Guid.NewGuid();
                regionTrafficStatusModel.DateTimeNow = result.Time;
                regionTrafficStatusModel.Region = regionToUpdate;
                regionTrafficStatusModel.RegionId = regionToUpdate.Id;
                regionTrafficStatusModel.TrafficIcon = result.Icon;
                regionTrafficStatusModel.TrafficLevel = result.Level;
                regionTrafficStatusModel.TrafficMessage = result.Text;

                context.Add(regionTrafficStatusModel);

                await context.SaveChangesAsync();

                return regionTrafficStatusModel;
            }
        }

        public async Task<IEnumerable<RegionModel>> AddRegionsAsync(IEnumerable<Region> regions)
        {
            if (regions == null)
            {
                throw new ArgumentNullException(nameof(regions));
            }

            using (var context = this.TrafficDataServiceProvider.GetDataService())
            {
                var regionModels = new List<RegionModel>();
                foreach (var region in regions)
                {
                    if (region == null)
                    {
                        throw new ArgumentNullException(nameof(regions));
                    }

                    var savedRegion = context.Regions
                        .Include(regionModel => regionModel.RegionTrafficStatuses)
                        .FirstOrDefault(dbRegion => dbRegion.RegionCode == region.RegionCode);

                    if (savedRegion == null)
                    {
                        savedRegion = new RegionModel();
                        savedRegion.Id = Guid.NewGuid();
                        savedRegion.RegionCode = region.RegionCode;
                        savedRegion.RegionName = region.RegionName;
                        savedRegion.RegionTrafficStatuses = new List<RegionTrafficStatusModel>();
                        context.Regions.AddRange(savedRegion);
                    }
                }

                await context.SaveChangesAsync();

                regionModels = context.Regions.Select(region => region).ToList();

                return regionModels;
            }
        }

        public RegionTrafficStatusModel GetRegionTrafficStatusAsync(string regionCode, DateTimeOffset dateTimeNow)
        {
            if (string.IsNullOrWhiteSpace(regionCode))
            {
                throw new ArgumentException("Argument is not valid!", nameof(regionCode));
            }

            using (var context = this.TrafficDataServiceProvider.GetDataService())
            {
                return context.RegionTrafficStatuses
                    .Include(status => status.Region)
                    .OrderByDescending(regionStatus => regionStatus.DateTimeNow)
                    .FirstOrDefault(
                    status =>
                        status.Region.RegionCode == regionCode &&
                        DateTimeOffset.Now - status.DateTimeNow < TimeSpan.FromMinutes(1));
            }
        }
    }
}
