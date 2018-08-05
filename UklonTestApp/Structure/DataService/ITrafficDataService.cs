using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UklonTestApp.Models;

namespace UklonTestApp.Structure.DataService
{
    public interface ITrafficDataService
    {
        RegionTrafficStatusModel GetRegionTrafficStatusAsync(string regionId, DateTimeOffset dateTimeNow);
        Task<RegionTrafficStatusModel> AddRegionTrafficStatusAsync(RegionTrafficStatus result);
        Task<IEnumerable<RegionModel>> AddRegionsAsync(IEnumerable<Region> regions);
    }
}
