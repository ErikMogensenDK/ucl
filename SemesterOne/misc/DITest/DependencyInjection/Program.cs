using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DependencyInjection;

// DI, serilog, our settings.

partial class Program
{
    static void Main(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();
        // prøv efterfølgende om u bare kan holde dig til ndenstående (og fjerne UseSerilog), hvis du KUN vil lave DI.

        //var dataService = ActivatorUtilities.CreateInstance<DataService>(host.Services);
        var scope = host.Services.CreateScope();
        var myTestClass = ActivatorUtilities.CreateInstance<SomeTestClass>(host.Services);
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        myTestClass.TestMethod();
        int someUserId = 1000;
        myTestClass.UserId = someUserId;
        Console.WriteLine(myTestClass.UserId);
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddSingleton<IDataService, DataService>();
                services.AddTransient<ISomeTestClass, SomeTestClass>();
            });
    }
}
