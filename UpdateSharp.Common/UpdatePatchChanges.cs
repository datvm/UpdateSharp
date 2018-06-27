using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UpdateSharp.Common
{
    public class UpdatePatchChanges
    {

        /// <summary>
        /// Deleted items since last patch
        /// </summary>
        public List<UpdatePatchFile> DeletedItems { get; set; } = new List<UpdatePatchFile>();

        /// <summary>
        /// Modified items (create or change) since last patch
        /// </summary>
        public List<UpdatePatchFile> Modifications { get; set; } = new List<UpdatePatchFile>();

    }

}
