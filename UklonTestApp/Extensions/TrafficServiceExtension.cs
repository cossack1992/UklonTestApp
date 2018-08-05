using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using UklonTestApp.Structure.TrafficStructure.Interfaces;
using UklonTestApp.Exceptions;

namespace UklonTestApp.Exensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddTrafficService(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var trafficServiceSection = "TrafficService";
            var serviceName = "";

            try
            {
                serviceName = configuration.GetSection(trafficServiceSection)?.Value;

                if (serviceName == null)
                {
                    throw new ConfigurationException($"Could not find [{trafficServiceSection}] section in provided configurarion!");
                }

                var serviceType = Type.GetType(serviceName);

                var serviceInstance = Activator.CreateInstance(serviceType) as ITrafficSevice;

                if(serviceInstance == null)
                {
                    throw new ConfigurationException($"Provided service [ServiceName = {serviceName}] is not ITrafficSevice");
                }

                return serviceCollection.AddTransient(_ => serviceInstance);
            }
            catch(Exception exception)
            {
                throw new ConfigurationException($"Could not create instance of type [TypeName = {serviceName}]", exception);
            }
        }
    }
}
