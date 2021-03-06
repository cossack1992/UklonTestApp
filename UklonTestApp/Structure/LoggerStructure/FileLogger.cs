﻿using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace UklonTestApp.Structure
{
    public class FileLogger : ILogger
    {
        private string filePath;
        static ReaderWriterLock rwl = new ReaderWriterLock();
        public FileLogger(string path)
        {
            filePath = path;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        private void Log<TState>(DateTimeOffset timestamp, LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            
                if (formatter != null)
                {
                    var builder = new StringBuilder();
                    builder.Append(timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff zzz"));
                    builder.Append(" [");
                    builder.Append(logLevel.ToString());
                    builder.Append("] ");
                    builder.Append(": ");
                    builder.AppendLine(formatter(state, exception));

                    File.AppendAllText(filePath, builder.ToString());
                }
            
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            try
            {
                rwl.AcquireWriterLock(500);
                Log(DateTimeOffset.Now, logLevel, eventId, state, exception, formatter);
            }
            finally
            {
                rwl.ReleaseWriterLock();
            }
        }
    }
}
