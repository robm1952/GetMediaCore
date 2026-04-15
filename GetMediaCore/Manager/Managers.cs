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
        private readonly ILogger<Managers> _logger;
        public Managers(IConfiguration configuration, ILogger<Managers>? logger = null) {
            _config = configuration;
            _logger = logger ?? NullLogger<Managers>.Instance;
            _dbManager = new DbManager(configuration);
            _fileManager = new FileManager(configuration);
            _trafficManager = new TrafficManager(configuration);
        }

        public void DoNothing() {
            _logger.LogInformation("Managers.DoNothing()");
        }

        public DbManager GetDBManager() {
            return _dbManager;
        }

        public FileManager GetFileManager()
        {
            return _fileManager;
        }

        public TrafficManager GetTrafficManager()
        {
            return _trafficManager;
        }
    }
}
