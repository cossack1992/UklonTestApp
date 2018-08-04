using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UklonTestApp.Models;

namespace UklonTestApp.Structure.DataService
{
    public interface ITrafficDataService
    {
        RegionTrafficStatusModel GetRegionTrafficStatusAsync(string regionId, DateTimeOffset dateTimeNow);
        Task<RegionTrafficStatusModel> AddOrUpdateRegionTrafficStatusAsync(RegionTrafficStatus result);
        Task<IEnumerable<RegionModel>> SaveRegions(IEnumerable<Region> regions);
    }
}
