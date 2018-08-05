using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UklonTestApp.Helpers;
using UklonTestApp.Models;
using UklonTestApp.Structure.DataService;
using UklonTestApp.Structure.TrafficStructure.Services;
using UklonTestApp.Exceptions;
using System.Threading;

namespace UklonTestApp.Structure.Service
{
    public class ServiceAgent : IService
    {
        public ServiceAgent(ITrafficStatusProvider trafficStatusProvider, ITrafficDataService trafficDataService)
        {
            TrafficStatusProvider = trafficStatusProvider ?? throw new ArgumentNullException(nameof(trafficStatusProvider));
            TrafficDataService = trafficDataService ?? throw new ArgumentNullException(nameof(trafficDataService));
        }

        public ITrafficStatusProvider TrafficStatusProvider { get; }
        public ITrafficDataService TrafficDataService { get; }

        public async Task<IEnumerable<Region>> GetRegions()
        {
            try
            {
                var regions = await Task.Run(
                    () =>
                    {
                        string url = @"https://goo.gl/EKCY6i";
                        var htmlWeb = new HtmlWeb();
                        var document = htmlWeb.Load(url);

                        return HelperMethods.GetRegionsFromHTMLDocument(document);
                    }
                    );
                var regionModels = await this.TrafficDataService.SaveRegions(regions);

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
                    var webResult = await this.TrafficStatusProvider.GetRegionTrafficStatusAsync(regionCode, dateTimeNow);

                    if(webResult != null)
                    {
                        result = await this.TrafficDataService.AddOrUpdateRegionTrafficStatusAsync(webResult);
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

        public async Task<RegionTrafficStatus[]> GetRegionTrafficStatuses(IEnumerable<string> regionCodes, DateTimeOffset dateTimeNow)
        {
            if (regionCodes == null)
            {
                throw new ArgumentNullException(nameof(regionCodes));
            }

            var tasks = new List<Task<RegionTrafficStatus>>();
            foreach (var regionCode in regionCodes)
            {
                var task = Task.Run(async () => await this.GetRegionTrafficStatusAsync(regionCode, dateTimeNow));

                tasks.Add(task);
            }

            return await Task.WhenAll(tasks);
        }
    }
}
