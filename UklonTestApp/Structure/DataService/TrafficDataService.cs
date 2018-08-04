using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UklonTestApp.Models;
using UklonTestApp.Structure.DataService.DataService;
using UklonTestApp.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace UklonTestApp.Structure.DataService
{
    public class TrafficDataService : ITrafficDataService
    {
        public TrafficDataService(DatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
        }

        public DatabaseContext DatabaseContext { get; }

        public async Task<RegionTrafficStatusModel> AddOrUpdateRegionTrafficStatusAsync(RegionTrafficStatus result)
        {
            if (result == null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            using (var context = new DatabaseContext())
            {
                var regionToUpdate = context.Regions.FirstOrDefault(region => region.RegionCode == result.RegionCode);
                var regionTrafficStatusModel = new RegionTrafficStatusModel();

                regionTrafficStatusModel.Id = Guid.NewGuid();
                regionTrafficStatusModel.DateTimeNow = result.Time;
                regionTrafficStatusModel.Region = regionToUpdate ?? throw new DataBaseException($"Could not find region [RegionCode = {result.RegionCode}]");
                regionTrafficStatusModel.RegionId = regionToUpdate.Id;
                regionTrafficStatusModel.TrafficIcon = result.Icon;
                regionTrafficStatusModel.TrafficLevel = result.Level;
                regionTrafficStatusModel.TrafficMessage = result.Text;

                context.Add(regionTrafficStatusModel);

                await context.SaveChangesAsync();

                return regionTrafficStatusModel;
            }
        }

        public async Task<IEnumerable<RegionModel>> SaveRegions(IEnumerable<Region> regions)
        {
            if (regions == null)
            {
                throw new ArgumentNullException(nameof(regions));
            }

            using (var context = new DatabaseContext())
            {
                var regionModels = new List<RegionModel>();
                foreach(var region in regions)
                {
                    if (region == null)
                    {
                        throw new ArgumentNullException(nameof(regions));
                    }

                    var savedRegion = context.Regions.FirstOrDefault(dbRegion => dbRegion.RegionCode == region.RegionCode);
                    if(savedRegion == null)
                    {
                        savedRegion = new RegionModel();
                        savedRegion.Id = Guid.NewGuid();
                        savedRegion.RegionCode = region.RegionCode;
                        savedRegion.RegionName = region.RegionName;
                        savedRegion.RegionTrafficStatuses = new List<RegionTrafficStatusModel>();
                        context.Regions.AddRange(savedRegion);
                    }

                    regionModels.Add(savedRegion);
                }              

                await context.SaveChangesAsync();

                return regionModels;
            }
        }

        public RegionTrafficStatusModel GetRegionTrafficStatusAsync(string regionCode, DateTimeOffset dateTimeNow)
        {
            if (string.IsNullOrWhiteSpace(regionCode))
            {
                throw new ArgumentException("Argument is not valid!", nameof(regionCode));
            }

            using (var context = new DatabaseContext())
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
