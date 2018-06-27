using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UpdateSharp.Common
{

    public static class UpdateSharpSettings
    {
        public static Func<Stream, string> HashFunction { get; set; } 
            = DefaultHashFunction;

        public static Func<Task<UpdatePatch>> LoadCurrentVersionFunction { get; set; } 
            = DefaultLoadCurrentVersionFunction;
        public static Func<UpdatePatch, Task> SaveCurrentVersionFunction { get; set; } 
            = DefaultSaveCurrentVersionFunction;

        public static string DefaultVersionFileName { get; set; } 
            = "version.json";

        private static string DefaultHashFunction(Stream data)
        {
            using (var md5 = MD5.Create())
            {
                var hashed = md5.ComputeHash(data);

                return Convert.ToBase64String(hashed);
            }
        }

        private static Task<UpdatePatch> DefaultLoadCurrentVersionFunction()
        {
            return Task.Factory.StartNew(() =>
            {
                var filePath = DefaultVersionFileName;
                var fileContent = File.ReadAllText(filePath);

                return JsonConvert.DeserializeObject<UpdatePatch>(fileContent);
            });
        }

        private static Task DefaultSaveCurrentVersionFunction(UpdatePatch patch)
        {
            return Task.Factory.StartNew(() =>
            {
                var filePath = DefaultVersionFileName;
                File.WriteAllText(filePath, JsonConvert.SerializeObject(patch));
            });
        }

    }

}
