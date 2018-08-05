using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UklonTestApp.Structure
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private string path;
        public FileLoggerProvider(string _path)
        {
            if (string.IsNullOrWhiteSpace(_path))
            {
                throw new ArgumentException("Argument is not valid!", nameof(_path));
            }

            path = _path;
        }
        public ILogger CreateLogger(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                throw new ArgumentException("Argument is not valid!", nameof(categoryName));
            }

            return new FileLogger(path);
        }

        public void Dispose()
        {
        }
    }
}
