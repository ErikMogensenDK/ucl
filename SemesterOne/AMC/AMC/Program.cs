using System;
using System.Diagnostics;
namespace AMC;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter project name: ");
        string projectName = Console.ReadLine();
        Console.Write("Enter Path ending in '/' : ");
        string pathName = Console.ReadLine();


        // Use the dotnet new console command to create a new project with the given name
        Process.Start("dotnet", $"new console -n {projectName} -o {pathName} --use-program-main").WaitForExit();

        // Use the dotnet command to create test project 
        Process.Start("dotnet", $"new mstest -n {projectName}Tests -o {pathName}").WaitForExit();

        Process.Start("dotnet", $"add {pathName}{projectName}Tests/{projectName}Tests.csproj reference {pathName}{projectName}/{projectName}.csproj").WaitForExit();
    }
}
