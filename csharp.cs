using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateProjectApp
{
    internal class csharp
    {
        public string Name;
        public string Type;
        public string Location;
        public string startCommand;

        public csharp(string name, string type, string location)
        {
            Name = name; Type = type; Location = location;
            startCommand = $"new {Type} -f net7.0 --language C# --name {Name} -o \"{Path.Combine(Location,Name)}\"";
            if (!Directory.Exists(location))
            {
                throw new Exception("LOCATION DOES NOT EXIST.. TRY AGIAN");
            }
        }

        public void Create()
        {
            string gitignoreurl = @"https://gist.githubusercontent.com/xmikedanielsx/03983482114e41e5a4358645e31db7be/raw/66608bd60a2fe6ea1ef30c226f0e5e2c076b3ec4/gitignore%2520VisualStudio";
            string projectLocation = Path.Combine(Location, Name);
            string gitignorepath = Path.Combine(projectLocation, ".gitignore");
            string readmepath = Path.Combine(projectLocation, "README.md");
            Console.WriteLine($"Going to create a project of type {Type} with the name of {Name} in the location of");
            Console.WriteLine(projectLocation);

            if (Directory.Exists(projectLocation)) {
                Console.WriteLine("Location Already Exists");
                return;
            }
            Directory.CreateDirectory(projectLocation);
            


            RunProcess.Now("dotnet", startCommand);
            RunProcess.Now("git", "init", projectLocation);
            using (StreamWriter outputFile = new StreamWriter(gitignorepath))
            {
                foreach (string line in Helpers.GetFileContent(gitignoreurl))
                    outputFile.WriteLine(line);
            }
            using (StreamWriter outputFile = new StreamWriter(gitignorepath))
            {
                outputFile.WriteLine($"#Project: {Name}");
            }

            RunProcess.Now("git", "add .gitignore", projectLocation);
            RunProcess.Now("git", "add README.md", projectLocation);
            RunProcess.Now("git", "commit -m \"Initial GitIgnore and ReadMe\"", projectLocation);
            RunProcess.Now("git", "add .", projectLocation);
            RunProcess.Now("git", "commit -m \"Initial Commit\"", projectLocation);
        }

    }
}
