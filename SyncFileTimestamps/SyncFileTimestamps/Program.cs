namespace SyncFileTimestamps
{
    internal class Program
    {
        private const int MAX_PARALLEL = 4;

        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                Sync(args[0], args[1]);
            }
        }


        static void Sync(string sourcePath, string destinationPath)
        {
            List<Task> taskList = new List<Task>();

            FolderComparer folderComparer = new FolderComparer();
            foreach(FilePair filePair in folderComparer.GetFilePairs(sourcePath, destinationPath))
            {
                if (taskList.Count >= MAX_PARALLEL)
                    RemoveFinishedFromList();

                taskList.Add(Task.Run(() => HandleFilePair(filePair)));
            }

            while (taskList.Any())
                RemoveFinishedFromList();

            void RemoveFinishedFromList()
            {
                Task<Task> waitTask = Task.WhenAny(taskList);
                Task completedTask = waitTask.Result;

                taskList.Remove(completedTask);
            }
        }

        static void HandleFilePair(FilePair filePair)
        {
            FileInfo sourceFileInfo = new FileInfo(filePair.SourcePath);
            FileInfo destinationFileInfo = new FileInfo(filePair.DestinationPath);

            destinationFileInfo.LastWriteTime = sourceFileInfo.LastWriteTime;
            destinationFileInfo.CreationTime = sourceFileInfo.CreationTime;
            destinationFileInfo.LastAccessTime = sourceFileInfo.LastAccessTime;
            Console.WriteLine($"Set {Path.GetFileName(filePair.DestinationPath)}!");
        }
    }
}
