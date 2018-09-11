using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ez.Common.Logging
{
    public interface ILogger<T> : ILogger
    {

    }
    public interface IEzLogger<T> : IEzLogger
    {

    }
    public interface IEzLogger
    {
        bool IsFatalEnabled { get; }
        bool IsWarnEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsDebugEnabled { get; }
        bool IsTraceEnabled { get; }
        bool IsErrorEnabled { get; }
        void Debug(string message, int argument);
        void Debug(IFormatProvider formatProvider, string message, int argument);
        void Debug(IFormatProvider formatProvider, string message, string argument);
        void Log(LogEventInfo logEvent);
        void Log<T>(LogLevel level, T value);
        void Log<T>(LogLevel level, IFormatProvider formatProvider, T value);
    }
}
