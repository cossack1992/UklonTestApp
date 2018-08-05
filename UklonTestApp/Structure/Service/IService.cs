using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UklonTestApp.Models;

namespace UklonTestApp.Structure.Service
{
    public interface IService
    {
        /// <summary>
        /// Get region traffic stratus for <paramref name="regionCode"/> and <paramref name="dateTimeNow"/>
        /// </summary>
        /// <param name="regionCode">Region code.</param>
        /// <param name="dateTimeNow">Timestamp for status.</param>
        /// <returns><see cref="Task<RegionTrafficStatus>"/></returns>
        Task<RegionTrafficStatus> GetRegionTrafficStatusAsync(string regionCode, DateTimeOffset dateTimeNow);

        /// <summary>
        /// Get <see cref="RegionTrafficStatus"/> for provided region codes!
        /// </summary>
        /// <param name="regionCodes">List of region codes to retrive.</param>
        /// <param name="dateTimeNow">Timestamp for statuses.</param>
        /// <returns><see cref="Task<RegionTrafficStatus[]>"/></returns>
        Task<RegionTrafficStatus[]> GetRegionTrafficStatuses(IEnumerable<string> regionCodes, DateTimeOffset dateTimeNow);

        /// <summary>
        /// Get all available regions!
        /// </summary>
        /// <returns>List of <see cref="Region"/>s</returns>
        Task<IEnumerable<Region>> GetRegions();
    }
}
