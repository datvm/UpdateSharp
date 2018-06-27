using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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

        [AssemblyInitialize]
        public static void InitAllTests(TestContext context)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                Converters = new List<JsonConverter>() { new VersionConverter(), },
            };
        }

        [TestMethod]
        public void GenerateAllRootUpdatePatchFile()
        {
            GenerateRootUpdatePatchFileTest(@"D:\Temp\updater\1.0", @"D:\Temp\updater\test-1.json");
            GenerateRootUpdatePatchFileTest(@"D:\Temp\updater\1.0.1", @"D:\Temp\updater\test-101.json");
            GenerateRootUpdatePatchFileTest(@"D:\Temp\updater\2.1", @"D:\Temp\updater\test-21.json");
        }

        [TestMethod]
        public void GenerateRootUpdatePatchFileTest()
        {
            GenerateRootUpdatePatchFileTest(@"D:\Temp\updater\1.0", @"D:\Temp\updater\test-1.json");
        }

        public void GenerateRootUpdatePatchFileTest(string TestFolder, string WriteOutputTo)
        {
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
