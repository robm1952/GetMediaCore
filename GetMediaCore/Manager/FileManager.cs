using System;
using GetMediaCore.Traffic;
using Microsoft.Extensions.Configuration;

namespace GetMediaCore.Manager
{
    public class FileManager //: Managers
    {
        private readonly IConfiguration _config;
        public FileManager(IConfiguration configuration) /// : base(configuration)
        {
            _config = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        
        public List<FileInfo> GetFiles()
        {
            FileSysemAccessor fsa = new FileSysemAccessor(_config);
            List<FileInfo> lfi = fsa.GetFiles();
            return lfi;
        }
    }
}
