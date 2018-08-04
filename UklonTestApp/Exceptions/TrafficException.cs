using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UklonTestApp.Exceptions
{
    [Serializable]
    public class TrafficException : Exception
    {
        public TrafficException()
        : base() { }

        public TrafficException(string message)
            : base(message) { }

        public TrafficException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public TrafficException(string message, Exception innerException)
            : base(message, innerException) { }

        public TrafficException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    }
}
