using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaja
{
    public class Project
    {
        public List<ProjectItem> list = new List<ProjectItem>();
        public string ProjectName { get; private set; }
                

        public Project(string project)
        {
            ProjectName = project;
        }

        public void AddItem(ProjectItem item)
        {
            if (item.Project == this.ProjectName)
                list.Add(item);
        }

        public TimeSpan GetDuration()
        {
            TimeSpan ts = new TimeSpan(0);
                        
            foreach (var i in list)
            {
                if (i.Enabled)
                    ts += i.Duration;
            }
            return ts;
        }

        public bool AnyEnabled
        {
            get
            {
                foreach (ProjectItem pi in list)
                    if (pi.Enabled)
                        return true;
                return false;
            }
        }

        public bool? Enabled // pokud vsechny jsou true/false, vrati to, pokud jsou ruzne, vraci null
        {
            set { if (value.HasValue) foreach (var i in list) i.Enabled = value.Value; }
            get
            {
                bool? first = null;
                foreach (var i in list)
                {
                    if (!first.HasValue)
                        first = i.Enabled;

                    if (i.Enabled != first.Value)
                        return null;
                }
                return first;
            }            
        }
    }
}
