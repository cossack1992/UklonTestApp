using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UklonTestApp.Models;

namespace UklonTestApp.Structure.Service
{
    public interface IService
    {
        Task<RegionTrafficStatus> GetRegionTrafficStatusAsync(string regionCode, DateTimeOffset dateTimeNow);

        Task<RegionTrafficStatus[]> GetRegionTrafficStatuses(IEnumerable<string> regionCodes, DateTimeOffset dateTimeNow);

        Task<IEnumerable<Region>> GetRegions();
    }
}
