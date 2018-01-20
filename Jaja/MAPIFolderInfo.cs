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

        public MAPIFolderInfo(MAPIFolder folder)
        {
            Folder = folder;
        }

        public override string ToString()
        {
            return Folder.FolderPath.TrimStart('\\');
            //int p = path.LastIndexOf('\\');
            
            //if (p > 0)
            //{
            //    return path.Substring(0, p);
            //}
            //return path;
        }
    }
}
