using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaja
{
    public class Tools
    {
        public static int CompareItems(ProjectItem x, ProjectItem y)
        {
            if (x.Start < y.Start) return -1;
            if (x.Start > y.Start) return 1;
            return 0;
        }
    }
}
