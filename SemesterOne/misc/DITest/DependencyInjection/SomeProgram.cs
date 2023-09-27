using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Serilog;
//
//namespace DependencyInjection
//{
//partial class Program
//{
//    static void Main(string[] args)
//    {
//        var builder = new ConfigurationBuilder();
//        BuildConfig(builder);
//
//        Log.Logger = new LoggerConfiguration()
//            .ReadFrom.Configuration(builder.Build())
//            .Enrich.FromLogContext()
//            .WriteTo.Console()
//            .CreateLogger();
//
//        Log.Logger.Information("Starting application!");
//
//
//        // prøv efterfølgende om u bare kan holde dig til ndenstående (og fjerne UseSerilog), hvis du KUN vil lave DI.
//        var host = Host.CreateDefaultBuilder()
//            .ConfigureServices((context, services) =>
//            {
//                services.AddTransient<IGreetingService, GreetingService>();
//                services.AddSingleton<IDataService, DataService>();
//                services.AddTransient<ISomeTestClass, SomeTestClass>();
//            })
//            .UseSerilog()
//            .Build();
//
//        var svc = ActivatorUtilities.CreateInstance<GreetingService>(host.Services);
//        svc.Run();
//
//        //var dataService = ActivatorUtilities.CreateInstance<DataService>(host.Services);
//        var myTestClass = ActivatorUtilities.CreateInstance<SomeTestClass>(host.Services);
//        Console.WriteLine();
//        Console.WriteLine();
//        Console.WriteLine();
//        myTestClass.TestMethod();
//        int someUserId = 1000;
//        myTestClass.UserId = someUserId;
//        Console.WriteLine(myTestClass.UserId);
//    }
//
//    static void BuildConfig(IConfigurationBuilder builder)
//    {
//        builder.SetBasePath(Directory.GetCurrentDirectory())
//            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNET_CORE_ENVIRONMENT") ?? "ProductioN"}.json", optional: true)
//            .AddEnvironmentVariables();
//    }
//
//}
//
//}