using Microsoft.Extensions.Configuration;

namespace GetMediaCore.Traffic {
    internal class FileSysemAccessor {
        private IConfiguration _configuration;

        public FileSysemAccessor(IConfiguration configuration) {
            _configuration = configuration;
        }

        public List<FileInfo> GetFiles() {
            string? path = _configuration["AppSettings:PathToFLAC"];
            if (string.IsNullOrWhiteSpace(path)) {
                throw new InvalidOperationException("The configuration value for 'AppSettings:PathToFLAC' is missing or empty.");
            }

            DirectoryInfo di = new DirectoryInfo(path);
            return di.GetFiles("*.flac", SearchOption.AllDirectories).ToList();
        }
    }
}
