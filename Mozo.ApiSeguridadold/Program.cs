using Autofac.Extensions.DependencyInjection;
//using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Mozo.Comun.Implement;
using System.IO;


//var builder = WebApplication.CreateBuilder(args);
//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//builder.Host.ConfigureWebHostDefaults(webBuilder =>
//{
//    webBuilder
//        .UseContentRoot(Directory.GetCurrentDirectory())
//        .UseIISIntegration()
//        .UseStartup<Startup>();
//}).ConfigureAppConfiguration((hostingContext, config) =>
//{
//    config.AddJsonFile(hostingContext.HostingEnvironment.PathJsonFileSharedSettings());
//    config.AddEnvironmentVariables();
//});


namespace Mozo.ApiSeguridad
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
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
}