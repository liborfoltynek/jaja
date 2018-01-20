using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaja
{
    public class MAPIFolderInfo
    {
        public MAPIFolder Folder { get; private set; }
        private string toString;
        
        /// <summary>
        /// uz bylo nacteno?
        /// </summary>
        public bool WasProcessed { get; set; }

        public MAPIFolderInfo(MAPIFolder folder)
        {            
            Folder = folder;
            WasProcessed = false;
            toString = Folder.FolderPath.TrimStart('\\');
        }

        public override string ToString()
        {

            return toString;
            //int p = path.LastIndexOf('\\');

            //if (p > 0)
            //{
            //    return path.Substring(0, p);
            //}
            //return path;
        }
    }
}
