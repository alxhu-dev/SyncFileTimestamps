using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncFileTimestamps
{
    public struct FilePair
    {
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }

        public FilePair(string sourcePath, string destinationPath)
        {
            SourcePath = sourcePath;
            DestinationPath = destinationPath;
        }
    }
}
