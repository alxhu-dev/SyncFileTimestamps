namespace SyncFileTimestamps
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                Sync(args[0], args[1]);
            }
        }


        static void Sync(string sourcePath, string destinationPath)
        {
            FolderComparer folderComparer = new FolderComparer();
            foreach(FilePair filePair in folderComparer.GetFilePairs(sourcePath, destinationPath))
            {
                FileInfo sourceFileInfo = new FileInfo(filePair.SourcePath);
                FileInfo destinationFileInfo = new FileInfo(filePair.DestinationPath);

                Console.Write($"Setting {filePair.DestinationPath.Substring(destinationPath.Length)} ... ");
                destinationFileInfo.LastWriteTime = sourceFileInfo.LastWriteTime;
                destinationFileInfo.CreationTime = sourceFileInfo.CreationTime;
                destinationFileInfo.LastAccessTime = sourceFileInfo.LastAccessTime;
                Console.WriteLine("Done!");
            }
        }
    }
}
