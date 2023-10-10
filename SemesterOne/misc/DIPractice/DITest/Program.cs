using System.Data;
using System.Diagnostics.SymbolStore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DITest;

class Program
{
    static void Main(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();
        using var scope = host.Services.CreateScope();
        var service = scope.ServiceProvider;

        var myWriter = service.GetRequiredService<IWriter>();
        myWriter.Write();


        IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddTransient<IWriter, WriterTwo>();
                } );
        }
    }
}


