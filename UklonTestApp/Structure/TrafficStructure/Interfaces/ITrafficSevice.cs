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
        RegionTrafficStatus GetRegionTrafficStatus(string regionCode, DateTimeOffset dateTimeNow);
    }
}
