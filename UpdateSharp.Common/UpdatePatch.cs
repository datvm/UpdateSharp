using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UpdateSharp.Common
{

    public class UpdatePatch
    {

        public string VersionText { get; set; }
        public Version VersionCode { get; set; }
        public string ChangeLog { get; set; }

        public UpdatePatchFile Files { get; set; }

        public UpdatePatchFile SearchFile(string path)
        {
            var parts = path.Split('/');

            var currentFile = this.Files;
            foreach (var part in parts)
            {
                if (!currentFile.IsFolder ||
                    !currentFile.SubItems.TryGetValue(part, out currentFile))
                {
                    return null;
                }
            }

            return currentFile;
        }
    }

    public class UpdatePatchFile
    {

        /// <summary>
        /// A folder is determined when SubItems is null
        /// </summary>
        public bool IsFolder
        {
            get
            {
                return this.SubItems != null;
            }
        }

        /// <summary>
        /// The name only, not full path
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Full Directory
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// Null if it is a folder
        /// </summary>
        public string HashCode { get; set; }

        /// <summary>
        /// Latest version that the file has not changed
        /// </summary>
        public Version LatestAvailableSince { get; set; }

        /// <summary>
        /// Only has item when it is a folder and does not have hash code.
        /// The Dictionary key is the file/folder name and the value is its info
        /// </summary>
        public Dictionary<string, UpdatePatchFile> SubItems { get; set; }

    }



}
