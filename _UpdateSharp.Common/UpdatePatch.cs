using System;
using System.Collections.Generic;
using System.Text;

namespace UpdateSharp.Common
{

    public class UpdatePatch
    {

        public string VersionText { get; set; }
        public Version VersionCode { get; set; }
        public string ChangeLog { get; set; }

        public List<UpdatePatchFileChange> FileChanges { get; set; }

    }

    public class UpdatePatchFileChange
    {

        public bool IsFolder { get; set; }
        public string Path { get; set; }
        public FileChange Type { get; set; }
        public string HashCode { get; set; }

    }

    public enum FileChange
    {
        Create,
        Modify,
        Delete,
    }

}
