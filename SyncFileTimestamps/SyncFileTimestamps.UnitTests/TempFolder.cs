using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncFileTimestamps.UnitTests
{
    internal class TempFolder : IDisposable
    {
        public string FolderPath { get; init; }
        public TempFolder()
        {
            do
            {
                FolderPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            } while (Directory.Exists(FolderPath));
            Directory.CreateDirectory(FolderPath);
        }
        public void Dispose()
        {
            Directory.Delete(FolderPath, true);
        }
    }
}
