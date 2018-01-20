using System;
using System.IO;

namespace Jaja
{
    public static class Log
    {
        public static string File { private set; get; }

        static Log()
        {
            string tempFolder = Path.Combine(Path.GetTempPath(), "jaja");
            Directory.CreateDirectory(tempFolder);
            File = Path.Combine(tempFolder, DateTime.Now.ToString("jaja-yyyy-MM-dd_HH-mm-ss") + ".txt");
        }

        public static void Write(string txt)
        {
            using (StreamWriter w = new StreamWriter(File, true))
            {
                w.WriteLine($"[{DateTime.Now}]\t{txt}");
            }
        }
    }
}
