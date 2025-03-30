using Serilog;
using Serilog.Events;
using System;

namespace mdl_parser.src.logging {
    public class Logger {
        private static ILogger _logger;

        static Logger() {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug() // Set the logging level
                .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message}{NewLine}{Exception}") // Output to Console
                .WriteTo.File($"logs/log-{DateTime.Now:dd-MM-yyyy__HH-mm-ss}.txt",
                    outputTemplate: "{Timestamp:dd-MM-yyyy HH:mm:ss.fff zzz} [{Level:u3}] {Message}{NewLine}{Exception}",
                    retainedFileCountLimit: 7, // Keeps logs for 7 days
                    fileSizeLimitBytes: 10_000_000, // 10MB max file size
                    restrictedToMinimumLevel: LogEventLevel.Information) // Minimum level for file logging
                .Enrich.WithThreadId()
                .Enrich.WithProcessId()
                .CreateLogger();

            // Add a separator at startup
            _logger.Information("\n\n");
            _logger.Information("=============================================");
            _logger.Information("         APPLICATION STARTED");
            _logger.Information("=============================================\n\n");
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

    // Example Usage
        // Logger.Info("Application started");
        // Logger.Debug("This is a debug message");
        // Logger.Warning("This is a warning message");
        
        // try
        //    {
        //     throw new Exception("Something went wrong!");
        //}
        // catch (Exception ex)
        // {
        //    Logger.Error("An error occurred", ex);
        //  }
}
