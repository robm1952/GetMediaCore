using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace GetMediaCore.Manager
{
    internal class Managers
    {
        // placeholder class to hold all manager classes
        private IConfiguration _config;
        private DbManager _dbManager;
        private FileManager _fileManager;
        private TrafficManager _trafficManager;
        private readonly ILoggerFactory? _loggerFactory;
        private readonly ILogger<Managers> _logger;
        public Managers(IConfiguration configuration, ILoggerFactory? loggerFactory = null) {
             _config = configuration;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory?.CreateLogger<Managers>() ?? NullLogger<Managers>.Instance;
            _logger.LogInformation("Managers constructor called");
            _dbManager = new DbManager(configuration, loggerFactory?.CreateLogger<DbManager>());
            _fileManager = new FileManager(configuration);
            _trafficManager = new TrafficManager(configuration);
        }

        public void DoNothing() {
            _logger.LogInformation("Managers.DoNothing()");
        }

        public DbManager GetDBManager() {
            _logger.LogInformation("Managers.GetDBManager()");
            return _dbManager;
        }

        public FileManager GetFileManager()
        {
            _logger.LogInformation("Managers.GetFileManager()");
            return _fileManager;
        }

        public TrafficManager GetTrafficManager()
        {
            _logger.LogInformation("Managers.GetTrafficManager()");
            return _trafficManager;
        }
    }
}
