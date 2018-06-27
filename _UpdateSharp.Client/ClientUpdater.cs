using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UpdateSharp.Common;

namespace UpdateSharp.Client
{

    public abstract class BaseClientUpdater
    {

        public string Server { get; protected set; }
        public string Resource { get; protected set; }

        protected List<UpdatePatch> Patches { get; set; }

        protected static HttpClient client = new HttpClient();
        
        public BaseClientUpdater(string server, string resource)
        {
            this.Server = server;
            this.Resource = resource;
        }

        public virtual async Task<IEnumerable<UpdatePatch>> CheckUpdatesAsync()
        {
            var currentPath = await this.GetCurrentPatchAsync();

            

            return null;
        }

        public abstract Task<UpdatePatch> GetCurrentPatchAsync();

    }

}
