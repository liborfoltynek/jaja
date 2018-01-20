using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jaja
{
    public class Tools
    {
        //public static int CompareItems(ProjectItem x, ProjectItem y)
        //{
        //    if (x.Start < y.Start) return -1;
        //    if (x.Start > y.Start) return 1;
        //    return 0;
        //}

        public static TimeSpan GetDuration(List<AppointmentInfo> allAppointments, string projectName)
        {
            double sec = allAppointments.Where(x => x.Enabled && x.Project == projectName).Sum(i => (i.Appointment.End - i.Appointment.Start).TotalSeconds);
            return TimeSpan.FromSeconds(sec);
        }

        public static TimeSpan GetDuration(List<AppointmentInfo> allAppointments)
        {
            double sec = allAppointments.Where(x => x.Enabled).Sum(i => (i.Appointment.End - i.Appointment.Start).TotalSeconds);
            return TimeSpan.FromSeconds(sec);
        }        
    }
}
