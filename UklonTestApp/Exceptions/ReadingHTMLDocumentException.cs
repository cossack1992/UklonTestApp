using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UklonTestApp.Exceptions
{
    [Serializable]
    public class ReadingHTMLDocumentException : Exception
    {
        public ReadingHTMLDocumentException()
        : base() { }

        public ReadingHTMLDocumentException(string message)
            : base(message) { }

        public ReadingHTMLDocumentException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public ReadingHTMLDocumentException(string message, Exception innerException)
            : base(message, innerException) { }

        public ReadingHTMLDocumentException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    }
}
