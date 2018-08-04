using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UklonTestApp.Models;
using UklonTestApp.Structure.TrafficStructure.Interfaces;

namespace UklonTestApp.Structure.TrafficStructure.Services
{
    public class TrafficStatusProvider : ITrafficStatusProvider
    {
        public TrafficStatusProvider(ITrafficSevice trafficService)
        {
            TrafficService = trafficService ?? throw new ArgumentNullException(nameof(trafficService));
        }

        public ITrafficSevice TrafficService { get; }

        public Task<RegionTrafficStatus> GetRegionTrafficStatusAsync(string regionCode, DateTimeOffset dateTimeNow)
        {
            if (string.IsNullOrWhiteSpace(regionCode))
            {
                throw new ArgumentException("Argument is not valid!", nameof(regionCode));
            }

            return Task.Run(() => TrafficService.GetRegionTrafficStatus(regionCode, dateTimeNow));
        }
    }
}
