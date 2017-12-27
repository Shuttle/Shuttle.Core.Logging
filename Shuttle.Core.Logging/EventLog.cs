using System;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Logging
{
    public class EventLog : AbstractLog
    {
        private readonly Type _logtype;

        public EventLog(Type type)
        {
            Guard.AgainstNull(type, "type");

            _logtype = type;
        }

        public static event LoggerDelegate LogFine = delegate { };
        public static event LoggerDelegate LogTrace = delegate { };
        public static event LoggerDelegate LogDebug = delegate { };
        public static event LoggerDelegate LogInformation = delegate { };
        public static event LoggerDelegate LogWarning = delegate { };
        public static event LoggerDelegate LogError = delegate { };
        public static event LoggerDelegate LogFatal = delegate { };

        public override void Verbose(string message)
        {
            LogFine.Invoke(_logtype, message);
        }

        public override void Trace(string message)
        {
            LogTrace.Invoke(_logtype, message);
        }

        public override void Debug(string message)
        {
            LogDebug.Invoke(_logtype, message);
        }

        public override void Information(string message)
        {
            LogInformation.Invoke(_logtype, message);
        }

        public override void Error(string message)
        {
            LogError.Invoke(_logtype, message);
        }

        public override void Fatal(string message)
        {
            LogFatal.Invoke(_logtype, message);
        }

        public override ILog For(Type type)
        {
            Guard.AgainstNull(type, "type");

            return new EventLog(type) {LogLevel = LogLevel};
        }

        public override ILog For(object instance)
        {
            Guard.AgainstNull(instance, "instance");

            return For(instance.GetType());
        }

        public override void Warning(string message)
        {
            LogWarning.Invoke(_logtype, message);
        }
    }

    public delegate void LoggerDelegate(Type type, string message);
}