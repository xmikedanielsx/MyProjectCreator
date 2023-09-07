using CreateProjectApp;
using Microsoft.Extensions.Configuration;



using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;

// Clear up the console screen
Console.Clear();
//Console.WriteLine(new string('\n', 2));

#region ConfigBuilder
IConfiguration config = new ConfigurationBuilder()
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .Build();

IEnumerable<SettingsProjectLocation> projectLocations = config.GetSection("projectLocations").Get<IEnumerable<SettingsProjectLocation>>();
#endregion

#region Variables
object codeLang = askLanguage();
csharp cSharpProject;
php phpProject;
#endregion

#region Functions/Helpers
php createPhpProject()
{
    php p;
    try
    {
        p = new php();
        return p;

    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        throw;
    }

}
object askLanguage()
{
    Console.WriteLine(""
        + "Which Language are you using?\n"
        + "1. C#\n"
        + "2. PHP\n"
        + "3. JS\n"
    + "");
    object codeLang = null;
    string langInserted = Console.ReadLine();
    int chosenLanguage;
    if (int.TryParse(langInserted, out chosenLanguage))
    {
        switch (chosenLanguage)
        {
            case 1: askCSharpProjectType(); break;
            case 2: try { codeLang = createPhpProject(); askPhpFramework(); } catch (Exception e) { }; break;
            case 3: Console.WriteLine("JS"); break;
        }
    }
    return codeLang;
}
void askCSharpProjectType()
{

    Console.WriteLine(""
        + "Which project type do you want?\n"
        + "1. Console\n"
        + "2. API\n"
        + "3. WinForms\n"
    + "");
    string projectType = Console.ReadLine();
    int projectTypeInt;


    Console.WriteLine("What do you want to name your project?");
    string ProjectName = Console.ReadLine();
    if (ProjectName == null) { Console.WriteLine("Fuck you name your project"); return; }




    if (int.TryParse(projectType, out projectTypeInt))
    {
        switch (projectTypeInt)
        {
            case 1: createNewCSharpProject(ProjectName, "console"); break;
            case 2: createNewCSharpProject(ProjectName, "webapp"); break;
            case 3: createNewCSharpProject(ProjectName, "winforms"); break;
        }
    }
}
void createNewCSharpProject(string name, string type)
{

    int cnt = 1;
    string locationStringBuilder = "Where do you want to store this?\n";
    foreach (SettingsProjectLocation pl in projectLocations)
    {
        pl.id = cnt;
        locationStringBuilder += cnt.ToString() + ". " + pl.Name + "\n";
        cnt++;
    }
    Console.WriteLine(locationStringBuilder);

    string projectStorageLocation = Console.ReadLine();
    int projectStorageLocationOut;
    SettingsProjectLocation projectLocation = new SettingsProjectLocation();
    if (projectStorageLocation == null) { Console.WriteLine("Well then good bye"); return; }
    if (int.TryParse(projectStorageLocation, out projectStorageLocationOut))
    {
        projectLocation = projectLocations.Where(f => f.id == projectStorageLocationOut).First();
    }
    else
    {
        Console.WriteLine("Dude, type something real.");
    }
    cSharpProject = new csharp(name, type, projectLocation.Location);
    cSharpProject.Create();

}
void askPhpFramework()
{
    Console.WriteLine(""
        + "Which framwork do you want to use?\n"
        + "1. Laravel\n"
    + "");
    string frameworkChosen = Console.ReadLine();
    int frameworkChosenInt;
    if (int.TryParse(frameworkChosen, out frameworkChosenInt))
    {
        switch (frameworkChosenInt)
        {
            case 1: createFrameworkLaravel(); break;
        }
    }
}
LaravelFramework createFrameworkLaravel()
{
    LaravelFramework lf = null;
    Console.WriteLine(""
        + "Which version do you wish to use?\n"
        + "1. 8\n"
        + "2. 9\n"
        + "3. 10\n"
    + "");
    string version = Console.ReadLine();
    int versionInt;
    if (int.TryParse(version, out versionInt))
    {
        switch (versionInt)
        {
            case 1: lf = new LaravelFramework(10); break;
        }
    }
    return lf;
}
#endregion


#region Program
if (codeLang == null) { return; }

if(codeLang.GetType() == typeof(php))
{
    Console.WriteLine("Awesome, let's start creating your php project");
} 
else if(codeLang.GetType() == typeof(string)) 
{
    Console.WriteLine("Do not know how to create a string project");
} 
else 
{
    Console.WriteLine("Sorry something went wrong. Please try again");
}
#endregion

// Sleep the program
Thread.Sleep(-1);