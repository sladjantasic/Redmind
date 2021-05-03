using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")] // Needed for Moq
[assembly: InternalsVisibleTo("RedmindATM.Tests")]         // Needed for tests
namespace RedmindATM
{
    class Program
    {
        static void Main(string[] args)
        {

            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            serviceProvider.GetService<Assignment>().Run();

        }

        private static void ConfigureServices(ServiceCollection serviceCollection)
        {
            IConfiguration configuration = GetConfig();

            serviceCollection.AddSingleton(configuration);
            serviceCollection.AddTransient<Assignment>();
            serviceCollection.AddScoped<IATMHandler, ATMHandler>();
            serviceCollection.AddScoped<IBillsForATM, BillsFromAppsettings>();
            serviceCollection.AddScoped<IATM, ATM>();
        }

        private static IConfiguration GetConfig()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

        }

    }
}
