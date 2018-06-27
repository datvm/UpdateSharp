using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UpdateSharp.Test.TestData
{

    [TestClass]
    public class TestCreator
    {

        const int MaxDepth = 3;
        const int MaxFileCount = 10;
        private static readonly string[] Extensions =
        {
            ".dat",".txt"
        };

        

        /// <summary>
        /// WARNING: the rootFolder will be deleted before creating.
        /// </summary>
        /// <param name="rootFolder"></param>
        [TestMethod]
        public async Task CreateRandomStructure()
        {
            await CreateRandomStructure(@"D:\Temp\updater\1.0", "-Version1");
            await CreateRandomStructure(@"D:\Temp\updater\1.1", "-Version11");
            await CreateRandomStructure(@"D:\Temp\updater\2.0", "-Version2");
            await CreateRandomStructure(@"D:\Temp\updater\2.0.1", "-Version201");
            await CreateRandomStructure(@"D:\Temp\updater\2.1", "-Version21");
        }

        public async Task CreateRandomStructure(string rootFolder, string versionPostfix)
        {
            if (Directory.Exists(rootFolder))
            {
                Directory.Delete(rootFolder, true);
            }

            // Make sure folder is deleted
            await Task.Delay(500);

            Directory.CreateDirectory(rootFolder);

            var random = new Random();

            Action<string, string, int> createCurrentFolder = null;
            createCurrentFolder = (currentFolder, currentPath, currentDepth) =>
            {
                var fileCount = random.Next(MaxFileCount);

                for (int i = 0; i < fileCount; i++)
                {
                    var extension = Extensions[random.Next(Extensions.Length)];
                    var fileName = $"file-{currentPath}{i}{extension}";
                    var filePath = Path.Combine(currentFolder, fileName);

                    var fileContent = fileName;
                    if (random.Next(2) == 0)
                    {
                        fileContent += versionPostfix;
                    }

                    File.WriteAllText(filePath, fileContent);
                }

                if (currentDepth >= MaxDepth)
                {
                    return;
                }

                var folderCount = random.Next(MaxFileCount);
                for (int i = 0; i < folderCount; i++)
                {
                    var folderName = $"folder-{currentPath}{i}";
                    var folderPath = Path.Combine(currentFolder, folderName);

                    Directory.CreateDirectory(folderPath);

                    createCurrentFolder(folderPath + "/", currentPath + i + "-", currentDepth + 1);
                }
            };

            createCurrentFolder(rootFolder, "", 0);
        }

    }

}
