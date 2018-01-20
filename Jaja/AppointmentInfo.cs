using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaja
{
    public class AppointmentInfo
    {
        public AppointmentItem Appointment { get; private set; }
        public MAPIFolderInfo FolderInfo { get; private set; }
        public ProjectInfo ProjectInfo { get; set; }
        public string Project { get; private set; }
        public string Subject { get; private set; }
        public bool Enabled { get; set; }

        public AppointmentInfo(AppointmentItem appointment, MAPIFolderInfo folderInfo)
        {
            Appointment = appointment;
            FolderInfo = folderInfo;
            Subject = appointment.Subject;            
            Enabled = true;
            ProjectInfo = new ProjectInfo(Subject);
            Project = ProjectInfo.Name;
        }

        public override string ToString()
        {
            return $"{Subject} ({Project})";
        }

        public TimeSpan Duration {  get { return Appointment.End - Appointment.Start; } }
               
    }
}
