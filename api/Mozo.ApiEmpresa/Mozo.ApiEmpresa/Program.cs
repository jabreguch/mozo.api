using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Mozo.Comun.Implement;
using System.IO;

namespace Mozo.ApiEmpresa
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
       Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
               .UseStartup<Startup>();
           }).ConfigureAppConfiguration((hostingContext, config) =>
           {
               config.AddJsonFile(hostingContext.HostingEnvironment.PathJsonFileSharedSettings());
               config.AddEnvironmentVariables();
           });
    }
}
