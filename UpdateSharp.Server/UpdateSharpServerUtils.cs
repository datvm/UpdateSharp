using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UpdateSharp.Common;

namespace UpdateSharp.Server
{

    public static class UpdateSharpServerUtils
    {

        public static UpdatePatchFile GenerateRootUpdatePatchFile(string folder, Version version)
        {
            var result = new UpdatePatchFile()
            {
                Name = "",
                FullPath = "",
                LatestAvailableSince = version,
                SubItems = new Dictionary<string, UpdatePatchFile>(),
            };

            Action<string, string, UpdatePatchFile> scan = null;
            scan = (currentFolder, currentPath, currentFile) =>
            {
                var subFiles = Directory.GetFiles(currentFolder);
                foreach (var subFile in subFiles)
                {
                    var fileName = Path.GetFileName(subFile);

                    string hashCode;
                    using (var fileStream = new FileStream(subFile, FileMode.Open))
                    {
                        hashCode = UpdateSharpSettings.HashFunction(fileStream);
                    }

                    currentFile.SubItems.Add(fileName, new UpdatePatchFile()
                    {
                        Name = fileName,
                        FullPath = currentPath + fileName,
                        HashCode = hashCode,
                        LatestAvailableSince = version,
                    });
                }

                var subFolders = Directory.GetDirectories(currentFolder);
                foreach (var subFolder in subFolders)
                {
                    var folderName = Path.GetFileName(subFolder);

                    var patchFolder = new UpdatePatchFile()
                    {
                        Name = folderName,
                        FullPath = currentPath + folderName,
                        LatestAvailableSince = version,
                        SubItems = new Dictionary<string, UpdatePatchFile>(),
                    };

                    scan(subFolder, patchFolder.FullPath + "/", patchFolder);

                    currentFile.SubItems.Add(folderName, patchFolder);
                }
            };

            scan(folder, "", result);

            return result;
        }

    }

}
