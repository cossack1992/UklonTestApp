using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UklonTestApp.Exceptions
{
    [Serializable]
    public class ConfigurationException : Exception
    {
        public ConfigurationException()
        : base() { }

        public ConfigurationException(string message)
            : base(message) { }

        public ConfigurationException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public ConfigurationException(string message, Exception innerException)
            : base(message, innerException) { }

        public ConfigurationException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    }
}
