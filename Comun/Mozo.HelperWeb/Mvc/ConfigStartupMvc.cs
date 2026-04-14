using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement.Http;
using Mozo.Comun.Implement.Verify;
using Mozo.Filtro.Mvc;
using Mozo.Model;
using Polly;
using Polly.Timeout;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace Mozo.Comun.Implement.Mvc;

public class HttpContainer<T>
{
    public T Class { get; set; }
}

public static class ConfigStartupMvc
{
    private static readonly AsyncTimeoutPolicy<HttpResponseMessage> timeout =
        Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(30));

    private static readonly AsyncTimeoutPolicy<HttpResponseMessage> longTimeout =
        Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(60));

    //IServiceCollection services
    //Configuration.GetSection("Server").GetSection("WebApi").GetSection("Empresa").Value.CheckSlashEnd()
    public static void AdminFolderShared(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseStaticFiles();

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(
                configuration.GetSection("AppSettings").GetSection("FolderRoot").Value,
                configuration.GetSection("AppSettings").GetSection("FolderDocument").Value)),
            RequestPath = "/Document"
        });

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(
                configuration.GetSection("AppSettings").GetSection("FolderRoot").Value,
                configuration.GetSection("AppSettings").GetSection("FolderResource").Value, "admin", "assets")),
            RequestPath = "/assets"
        });
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(
                configuration.GetSection("AppSettings").GetSection("FolderRoot").Value,
                configuration.GetSection("AppSettings").GetSection("FolderResource").Value, "admin", "assetscustom",
                "js")),
            RequestPath = "/js"
        });

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(
                configuration.GetSection("AppSettings").GetSection("FolderRoot").Value,
                configuration.GetSection("AppSettings").GetSection("FolderResource").Value, "admin", "assetscustom",
                "image")),
            RequestPath = "/image"
        });
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(
                configuration.GetSection("AppSettings").GetSection("FolderRoot").Value,
                configuration.GetSection("AppSettings").GetSection("FolderResource").Value, "admin", "assetscustom",
                "css")),
            RequestPath = "/css"
        });
    }

    public static void MktgFolderTransporteLogistico(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(
                configuration.GetSection("AppSettings").GetSection("FolderRoot").Value,
                configuration.GetSection("AppSettings").GetSection("FolderResource").Value, "mktg", "trasnportelog",
                "assets")),
            RequestPath = "/assetstrasnportelog"
        });

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(
                configuration.GetSection("AppSettings").GetSection("FolderRoot").Value,
                configuration.GetSection("AppSettings").GetSection("FolderResource").Value, "mktg", "trasnportelog",
                "assetscustom")),
            RequestPath = "/assetscustomtrasnportelog"
        });
    }

    public static void MktgFolderTI(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(
                configuration.GetSection("AppSettings").GetSection("FolderRoot").Value,
                configuration.GetSection("AppSettings").GetSection("FolderResource").Value, "mktg", "ti", "assets")),
            RequestPath = "/assetsti"
        });

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(
                configuration.GetSection("AppSettings").GetSection("FolderRoot").Value,
                configuration.GetSection("AppSettings").GetSection("FolderResource").Value, "mktg", "ti",
                "assetscustom")),
            RequestPath = "/assetscustomti"
        });
    }




    public static void MktgSoftwareDeveloper(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(
                configuration.GetSection("AppSettings").GetSection("FolderRoot").Value,
                configuration.GetSection("AppSettings").GetSection("FolderResource").Value, "mktg", "softwaredev", "assets")),
            RequestPath = "/assetssoftwaredev"
        });

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(
                configuration.GetSection("AppSettings").GetSection("FolderRoot").Value,
                configuration.GetSection("AppSettings").GetSection("FolderResource").Value, "mktg", "softwaredev",
                "assetscustom")),
            RequestPath = "/assetscustomsoftwaredev"
        });
    }

    public static void MktgFolderAbogado(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(
                configuration.GetSection("AppSettings").GetSection("FolderRoot").Value,
                configuration.GetSection("AppSettings").GetSection("FolderResource").Value, "mktg", "abogado",
                "assets")),
            RequestPath = "/assetsabogado"
        });

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(
                configuration.GetSection("AppSettings").GetSection("FolderRoot").Value,
                configuration.GetSection("AppSettings").GetSection("FolderResource").Value, "mktg", "abogado",
                "assetscustom")),
            RequestPath = "/assetscustomabogado"
        });
    }

    public static void MktgFolderMarketFashion(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(
                configuration.GetSection("AppSettings").GetSection("FolderRoot").Value,
                configuration.GetSection("AppSettings").GetSection("FolderResource").Value, "mktg", "marketfashion",
                "assets")),
            RequestPath = "/assetsmarketfashion"
        });

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(
                configuration.GetSection("AppSettings").GetSection("FolderRoot").Value,
                configuration.GetSection("AppSettings").GetSection("FolderResource").Value, "mktg", "marketfashion",
                "assetscustom")),
            RequestPath = "/assetscustommarketfashion"
        });
    }

    public static void AddMyRclConfigure(this IApplicationBuilder app, IWebHostEnvironment env,
        IConfiguration configuration)
    {
        app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseExceptionHandler(err => err.UseCustomErrors(env));

        app.Use(async (context, next) =>
        {
            await next();

            if (context.Response.StatusCode == 404)
            {
                if (HttpClientUtil.IsAjaxRequest(context))
                {
                    context.Response.ContentType = "application/json";
                    var http = Glo.StatusHttp(404);
                    ProblemDetails Problem = new()
                    {
                        Status = int.Parse(http[0]),
                        Title = http[1],
                        Instance = context.Request.Path,
                        Type = http[2],
                        Detail = "Page not found"
                    };

                    await context.Response.WriteAsync(Problem.Serializa(), Encoding.UTF8);
                }
                else
                {
                    if (context.Response.StatusCode == 404)
                    {
                        context.Request.Path = "/General/Page404";
                        await next();
                    }
                }
            }
        });

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseCookiePolicy();


        app.UseAuthentication();
        app.UseAuthorization();

        app.UseVerify();

        /*Folder Shared all Aplication*/
        app.AdminFolderShared(configuration);

        var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
        Url2.Configure(httpContextAccessor);
        app.UseSession();

        ServiceProviderYo.Services = app.ApplicationServices;
    }

    public static void AddHttpClientConfig(this IHttpClientBuilder httpClient)
    {
        httpClient.ConfigurePrimaryHttpMessageHandler(messageHandler =>
            {
                var handler = new HttpClientHandler();

                if (handler.SupportsAutomaticDecompression)
                    handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                return handler;
            }).AddHttpMessageHandler<TokenHandler>()
            .AddPolicyHandler(request => request.Method == HttpMethod.Get ? timeout : longTimeout);
    }

    public static ConfigurationModel BasicConfigureService(this IServiceCollection services,
        IConfiguration configuration, bool filter = true)
    {
        var config = new ConfigurationModel();
        configuration.Bind(config); //services.AddSingleton(config);

        services.Configure<ConfigurationModel>(options => configuration.Bind(options));
        services.AddCors();
        services.AddVerify();
        services.AddDistributedMemoryCache();

        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromHours(2);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        if (filter)
            services.AddControllers(
                options =>
                {
                    options.Filters.Add(typeof(MvcFilterAction));
                    options.Filters.Add(typeof(MvcFilterResult));
                }
            ).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                //options.JsonSerializerOptions.IgnoreNullValues = true;
            }).AddRazorRuntimeCompilation(); // Para editar y continuar
        else
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                //options.JsonSerializerOptions.IgnoreNullValues = true;
            });


        services.AddMvc()
            .AddRazorOptions(options =>
            {
                options.ViewLocationFormats.Add("/Views/Components/Layout/PageTitle/{0}.cshtml");
            });

        services.AddRazorPages();

        services.AddHttpContextAccessor();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        return config;
    }


    public static void AddMyRclServices(this IServiceCollection services, IConfiguration configuration,
        string[] httpTypeClientNames)
    {
        var config = services.BasicConfigureService(configuration);

        services.AddTransient<AuthenticationDelegatingHandler>();
        //services.AddTransient<HttpMessageHandler>(p => p.GetRequiredService<AuthenticationDelegatingHandler>());
        services.AddTransient<TokenHandler>();

        var urlServer = ConfigurationModel.GetUrlServer(config); // .GetUrlWebApi("Seguridad",

        services.AddHttpClient<IHttpClientToken, HttpClientToken>(client =>
        {
            client.BaseAddress = new Uri(string.Concat(urlServer, config.Server.Api.Seguridad.CheckSlashEnd()));
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }).ConfigurePrimaryHttpMessageHandler(messageHandler =>
        {
            var handler = new HttpClientHandler();

            if (handler.SupportsAutomaticDecompression)
                handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            return handler;
        }).AddPolicyHandler(request => request.Method == HttpMethod.Get ? timeout : longTimeout);


        foreach (var httpTypeClientName in httpTypeClientNames)
            switch (httpTypeClientName)
            {
                case "Seguridad":
                    services.AddHttpClient<IHttpClientSeguridad, HttpClientSeguridad>(x =>
                    {
                        x.BaseAddress =
                            new Uri(string.Concat(urlServer, config.Server.Api.Seguridad.CheckSlashEnd()));
                    }).AddHttpClientConfig();
                    break;
                case "Maestro":
                    services.AddHttpClient<IHttpClientMaestro, HttpClientMaestro>(x =>
                    {
                        x.BaseAddress =
                            new Uri(string.Concat(urlServer, config.Server.Api.Maestro.CheckSlashEnd()));
                    }).AddHttpClientConfig();
                    break;
                case "Contabilidad":
                    services.AddHttpClient<IHttpClientContabilidad, HttpClientContabilidad>(x =>
                    {
                        x.BaseAddress =
                            new Uri(string.Concat(urlServer, config.Server.Api.Contabilidad.CheckSlashEnd()));
                    }).AddHttpClientConfig();
                    break;
                case "Empresa":
                    services.AddHttpClient<IHttpClientEmpresa, HttpClientEmpresa>(x =>
                    {
                        x.BaseAddress =
                            new Uri(string.Concat(urlServer, config.Server.Api.Empresa.CheckSlashEnd()));
                    }).AddHttpClientConfig();
                    break;
                case "Matricula":
                    services.AddHttpClient<IHttpClientMatricula, HttpClientMatricula>(x =>
                    {
                        x.BaseAddress =
                            new Uri(string.Concat(urlServer, config.Server.Api.Matricula.CheckSlashEnd()));
                    }).AddHttpClientConfig();
                    break;
                case "Condominio":
                    services.AddHttpClient<IHttpClientCondominio, HttpClientCondominio>(x =>
                    {
                        x.BaseAddress =
                            new Uri(string.Concat(urlServer, config.Server.Api.Condominio.CheckSlashEnd()));
                    }).AddHttpClientConfig();
                    break;
                case "Facturacion":
                    services.AddHttpClient<IHttpClientFacturacion, HttpClientFacturacion>(x =>
                    {
                        x.BaseAddress =
                            new Uri(string.Concat(urlServer, config.Server.Api.Facturacion.CheckSlashEnd()));
                    }).AddHttpClientConfig();
                    break;
                case "Inventario":
                    services.AddHttpClient<IHttpClientInventario, HttpClientInventario>(x =>
                    {
                        x.BaseAddress =
                            new Uri(string.Concat(urlServer, config.Server.Api.Inventario.CheckSlashEnd()));
                    }).AddHttpClientConfig();
                    break;
                case "Urbano":
                    services.AddHttpClient<IHttpClientUrbano, HttpClientUrbano>(x =>
                    {
                        x.BaseAddress = new Uri(string.Concat(urlServer, config.Server.Api.Urbano.CheckSlashEnd()));
                    }).AddHttpClientConfig();
                    break;
                case "Soporte":
                    services.AddHttpClient<IHttpClientSoporte, HttpClientSoporte>(x =>
                    {
                        x.BaseAddress =
                            new Uri(string.Concat(urlServer, config.Server.Api.Soporte.CheckSlashEnd()));
                    }).AddHttpClientConfig();
                    break;
            }

        services.AddTransient<IEmailSender, EmailSender>();
        services.Configure<EmailSenderModel>(configuration.GetSection("EmailSenderOptions"));

    }
}