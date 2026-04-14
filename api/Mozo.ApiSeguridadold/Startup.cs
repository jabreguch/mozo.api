using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mozo.Comun.Implement.Api;
using Mozo.CrossCuttingSeguridad;
using System.Globalization;

namespace Mozo.ApiSeguridad;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        Accessor.Configuration = configuration;

        var cultureInfo = new CultureInfo("es-PE");
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMyConfigureServices();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.AddMyConfigure(env);
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        BootstrapperContainer.Configuration = Configuration;
        BootstrapperContainer.Register(builder);

        CrossCuttingLogin.BootstrapperContainer.Configuration = Configuration;
        CrossCuttingLogin.BootstrapperContainer.Register(builder);

        //CrossCuttingSeguridadWeb.BootstrapperContainer.Configuration = Configuration;
        //CrossCuttingSeguridadWeb.BootstrapperContainer.Register(builder);
    }
}