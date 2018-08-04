using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UklonTestApp.Models;

namespace UklonTestApp.Structure.TrafficStructure.Services
{
    public interface ITrafficStatusProvider
    {
        Task<RegionTrafficStatus> GetRegionTrafficStatusAsync(string regionCode, DateTimeOffset dateTimeNow);
    }
}