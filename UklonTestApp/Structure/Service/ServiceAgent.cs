using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UklonTestApp.Models;
using UklonTestApp.Structure.DataService;
using UklonTestApp.Exceptions;
using UklonTestApp.Structure.TrafficStructure.Interfaces;

namespace UklonTestApp.Structure.Service
{
    /// <summary>
    /// Traffic service. 
    /// </summary>
    public class ServiceAgent : IService
    {
        public ServiceAgent(ITrafficSevice trafficService, ITrafficDataService trafficDataService)
        {
            TrafficService = trafficService ?? throw new ArgumentNullException(nameof(trafficService));
            TrafficDataService = trafficDataService ?? throw new ArgumentNullException(nameof(trafficDataService));
        }

        public ITrafficSevice TrafficService { get; }
        public ITrafficDataService TrafficDataService { get; }

        /// <summary>
        /// Get all available regions!
        /// </summary>
        /// <returns>List of <see cref="Region"/>s</returns>
        public async Task<IEnumerable<Region>> GetRegions()
        {
            try
            {
                var regions = await this.TrafficService.GetRegionsAsync();            

                var regionModels = await this.TrafficDataService.AddRegionsAsync(regions);

                return regionModels.Select(
                    model => 
                    new Region(
                        model.RegionCode,
                        model.RegionName));
            }
            catch (Exception exception)
            {
                throw new ServiceException("There were an exception during work of service of retriving regions", exception);
            }

        }

        /// <summary>
        /// Get region traffic stratus for <paramref name="regionCode"/> and <paramref name="dateTimeNow"/>
        /// </summary>
        /// <param name="regionCode">Region code.</param>
        /// <param name="dateTimeNow">Timestamp for status.</param>
        /// <returns><see cref="Task<RegionTrafficStatus>"/></returns>
        public async Task<RegionTrafficStatus> GetRegionTrafficStatusAsync(string regionCode, DateTimeOffset dateTimeNow)
        {
            if (string.IsNullOrWhiteSpace(regionCode))
            {
                throw new ArgumentException("Argument is not valid!", nameof(regionCode));
            }

            try
            {
                var result = this.TrafficDataService.GetRegionTrafficStatusAsync(regionCode, dateTimeNow);

                if (result == null)
                {
                    var webResult = await this.TrafficService.GetRegionTrafficStatusAsync(regionCode, dateTimeNow);

                    if(webResult != null)
                    {
                        result = await this.TrafficDataService.AddRegionTrafficStatusAsync(webResult);
                    }
                }

                return result == null ? null : new RegionTrafficStatus(
                    result.Region.RegionCode,
                    result.Region.RegionName,
                    result.DateTimeNow,
                    result.TrafficLevel,
                    result.TrafficIcon, 
                    result.TrafficMessage);
                
            }
            catch (Exception exception)
            {
                throw new ServiceException("There were an exception during work of service of retriving region traffic statuses", exception);
            }
        }

        /// <summary>
        /// Get <see cref="RegionTrafficStatus"/> for provided region codes!
        /// </summary>
        /// <param name="regionCodes">List of region codes to retrive.</param>
        /// <param name="dateTimeNow">Timestamp for statuses.</param>
        /// <returns><see cref="Task<RegionTrafficStatus[]>"/></returns>
        public async Task<RegionTrafficStatus[]> GetRegionTrafficStatuses(IEnumerable<string> regionCodes, DateTimeOffset dateTimeNow)
        {
            if (regionCodes == null)
            {
                throw new ArgumentNullException(nameof(regionCodes));
            }

            var tasks = new List<Task<RegionTrafficStatus>>();
            foreach (var regionCode in regionCodes)
            {
                tasks.Add(this.GetRegionTrafficStatusAsync(regionCode, dateTimeNow));
            }

            try
            {
                return await Task.WhenAll(tasks);
            }
            catch(Exception exception)
            {
                throw new ServiceException("There were an exception during work of service of retriving region traffic statuses", exception);
            }
        }
    }
}
