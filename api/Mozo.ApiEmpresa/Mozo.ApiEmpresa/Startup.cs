using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mozo.Comun.Implement.Api;
using System.Globalization;
//using Mozo.CrossCuttingEmpresa;

namespace Mozo.ApiEmpresa
{
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

        // This method gets called by the runtime. Use this method to add services to the container.
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
            Mozo.CrossCuttingEmpresa.BootstrapperContainer.Configuration = Configuration;
            Mozo.CrossCuttingEmpresa.BootstrapperContainer.Register(builder);


            //Mozo.CrossCuttingEmpresaWeb.BootstrapperContainer.Configuration = Configuration;
            //Mozo.CrossCuttingEmpresaWeb.BootstrapperContainer.Register(builder);
        }
    }
}