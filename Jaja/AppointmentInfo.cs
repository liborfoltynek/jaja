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
        public string Project { get; private set; }
        public string Subject { get; private set; }

        public AppointmentInfo(AppointmentItem appointment, MAPIFolderInfo folderInfo)
        {
            Appointment = appointment;
            FolderInfo = folderInfo;
            Subject = appointment.Subject;
            Project = GetProjectName(Subject);
        }

        public override string ToString()
        {
            return $"{Subject} ({Project})";
        }

        private string GetProjectName(string subject)
        {
            if (string.IsNullOrWhiteSpace(subject))
                return "Neidentifikovatelný projekt";

            if (!subject.Contains(' ') && !subject.Contains('-'))
                return subject.Trim();
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
