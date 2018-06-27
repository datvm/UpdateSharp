using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateSharp.Common;
using UpdateSharp.Server;

namespace UpdateSharp.Test.Server
{

    [TestClass]
    public class UpdateSharpServerUtilsTests
    {

        [TestMethod]
        public void GenerateRootUpdatePatchFileTest()
        {
            const string TestFolder = @"D:\Temp\updater\2.1";
            const string WriteOutputTo = @"D:\Temp\updater\test-21.json";

            var result = UpdateSharpServerUtils
                .GenerateRootUpdatePatchFile(TestFolder, new Version("1.0"));

            Assert.IsNotNull(result);

            if (!string.IsNullOrEmpty(WriteOutputTo))
            {
                File.WriteAllText(WriteOutputTo, JsonConvert.SerializeObject(result));
            }

            this.ValidateUpdatePatchFile(result, TestFolder);
        }

        private void ValidateUpdatePatchFile(UpdatePatchFile updatePatchFile, string folder)
        {
            // TODO: add validation with folder structure
        }

    }

}
