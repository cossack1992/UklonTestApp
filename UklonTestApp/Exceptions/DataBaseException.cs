using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UklonTestApp.Exceptions
{
    [Serializable]
    public class DataBaseException : Exception
    {
        public DataBaseException()
            : base() { }

        public DataBaseException(string message)
            : base(message) { }

        public DataBaseException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public DataBaseException(string message, Exception innerException)
            : base(message, innerException) { }

        public DataBaseException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    }
}
