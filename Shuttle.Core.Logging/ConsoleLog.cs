using System;
using System.Threading;
using Shuttle.Core.Contract;

namespace Shuttle.Core.Logging
{
    public class ConsoleLog : AbstractLog
    {
        private static readonly object Lock = new object();
        private readonly Type _logtype;

        public ConsoleLog(Type type)
        {
            Guard.AgainstNull(type, "type");

            _logtype = type;
        }

        public new LogLevel LogLevel
        {
            get => base.LogLevel;
            set => base.LogLevel = value;
        }

        public override void Verbose(string message)
        {
            if (!IsVerboseEnabled)
            {
                return;
            }

            WriteLog(ConsoleColor.DarkGray, "VERBOSE", message);
        }

        private void WriteLog(ConsoleColor color, string level, string message)
        {
            lock (Lock)
            {
                var foregroundColor = Console.ForegroundColor;

                Console.ForegroundColor = color;
                Console.WriteLine("{0} [{1}] {2} {3} - {4}", Now(), Thread.CurrentThread.ManagedThreadId,
                    $"{level,-7}", _logtype.FullName, message);
                Console.ForegroundColor = foregroundColor;
            }
        }

        public override void Trace(string message)
        {
            if (!IsTraceEnabled)
            {
                return;
            }

            WriteLog(ConsoleColor.Gray, "TRACE", message);
        }

        public override void Debug(string message)
        {
            if (!IsDebugEnabled)
            {
                return;
            }

            WriteLog(ConsoleColor.Magenta, "DEBUG", message);
        }

        private static string Now()
        {
            return DateTime.Now.ToString("yyyy/mm/dd HH:mm:ss.fff");
        }

        public override void Warning(string message)
        {
            if (!IsWarningEnabled)
            {
                return;
            }

            WriteLog(ConsoleColor.Yellow, "WARNING", message);
        }

        public override void Information(string message)
        {
            if (!IsInformationEnabled)
            {
                return;
            }

            WriteLog(ConsoleColor.White, "INFO", message);
        }

        public override void Error(string message)
        {
            if (!IsErrorEnabled)
            {
                return;
            }

            WriteLog(ConsoleColor.Red, "ERROR", message);
        }

        public override void Fatal(string message)
        {
            if (!IsFatalEnabled)
            {
                return;
            }

            WriteLog(ConsoleColor.DarkRed, "FATAL", message);
        }

        public override ILog For(Type type)
        {
            Guard.AgainstNull(type, "type");

            return new ConsoleLog(type) {LogLevel = LogLevel};
        }

        public override ILog For(object instance)
        {
            Guard.AgainstNull(instance, "instance");

            return For(instance.GetType());
        }
    }
}