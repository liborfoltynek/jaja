using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaja
{
    public class ProjectInfo
    {
        public string Name { get; private set; }

        public ProjectInfo(string name)
        {
            Name = GetProjectName(name);
        }

        private string GetProjectName(string subject)
        {
            if (string.IsNullOrWhiteSpace(subject))
                return "Neidentifikovatelný projekt".ToUpper();

            if (!subject.Contains(' ') && !subject.Contains('-'))
                return subject.Trim().ToUpper();

            int i1 = subject.IndexOf(' ');
            int i2 = subject.IndexOf('-');
            int i = 0;
            if (i1 == -1)
                i = i2;
            else if (i2 == -1)
                i = i1;
            else if (i1 < i2)
                i = i1;
            else i = i2;

            return subject.Substring(0, i).Trim().ToUpper();
        }
    }
}
