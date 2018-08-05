using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UklonTestApp.Exceptions
{
    [Serializable]
    public class TrafficServiceException : Exception
    {
        public TrafficServiceException()
        : base() { }

        public TrafficServiceException(string message)
            : base(message) { }

        public TrafficServiceException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public TrafficServiceException(string message, Exception innerException)
            : base(message, innerException) { }

        public TrafficServiceException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    }
}
