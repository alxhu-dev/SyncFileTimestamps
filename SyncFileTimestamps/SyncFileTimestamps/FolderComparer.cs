using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncFileTimestamps
{
    public class FolderComparer
    {

        public IEnumerable<FilePair> GetFilePairs(string sourcePath, string destinationPath)
        {
            IEnumerable<string> sourceFiles = Directory.EnumerateFiles(sourcePath, "*", SearchOption.AllDirectories).ToList();
            
            IEnumerable<string> destinationFiles = Directory.EnumerateFiles(destinationPath, "*", SearchOption.AllDirectories).ToList();

            foreach (string sourceFile in sourceFiles)
            {
                string sourceFileWithoutPrefix = sourceFile.Substring(sourcePath.Length);
                string? destinationFile = destinationFiles.FirstOrDefault(d => d.Substring(destinationPath.Length).Equals(sourceFileWithoutPrefix));
                if (destinationFile != null)
                    yield return new FilePair(sourceFile,  destinationFile);
            }
        }
    }
}
