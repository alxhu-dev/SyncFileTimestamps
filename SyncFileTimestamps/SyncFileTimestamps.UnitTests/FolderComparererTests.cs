namespace SyncFileTimestamps.UnitTests
{
    [TestClass]
    public class FolderComparererTests
    {



        [TestMethod]
        public void TestEmpty()
        {
            using (TempFolder tempFolder = new TempFolder())
            {
                string sourceTempFolder = Path.Combine(tempFolder.FolderPath, "source");
                Directory.CreateDirectory(sourceTempFolder);
                string destinationTempFolder = Path.Combine(tempFolder.FolderPath, "destination");
                Directory.CreateDirectory(destinationTempFolder);

                FolderComparer folderComparer = new FolderComparer();
                IEnumerable<FilePair> filePairs = folderComparer.GetFilePairs(sourceTempFolder, destinationTempFolder).ToList();

                Assert.AreEqual(0, filePairs.Count());
            }
        }

        [TestMethod]
        public void OnlyOneFile()
        {
            using (TempFolder tempFolder = new TempFolder())
            {
                string sourceTempFolder = Path.Combine(tempFolder.FolderPath, "source");
                Directory.CreateDirectory(sourceTempFolder);
                string destinationTempFolder = Path.Combine(tempFolder.FolderPath, "destination");
                Directory.CreateDirectory(destinationTempFolder);

                string sourceFilePath = Path.Combine(sourceTempFolder, "OneEqualFile.txt");
                File.WriteAllText(sourceFilePath, "test");
                string destFilePath = Path.Combine(destinationTempFolder, "OneEqualFile.txt");
                File.WriteAllText(destFilePath, "test");

                FolderComparer folderComparer = new FolderComparer();
                IEnumerable<FilePair> filePairs = folderComparer.GetFilePairs(sourceTempFolder, destinationTempFolder).ToList();

                Assert.AreEqual(1, filePairs.Count());
                Assert.AreEqual(sourceFilePath, filePairs.First().SourcePath);
                Assert.AreEqual(destFilePath, filePairs.First().DestinationPath);
            }
        }

        [TestMethod]
        public void NoEqualFile()
        {
            using (TempFolder tempFolder = new TempFolder())
            {
                string sourceTempFolder = Path.Combine(tempFolder.FolderPath, "source");
                Directory.CreateDirectory(sourceTempFolder);
                string destinationTempFolder = Path.Combine(tempFolder.FolderPath, "destination");
                Directory.CreateDirectory(destinationTempFolder);

                string sourceFilePath = Path.Combine(sourceTempFolder, "NoEqualFile.txt");
                File.WriteAllText(sourceFilePath, "test");
                string destFilePath = Path.Combine(destinationTempFolder, "NoEqualFile_.txt");
                File.WriteAllText(destFilePath, "test");

                FolderComparer folderComparer = new FolderComparer();
                IEnumerable<FilePair> filePairs = folderComparer.GetFilePairs(sourceTempFolder, destinationTempFolder).ToList();

                Assert.AreEqual(0, filePairs.Count());
            }
        }

        [TestMethod]
        public void OnlyOneEqualFile()
        {
            using (TempFolder tempFolder = new TempFolder())
            {
                string sourceTempFolder = Path.Combine(tempFolder.FolderPath, "source");
                Directory.CreateDirectory(sourceTempFolder);
                string destinationTempFolder = Path.Combine(tempFolder.FolderPath, "destination");
                Directory.CreateDirectory(destinationTempFolder);

                string sourceFilePath = Path.Combine(sourceTempFolder, "OnlyOneEqualFile.txt");
                File.WriteAllText(sourceFilePath, "test");
                string sourceFilePath2 = Path.Combine(sourceTempFolder, "AOnlyOneEqualFile.txt");
                File.WriteAllText(sourceFilePath2, "test");
                string destFilePath = Path.Combine(destinationTempFolder, "OnlyOneEqualFile.txt");
                File.WriteAllText(destFilePath, "test");
                string destFilePath2 = Path.Combine(destinationTempFolder, "BOnlyOneEqualFile.txt");
                File.WriteAllText(destFilePath2, "test");

                FolderComparer folderComparer = new FolderComparer();
                IEnumerable<FilePair> filePairs = folderComparer.GetFilePairs(sourceTempFolder, destinationTempFolder).ToList();

                Assert.AreEqual(1, filePairs.Count());
                Assert.AreEqual(sourceFilePath, filePairs.First().SourcePath);
                Assert.AreEqual(destFilePath, filePairs.First().DestinationPath);
            }
        }

        [TestMethod]
        public void OnlySource()
        {
            using (TempFolder tempFolder = new TempFolder())
            {
                string sourceTempFolder = Path.Combine(tempFolder.FolderPath, "source");
                Directory.CreateDirectory(sourceTempFolder);
                string destinationTempFolder = Path.Combine(tempFolder.FolderPath, "destination");
                Directory.CreateDirectory(destinationTempFolder);

                string sourceFilePath = Path.Combine(sourceTempFolder, "OnlyOneEqualFile.txt");
                File.WriteAllText(sourceFilePath, "test");

                FolderComparer folderComparer = new FolderComparer();
                IEnumerable<FilePair> filePairs = folderComparer.GetFilePairs(sourceTempFolder, destinationTempFolder).ToList();

                Assert.AreEqual(0, filePairs.Count());
            }
        }

        [TestMethod]
        public void OnlyDest()
        {
            using (TempFolder tempFolder = new TempFolder())
            {
                string sourceTempFolder = Path.Combine(tempFolder.FolderPath, "source");
                Directory.CreateDirectory(sourceTempFolder);
                string destinationTempFolder = Path.Combine(tempFolder.FolderPath, "destination");
                Directory.CreateDirectory(destinationTempFolder);

                string destinationFilePath = Path.Combine(destinationTempFolder, "OnlyOneEqualFile.txt");
                File.WriteAllText(destinationFilePath, "test");

                FolderComparer folderComparer = new FolderComparer();
                IEnumerable<FilePair> filePairs = folderComparer.GetFilePairs(sourceTempFolder, destinationTempFolder).ToList();

                Assert.AreEqual(0, filePairs.Count());
            }
        }

        [TestMethod]
        public void SubDirectories()
        {
            using (TempFolder tempFolder = new TempFolder())
            {
                string sourceTempFolder = Path.Combine(tempFolder.FolderPath, "source");
                Directory.CreateDirectory(sourceTempFolder);
                string destinationTempFolder = Path.Combine(tempFolder.FolderPath, "destination");
                Directory.CreateDirectory(destinationTempFolder);

                string sourceFilePath = Path.Combine(sourceTempFolder, "SubDirectories.txt");
                File.WriteAllText(sourceFilePath, "test");
                string destFilePath = Path.Combine(destinationTempFolder, "SubDirectories.txt");
                File.WriteAllText(destFilePath, "test");

                string sourceSubFolder = Path.Combine(sourceTempFolder, "sub", "sub", "sub");
                Directory.CreateDirectory(sourceSubFolder);
                string destinationSubFolder = Path.Combine(destinationTempFolder, "sub", "sub", "sub");
                Directory.CreateDirectory(destinationSubFolder);

                string sourceSubFilePath = Path.Combine(sourceSubFolder, "SubDirectories.txt");
                File.WriteAllText(sourceSubFilePath, "test");
                string destSubFilePath = Path.Combine(destinationSubFolder, "SubDirectories.txt");
                File.WriteAllText(destSubFilePath, "test");

                FolderComparer folderComparer = new FolderComparer();
                IEnumerable<FilePair> filePairs = folderComparer.GetFilePairs(sourceTempFolder, destinationTempFolder).ToList();

                Assert.AreEqual(2, filePairs.Count());
                Assert.AreEqual(sourceFilePath, filePairs.First().SourcePath);
                Assert.AreEqual(destFilePath, filePairs.First().DestinationPath);
                Assert.AreEqual(sourceSubFilePath, filePairs.Last().SourcePath);
                Assert.AreEqual(destSubFilePath, filePairs.Last().DestinationPath);
            }
        }

    }
}