using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UklonTestApp.Structure;

namespace UklonTestApp.Exensions
{
    public static class FileLoggerExtension
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory,
                                        string filePath)
        {
            factory.AddProvider(new FileLoggerProvider(filePath));
            return factory;
        }
    }
}
