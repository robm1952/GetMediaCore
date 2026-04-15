using Microsoft.Extensions.Configuration;
using GetMediaCore.Traffic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetMediaCore.Manager
{
    internal class TrafficManager
    {
        private IConfiguration _configuration;
        public FileSysemAccessor _fsa;
        public TagSharp _tagSharp;

        public TrafficManager (IConfiguration configuration)
        {
            _configuration = configuration;
            _fsa = new FileSysemAccessor (configuration);
            _tagSharp = new TagSharp(configuration);
        }

        public void ProcessFiles(IEnumerable<FileInfo> files) {
            // wrapper to avoid exposing TagSharp publicly
            _tagSharp.ProcessListToDb(files.ToList());
        }
    }
}
