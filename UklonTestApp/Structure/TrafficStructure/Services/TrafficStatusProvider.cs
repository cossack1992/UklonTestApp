using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UklonTestApp.Models;
using UklonTestApp.Structure.TrafficStructure.Interfaces;

namespace UklonTestApp.Structure.TrafficStructure.Services
{
    /// <summary>
    /// Provide <see cref="RegionTrafficStatus"/> 
    /// </summary>
    public class TrafficStatusProvider : ITrafficProvider
    {
        public TrafficStatusProvider(ITrafficSevice trafficService)
        {
            TrafficService = trafficService ?? throw new ArgumentNullException(nameof(trafficService));
        }

        public ITrafficSevice TrafficService { get; }

        public Task<IEnumerable<Region>> GetRegionsAsync()
        {
            return Task.Run(() => this.TrafficService.GetRegionsAsync());
        }

        /// <summary>
        /// Get region traffic stratus for <paramref name="regionCode"/> and <paramref name="dateTimeNow"/>
        /// </summary>
        /// <param name="regionCode">Region code.</param>
        /// <param name="dateTimeNow">Timestamp for status.</param>
        /// <returns><see cref="RegionTrafficStatus"/></returns>
        public Task<RegionTrafficStatus> GetRegionTrafficStatusAsync(string regionCode, DateTimeOffset dateTimeNow)
        {
            if (string.IsNullOrWhiteSpace(regionCode))
            {
                throw new ArgumentException("Argument is not valid!", nameof(regionCode));
            }

            return Task.Run(() => TrafficService.GetRegionTrafficStatusAsync(regionCode, dateTimeNow));
        }
    }
}
