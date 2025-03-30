using Serilog;
using Serilog.Events;
using System;

namespace mdl_parser.src.logging {
    public class Logger {
        private static ILogger _logger;

        static Logger() {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug() // Set the logging level
                .WriteTo.Console() // Output to Console
                .WriteTo.File("logs/log.txt",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7, // Keeps logs for 7 days
                    fileSizeLimitBytes: 10_000_000, // 10MB max file size
                    restrictedToMinimumLevel: LogEventLevel.Information) // Minimum level for file logging
                .Enrich.WithThreadId()
                .Enrich.WithProcessId()
                .CreateLogger();
        }

        public static void Info(string message) {
            _logger.Information(message);
        }

        public static void Debug(string message) {
            _logger.Debug(message);
        }

        public static void Warning(string message) {
            _logger.Warning(message);
        }

        public static void Error(string message, Exception ex = null) {
            if (ex != null)
                _logger.Error(ex, message);
            else
                _logger.Error(message);
        }
    }
}
