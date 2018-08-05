using System;
using UklonTestApp.Structure.DataService.DataService;

namespace UklonTestApp.Structure.TrafficStructure.Services
{
    /// <summary>
    /// Provide <see cref="RegionTrafficStatus"/> 
    /// </summary>
    public class TrafficDataServiceProvider : ITrafficDataServiceProvider
    {
        public TrafficDataServiceProvider(IServiceProvider provider)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public IServiceProvider Provider { get; }

        public DatabaseContext GetDataService()
        {
            return (DatabaseContext)Provider.GetService(typeof(DatabaseContext));
        }
    }
}
