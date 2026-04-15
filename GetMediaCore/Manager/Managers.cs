using Microsoft.Extensions.Configuration;

namespace GetMediaCore.Manager
{
    internal class Managers
    {
        // placeholder class to hold all manager classes
        private IConfiguration _config;
        private DbManager _dbManager;
        private FileManager _fileManager;
        private TrafficManager _trafficManager;
        public Managers(IConfiguration configuration) {
             _config = configuration;
            _dbManager = new DbManager(configuration);
            _fileManager = new FileManager(configuration);
            _trafficManager = new TrafficManager(configuration);
        }

        public void DoNothing() {
            System.Diagnostics.Debug.WriteLine("Managers.DoNothing()");
        }

        public DbManager GetDBManager() {
            System.Diagnostics.Debug.WriteLine("Managers.GetDBManager()");
            return _dbManager;
        }

        public FileManager GetFileManager()
        {
            System.Diagnostics.Debug.WriteLine("Managers.GetFileManager()");
            return _fileManager;
        }

        public TrafficManager GetTrafficManager()
        {
            System.Diagnostics.Debug.WriteLine("Managers.GetTrafficManager()");
            return _trafficManager;
        }
    }
}
