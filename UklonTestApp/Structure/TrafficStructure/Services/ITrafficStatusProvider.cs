using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UklonTestApp.Models;

namespace UklonTestApp.Structure.TrafficStructure.Services
{
    public interface ITrafficProvider
    {
        /// <summary>
        /// Get region traffic stratus for <paramref name="regionCode"/> and <paramref name="dateTimeNow"/>
        /// </summary>
        /// <param name="regionCode">Region code.</param>
        /// <param name="dateTimeNow">Timestamp for status.</param>
        /// <returns><see cref="RegionTrafficStatus"/></returns>
        Task<RegionTrafficStatus> GetRegionTrafficStatusAsync(string regionCode, DateTimeOffset dateTimeNow);

        /// <summary>
        /// Get all available <see cref="Region"/>
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Region>> GetRegionsAsync();
    }
}