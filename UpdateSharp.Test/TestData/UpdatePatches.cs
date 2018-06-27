using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateSharp.Common;

namespace UpdateSharp.Test.TestData
{

    public class UpdatePatches
    {

        public static readonly UpdatePatch V1;
        public static readonly UpdatePatch V101;
        public static readonly UpdatePatch V21;

        static UpdatePatches()
        {
            V1 = CreateFromFileList("TestData/test-1.json", "1.0", new Version("1.0"));
            V101 = CreateFromFileList("TestData/test-101.json", "1.0.1", new Version("1.0.1"));
            V21 = CreateFromFileList("TestData/test-21.json", "2.1", new Version("2.1"));
        }

        private static UpdatePatch CreateFromFileList(string file, string versionText, Version version)
        {
            return new UpdatePatch()
            {
                ChangeLog = versionText,
                VersionCode = version,
                VersionText = versionText,
                Files = JsonConvert.DeserializeObject<UpdatePatchFile>(
                    File.ReadAllText(file)),
            };
        }

    }

}
