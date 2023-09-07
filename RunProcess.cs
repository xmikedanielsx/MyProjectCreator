using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateProjectApp
{
    internal static class RunProcess
    {
        static string binPath = "";
        public static string Now(string fileName, string args, string startDir = null)
        {
            try
            {
                string progPath = GetPath(fileName);
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = progPath,
                        Arguments = args,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                if(startDir != null )
                {
                    process.StartInfo.WorkingDirectory = startDir;
                }

                process.Start();
                string lineoutput = "";

                while (!process.StandardOutput.EndOfStream)
                {
                    var line = process.StandardOutput.ReadLine();
                    lineoutput += line;
                }

                process.WaitForExit();
                return lineoutput;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
                //return null;
                throw;
            }
        }
        public static string GetPath(string progName)
        {
            var extensions = new List<string> { ".com", ".exe",".bat" };
            string envPath = Environment.GetEnvironmentVariable("Path");
            var dirs = envPath.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string d in dirs.Where(f => Directory.Exists(f)))
            {
                foreach (var f in (Directory.EnumerateFiles(d).
                    Where(thisFile => extensions.Any(h => Path.GetExtension(thisFile).Equals(h, StringComparison.InvariantCultureIgnoreCase)))))
                {
                    if (Path.GetFileNameWithoutExtension(f).Equals(progName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return f;
                    }
                }
            }
            return null;
        }


    }
}
