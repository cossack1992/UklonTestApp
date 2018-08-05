using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UklonTestApp.Models;

namespace UklonTestApp.Structure.TrafficStructure.Interfaces
{
    public interface ITrafficSevice
    {
        /// <summary>
        /// Get traffic status for region by regionCode.
        /// </summary>
        /// <param name="regionCode"></param>
        /// <returns></returns>
        Task<RegionTrafficStatus> GetRegionTrafficStatusAsync(string regionCode, DateTimeOffset dateTimeNow);

        /// <summary>
        /// Get all available <see cref="Region"/>
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Region>> GetRegionsAsync();
    }
}
