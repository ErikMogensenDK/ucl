using System.Data;
using System.Diagnostics.SymbolStore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DITest;

class Program
{
    static void Main(string[] args)
    {
        string testFilePath = "TestFile.txt";
        try
        {
            var myWriter = new StreamWriter(testFilePath);
            for (int i = 0; i < 10; i++)
                myWriter.WriteLine($"Hey, let me write a new line! {i}");
            myWriter.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        try
        {
            var myReader = new StreamReader(testFilePath);
            string? myString = myReader.ReadLine();
            do
            {
                Console.WriteLine(myString);
                myString = myReader.ReadLine();
            }
            while (myString != null);
            myReader.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            Console.WriteLine("Reached finally block");
        }
        //using IHost host = CreateHostBuilder(args).Build();
        //using var scope = host.Services.CreateScope();
        //var service = scope.ServiceProvider;

        //Reader myReader= service.GetRequiredService<Reader>();
        //Writer myWriter = service.GetRequiredService<Writer>();
        //while (true)
        //{
        //myWriter.Write(myReader.Read());
        //}




        //IHostBuilder CreateHostBuilder(string[] args)
        //{
        //return Host.CreateDefaultBuilder(args)
        //.ConfigureServices((_, services) =>
        //{
        //services.AddSingleton<Writer, Writer>();
        //services.AddSingleton<Reader, Reader>();
        //});
        //}
    }
}


