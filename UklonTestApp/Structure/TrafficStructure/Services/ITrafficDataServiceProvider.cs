using UklonTestApp.Structure.DataService.DataService;

namespace UklonTestApp.Structure.TrafficStructure.Services
{
    public interface ITrafficDataServiceProvider
    {
        DatabaseContext GetDataService();
    }
}