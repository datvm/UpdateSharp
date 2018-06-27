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
using UpdateSharp.Test.TestData;

namespace UpdateSharp.Test.Common
{

    [TestClass]
    public class UpdateSharpUtilsTests
    {

        [TestMethod]
        public void TestParseChanges1()
        {
            const string WriteOutputTo = @"D:\Temp\updater\v1-101.json";

            var result = UpdateSharpUtils.ParseChanges(
                UpdatePatches.V1,
                UpdatePatches.V101);

            if (!string.IsNullOrEmpty(WriteOutputTo))
            {
                File.WriteAllText(WriteOutputTo, JsonConvert.SerializeObject(result));
            }
        }

        [TestMethod]
        public void TestParseChanges2()
        {
            const string WriteOutputTo = @"D:\Temp\updater\v1-21.json";

            var result = UpdateSharpUtils.ParseChanges(
                UpdatePatches.V1,
                UpdatePatches.V21);

            if (!string.IsNullOrEmpty(WriteOutputTo))
            {
                File.WriteAllText(WriteOutputTo, JsonConvert.SerializeObject(result));
            }
        }

        [TestMethod]
        public void TestParseChanges3()
        {
            const string WriteOutputTo = @"D:\Temp\updater\v101-21.json";

            var result = UpdateSharpUtils.ParseChanges(
                UpdatePatches.V101,
                UpdatePatches.V21);

            if (!string.IsNullOrEmpty(WriteOutputTo))
            {
                File.WriteAllText(WriteOutputTo, JsonConvert.SerializeObject(result));
            }
        }

    }

}
