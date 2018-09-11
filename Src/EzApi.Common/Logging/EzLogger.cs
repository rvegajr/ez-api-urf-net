using NLog;
using System;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Ez.Common.Logging
{
    public class EzLogger<T> : IEzLogger<T>, ILogger<T>, IEzLogger, ILogger
    {
        private readonly NLog.ILogger logger = LogManager.GetLogger(typeof(T).FullName);

        public bool IsTraceEnabled => logger.IsTraceEnabled;

        public bool IsDebugEnabled => logger.IsDebugEnabled;

        public bool IsInfoEnabled => logger.IsInfoEnabled;

        public bool IsWarnEnabled => logger.IsWarnEnabled;

        public bool IsErrorEnabled => logger.IsErrorEnabled;

        public bool IsFatalEnabled => logger.IsFatalEnabled;

        public string Name => logger.Name;

        public LogFactory Factory => logger.Factory;

        public event EventHandler<EventArgs> LoggerReconfigured
        {
            add
            {
                logger.LoggerReconfigured += value;
            }

            remove
            {
                logger.LoggerReconfigured -= value;
            }
        }

        public void Debug<T1>(T1 value)
        {
            logger.Debug(value);
        }

        public void Debug<T1>(IFormatProvider formatProvider, T1 value)
        {
            logger.Debug(formatProvider, value);
        }

        public void Debug(LogMessageGenerator messageFunc)
        {
            logger.Debug(messageFunc);
        }

        public void Debug(Exception exception, [Localizable(false)] string message)
        {
            logger.Debug(exception, message);
        }

        public void Debug(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            logger.Debug(exception, message, args);
        }

        public void Debug(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Debug(exception, formatProvider, message, args);
        }

        public void Debug(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Debug(formatProvider, message, args);
        }

        public void Debug([Localizable(false)] string message)
        {
            logger.Debug(message);
        }

        public void Debug([Localizable(false)] string message, params object[] args)
        {
            logger.Debug(message, args);
        }

        public void Debug([Localizable(false)] string message, Exception exception)
        {
            logger.Debug(message, exception);
        }

        public void Debug<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug<TArgument>([Localizable(false)] string message, TArgument argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Debug(formatProvider, message, argument1, argument2);
        }

        public void Debug<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Debug(message, argument1, argument2);
        }

        public void Debug<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Debug(formatProvider, message, argument1, argument2, argument3);
        }

        public void Debug<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Debug(message, argument1, argument2, argument3);
        }

        public void Debug(object value)
        {
            logger.Debug(value);
        }

        public void Debug(IFormatProvider formatProvider, object value)
        {
            logger.Debug(formatProvider, value);
        }

        public void Debug(string message, object arg1, object arg2)
        {
            logger.Debug(message, arg1, arg2);
        }

        public void Debug(string message, object arg1, object arg2, object arg3)
        {
            logger.Debug(message, arg1, arg2, arg3);
        }

        public void Debug(IFormatProvider formatProvider, string message, bool argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, bool argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, char argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, char argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, byte argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, byte argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, string argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, string argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, int argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, int argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, long argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, long argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, float argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, float argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, double argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, double argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, decimal argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, decimal argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, object argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, object argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, sbyte argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, sbyte argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, uint argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, uint argument)
        {
            logger.Debug(message, argument);
        }

        public void Debug(IFormatProvider formatProvider, string message, ulong argument)
        {
            logger.Debug(formatProvider, message, argument);
        }

        public void Debug(string message, ulong argument)
        {
            logger.Debug(message, argument);
        }

        public void DebugException([Localizable(false)] string message, Exception exception)
        {
            logger.DebugException(message, exception);
        }

        public void Error<T1>(T1 value)
        {
            logger.Error(value);
        }

        public void Error<T1>(IFormatProvider formatProvider, T1 value)
        {
            logger.Error(formatProvider, value);
        }

        public void Error(LogMessageGenerator messageFunc)
        {
            logger.Error(messageFunc);
        }

        public void Error(Exception exception, [Localizable(false)] string message)
        {
            logger.Error(exception, message);
        }

        public void Error(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            logger.Error(exception, message, args);
        }

        public void Error(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Error(exception, formatProvider, message, args);
        }

        public void Error(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Error(formatProvider, message, args);
        }

        public void Error([Localizable(false)] string message)
        {
            logger.Error(message);
        }

        public void Error([Localizable(false)] string message, params object[] args)
        {
            logger.Error(message, args);
        }

        public void Error([Localizable(false)] string message, Exception exception)
        {
            logger.Error(message, exception);
        }

        public void Error<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error<TArgument>([Localizable(false)] string message, TArgument argument)
        {
            logger.Error(message, argument);
        }

        public void Error<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Error(formatProvider, message, argument1, argument2);
        }

        public void Error<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Error(message, argument1, argument2);
        }

        public void Error<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Error(formatProvider, message, argument1, argument2, argument3);
        }

        public void Error<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Error(message, argument1, argument2, argument3);
        }

        public void Error(object value)
        {
            logger.Error(value);
        }

        public void Error(IFormatProvider formatProvider, object value)
        {
            logger.Error(formatProvider, value);
        }

        public void Error(string message, object arg1, object arg2)
        {
            logger.Error(message, arg1, arg2);
        }

        public void Error(string message, object arg1, object arg2, object arg3)
        {
            logger.Error(message, arg1, arg2, arg3);
        }

        public void Error(IFormatProvider formatProvider, string message, bool argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, bool argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, char argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, char argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, byte argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, byte argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, string argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, string argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, int argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, int argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, long argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, long argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, float argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, float argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, double argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, double argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, decimal argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, decimal argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, object argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, object argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, sbyte argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, sbyte argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, uint argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, uint argument)
        {
            logger.Error(message, argument);
        }

        public void Error(IFormatProvider formatProvider, string message, ulong argument)
        {
            logger.Error(formatProvider, message, argument);
        }

        public void Error(string message, ulong argument)
        {
            logger.Error(message, argument);
        }

        public void ErrorException([Localizable(false)] string message, Exception exception)
        {
            logger.ErrorException(message, exception);
        }

        public void Fatal<T1>(T1 value)
        {
            logger.Fatal(value);
        }

        public void Fatal<T1>(IFormatProvider formatProvider, T1 value)
        {
            logger.Fatal(formatProvider, value);
        }

        public void Fatal(LogMessageGenerator messageFunc)
        {
            logger.Fatal(messageFunc);
        }

        public void Fatal(Exception exception, [Localizable(false)] string message)
        {
            logger.Fatal(exception, message);
        }

        public void Fatal(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            logger.Fatal(exception, message, args);
        }

        public void Fatal(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Fatal(exception, formatProvider, message, args);
        }

        public void Fatal(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Fatal(formatProvider, message, args);
        }

        public void Fatal([Localizable(false)] string message)
        {
            logger.Fatal(message);
        }

        public void Fatal([Localizable(false)] string message, params object[] args)
        {
            logger.Fatal(message, args);
        }

        public void Fatal([Localizable(false)] string message, Exception exception)
        {
            logger.Fatal(message, exception);
        }

        public void Fatal<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal<TArgument>([Localizable(false)] string message, TArgument argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Fatal(formatProvider, message, argument1, argument2);
        }

        public void Fatal<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Fatal(message, argument1, argument2);
        }

        public void Fatal<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Fatal(formatProvider, message, argument1, argument2, argument3);
        }

        public void Fatal<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Fatal(message, argument1, argument2, argument3);
        }

        public void Fatal(object value)
        {
            logger.Fatal(value);
        }

        public void Fatal(IFormatProvider formatProvider, object value)
        {
            logger.Fatal(formatProvider, value);
        }

        public void Fatal(string message, object arg1, object arg2)
        {
            logger.Fatal(message, arg1, arg2);
        }

        public void Fatal(string message, object arg1, object arg2, object arg3)
        {
            logger.Fatal(message, arg1, arg2, arg3);
        }

        public void Fatal(IFormatProvider formatProvider, string message, bool argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, bool argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, char argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, char argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, byte argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, byte argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, string argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, string argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, int argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, int argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, long argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, long argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, float argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, float argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, double argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, double argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, decimal argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, decimal argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, object argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, object argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, sbyte argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, sbyte argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, uint argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, uint argument)
        {
            logger.Fatal(message, argument);
        }

        public void Fatal(IFormatProvider formatProvider, string message, ulong argument)
        {
            logger.Fatal(formatProvider, message, argument);
        }

        public void Fatal(string message, ulong argument)
        {
            logger.Fatal(message, argument);
        }

        public void FatalException([Localizable(false)] string message, Exception exception)
        {
            logger.FatalException(message, exception);
        }

        public void Info<T1>(T1 value)
        {
            logger.Info(value);
        }

        public void Info<T1>(IFormatProvider formatProvider, T1 value)
        {
            logger.Info(formatProvider, value);
        }

        public void Info(LogMessageGenerator messageFunc)
        {
            logger.Info(messageFunc);
        }

        public void Info(Exception exception, [Localizable(false)] string message)
        {
            logger.Info(exception, message);
        }

        public void Info(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            logger.Info(exception, message, args);
        }

        public void Info(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Info(exception, formatProvider, message, args);
        }

        public void Info(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Info(formatProvider, message, args);
        }

        public void Info([Localizable(false)] string message)
        {
            logger.Info(message);
        }

        public void Info([Localizable(false)] string message, params object[] args)
        {
            logger.Info(message, args);
        }

        public void Info([Localizable(false)] string message, Exception exception)
        {
            logger.Info(message, exception);
        }

        public void Info<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info<TArgument>([Localizable(false)] string message, TArgument argument)
        {
            logger.Info(message, argument);
        }

        public void Info<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Info(formatProvider, message, argument1, argument2);
        }

        public void Info<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Info(message, argument1, argument2);
        }

        public void Info<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Info(formatProvider, message, argument1, argument2, argument3);
        }

        public void Info<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Info(message, argument1, argument2, argument3);
        }

        public void Info(object value)
        {
            logger.Info(value);
        }

        public void Info(IFormatProvider formatProvider, object value)
        {
            logger.Info(formatProvider, value);
        }

        public void Info(string message, object arg1, object arg2)
        {
            logger.Info(message, arg1, arg2);
        }

        public void Info(string message, object arg1, object arg2, object arg3)
        {
            logger.Info(message, arg1, arg2, arg3);
        }

        public void Info(IFormatProvider formatProvider, string message, bool argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, bool argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, char argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, char argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, byte argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, byte argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, string argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, string argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, int argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, int argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, long argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, long argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, float argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, float argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, double argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, double argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, decimal argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, decimal argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, object argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, object argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, sbyte argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, sbyte argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, uint argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, uint argument)
        {
            logger.Info(message, argument);
        }

        public void Info(IFormatProvider formatProvider, string message, ulong argument)
        {
            logger.Info(formatProvider, message, argument);
        }

        public void Info(string message, ulong argument)
        {
            logger.Info(message, argument);
        }

        public void InfoException([Localizable(false)] string message, Exception exception)
        {
            logger.InfoException(message, exception);
        }

        public bool IsEnabled(LogLevel level)
        {
            return logger.IsEnabled(level);
        }

        public void Log(LogEventInfo logEvent)
        {
            logger.Log(logEvent);
        }

        public void Log(Type wrapperType, LogEventInfo logEvent)
        {
            logger.Log(wrapperType, logEvent);
        }

        public void Log<T1>(LogLevel level, T1 value)
        {
            logger.Log(level, value);
        }

        public void Log<T1>(LogLevel level, IFormatProvider formatProvider, T1 value)
        {
            logger.Log(level, formatProvider, value);
        }

        public void Log(LogLevel level, LogMessageGenerator messageFunc)
        {
            logger.Log(level, messageFunc);
        }

        public void Log(LogLevel level, Exception exception, [Localizable(false)] string message, params object[] args)
        {
            logger.Log(level, exception, message, args);
        }

        public void Log(LogLevel level, Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Log(level, exception, formatProvider, message, args);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Log(level, formatProvider, message, args);
        }

        public void Log(LogLevel level, [Localizable(false)] string message)
        {
            logger.Log(level, message);
        }

        public void Log(LogLevel level, [Localizable(false)] string message, params object[] args)
        {
            logger.Log(level, message, args);
        }

        public void Log(LogLevel level, [Localizable(false)] string message, Exception exception)
        {
            logger.Log(level, message, exception);
        }

        public void Log<TArgument>(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            logger.Log(level, formatProvider, message, argument);
        }

        public void Log<TArgument>(LogLevel level, [Localizable(false)] string message, TArgument argument)
        {
            logger.Log(level, message, argument);
        }

        public void Log<TArgument1, TArgument2>(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Log(level, formatProvider, message, argument1, argument2);
        }

        public void Log<TArgument1, TArgument2>(LogLevel level, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Log(level, message, argument1, argument2);
        }

        public void Log<TArgument1, TArgument2, TArgument3>(LogLevel level, IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Log(level, formatProvider, message, argument1, argument2, argument3);
        }

        public void Log<TArgument1, TArgument2, TArgument3>(LogLevel level, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Log(level, message, argument1, argument2, argument3);
        }

        public void Log(LogLevel level, object value)
        {
            logger.Log(level, value);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, object value)
        {
            logger.Log(level, formatProvider, value);
        }

        public void Log(LogLevel level, string message, object arg1, object arg2)
        {
            logger.Log(level, message, arg1, arg2);
        }

        public void Log(LogLevel level, string message, object arg1, object arg2, object arg3)
        {
            logger.Log(level, message, arg1, arg2, arg3);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, bool argument)
        {
            logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, bool argument)
        {
            logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, char argument)
        {
            logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, char argument)
        {
            logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, byte argument)
        {
            logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, byte argument)
        {
            logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, string argument)
        {
            logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, string argument)
        {
            logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, int argument)
        {
            logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, int argument)
        {
            logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, long argument)
        {
            logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, long argument)
        {
            logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, float argument)
        {
            logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, float argument)
        {
            logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, double argument)
        {
            logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, double argument)
        {
            logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, decimal argument)
        {
            logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, decimal argument)
        {
            logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, object argument)
        {
            logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, object argument)
        {
            logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, sbyte argument)
        {
            logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, sbyte argument)
        {
            logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, uint argument)
        {
            logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, uint argument)
        {
            logger.Log(level, message, argument);
        }

        public void Log(LogLevel level, IFormatProvider formatProvider, string message, ulong argument)
        {
            logger.Log(level, formatProvider, message, argument);
        }

        public void Log(LogLevel level, string message, ulong argument)
        {
            logger.Log(level, message, argument);
        }

        public void LogException(LogLevel level, [Localizable(false)] string message, Exception exception)
        {
            logger.LogException(level, message, exception);
        }

        public void Swallow(Action action)
        {
            logger.Swallow(action);
        }

        public T1 Swallow<T1>(Func<T1> func)
        {
            return logger.Swallow(func);
        }

        public T1 Swallow<T1>(Func<T1> func, T1 fallback)
        {
            return logger.Swallow(func, fallback);
        }

        public void Swallow(Task task)
        {
            logger.Swallow(task);
        }

        public Task SwallowAsync(Task task)
        {
            return logger.SwallowAsync(task);
        }

        public Task SwallowAsync(Func<Task> asyncAction)
        {
            return logger.SwallowAsync(asyncAction);
        }

        public Task<TResult> SwallowAsync<TResult>(Func<Task<TResult>> asyncFunc)
        {
            return logger.SwallowAsync(asyncFunc);
        }

        public Task<TResult> SwallowAsync<TResult>(Func<Task<TResult>> asyncFunc, TResult fallback)
        {
            return logger.SwallowAsync(asyncFunc, fallback);
        }

        public void Trace<T1>(T1 value)
        {
            logger.Trace(value);
        }

        public void Trace<T1>(IFormatProvider formatProvider, T1 value)
        {
            logger.Trace(formatProvider, value);
        }

        public void Trace(LogMessageGenerator messageFunc)
        {
            logger.Trace(messageFunc);
        }

        public void Trace(Exception exception, [Localizable(false)] string message)
        {
            logger.Trace(exception, message);
        }

        public void Trace(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            logger.Trace(exception, message, args);
        }

        public void Trace(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Trace(exception, formatProvider, message, args);
        }

        public void Trace(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Trace(formatProvider, message, args);
        }

        public void Trace([Localizable(false)] string message)
        {
            logger.Trace(message);
        }

        public void Trace([Localizable(false)] string message, params object[] args)
        {
            logger.Trace(message, args);
        }

        public void Trace([Localizable(false)] string message, Exception exception)
        {
            logger.Trace(message, exception);
        }

        public void Trace<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace<TArgument>([Localizable(false)] string message, TArgument argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Trace(formatProvider, message, argument1, argument2);
        }

        public void Trace<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Trace(message, argument1, argument2);
        }

        public void Trace<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Trace(formatProvider, message, argument1, argument2, argument3);
        }

        public void Trace<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Trace(message, argument1, argument2, argument3);
        }

        public void Trace(object value)
        {
            logger.Trace(value);
        }

        public void Trace(IFormatProvider formatProvider, object value)
        {
            logger.Trace(formatProvider, value);
        }

        public void Trace(string message, object arg1, object arg2)
        {
            logger.Trace(message, arg1, arg2);
        }

        public void Trace(string message, object arg1, object arg2, object arg3)
        {
            logger.Trace(message, arg1, arg2, arg3);
        }

        public void Trace(IFormatProvider formatProvider, string message, bool argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, bool argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, char argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, char argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, byte argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, byte argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, string argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, string argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, int argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, int argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, long argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, long argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, float argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, float argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, double argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, double argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, decimal argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, decimal argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, object argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, object argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, sbyte argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, sbyte argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, uint argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, uint argument)
        {
            logger.Trace(message, argument);
        }

        public void Trace(IFormatProvider formatProvider, string message, ulong argument)
        {
            logger.Trace(formatProvider, message, argument);
        }

        public void Trace(string message, ulong argument)
        {
            logger.Trace(message, argument);
        }

        public void TraceException([Localizable(false)] string message, Exception exception)
        {
            logger.TraceException(message, exception);
        }

        public void Warn<T1>(T1 value)
        {
            logger.Warn(value);
        }

        public void Warn<T1>(IFormatProvider formatProvider, T1 value)
        {
            logger.Warn(formatProvider, value);
        }

        public void Warn(LogMessageGenerator messageFunc)
        {
            logger.Warn(messageFunc);
        }

        public void Warn(Exception exception, [Localizable(false)] string message)
        {
            logger.Warn(exception, message);
        }

        public void Warn(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            logger.Warn(exception, message, args);
        }

        public void Warn(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Warn(exception, formatProvider, message, args);
        }

        public void Warn(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Warn(formatProvider, message, args);
        }

        public void Warn([Localizable(false)] string message)
        {
            logger.Warn(message);
        }

        public void Warn([Localizable(false)] string message, params object[] args)
        {
            logger.Warn(message, args);
        }

        public void Warn([Localizable(false)] string message, Exception exception)
        {
            logger.Warn(message, exception);
        }

        public void Warn<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn<TArgument>([Localizable(false)] string message, TArgument argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Warn(formatProvider, message, argument1, argument2);
        }

        public void Warn<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Warn(message, argument1, argument2);
        }

        public void Warn<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Warn(formatProvider, message, argument1, argument2, argument3);
        }

        public void Warn<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Warn(message, argument1, argument2, argument3);
        }

        public void Warn(object value)
        {
            logger.Warn(value);
        }

        public void Warn(IFormatProvider formatProvider, object value)
        {
            logger.Warn(formatProvider, value);
        }

        public void Warn(string message, object arg1, object arg2)
        {
            logger.Warn(message, arg1, arg2);
        }

        public void Warn(string message, object arg1, object arg2, object arg3)
        {
            logger.Warn(message, arg1, arg2, arg3);
        }

        public void Warn(IFormatProvider formatProvider, string message, bool argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, bool argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, char argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, char argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, byte argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, byte argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, string argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, string argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, int argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, int argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, long argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, long argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, float argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, float argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, double argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, double argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, decimal argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, decimal argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, object argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, object argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, sbyte argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, sbyte argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, uint argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, uint argument)
        {
            logger.Warn(message, argument);
        }

        public void Warn(IFormatProvider formatProvider, string message, ulong argument)
        {
            logger.Warn(formatProvider, message, argument);
        }

        public void Warn(string message, ulong argument)
        {
            logger.Warn(message, argument);
        }

        public void WarnException([Localizable(false)] string message, Exception exception)
        {
            logger.WarnException(message, exception);
        }
    }
}