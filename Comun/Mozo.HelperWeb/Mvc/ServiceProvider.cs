using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Mozo.Comun.Helper.Global;
using System;

namespace Mozo.Comun.Implement.Mvc;

public static class ServiceProviderYo
{
    public static IConfiguration Configuration;
    public static IMemoryCache CacheMemory;


    private static IServiceProvider services;

    /// <summary>
    ///     Provides static access to the framework's services provider
    /// </summary>
    public static IServiceProvider Services
    {
        get => services;
        set
        {
            if (services != null) throw new Exception("Can't set once a value has already been set.");
            services = value;
        }
    }

    /// <summary>
    ///     Provides static access to the current HttpContext
    /// </summary>
    public static HttpContext Current
    {
        get
        {
            var httpContextAccessor = services.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
            return httpContextAccessor?.HttpContext;
        }
    }

    public static string UrlPathFolderDocument()
    {
        //string NoUrlBase = null;
        ////if (cache == null) cache = CacheMemory;

        //if (cache.TryGetValue("HttpConnect_UrlPath", out NoUrlBase))
        //{
        //}
        //else
        //{
        //    //var IsHttps = bool.Parse(Configuration.GetSection("Server").GetSection("IsHttps").Value);
        //    NoUrlBase = string.Concat(Configuration.GetSection("AppSettings").GetSection("FolderRoot").Value.CheckSlashEnd(), Configuration.GetSection("AppSettings").GetSection("FolderResource").Value.CheckSlashEnd());
        //    cache.Set("HttpConnect_UrlPath", NoUrlBase);
        //}

        return string.Concat(Configuration.GetSection("AppSettings").GetSection("FolderRoot").Value.CheckSlashEnd(), Configuration.GetSection("AppSettings").GetSection("FolderDocument").Value.CheckSlashEnd()); ;
    }
    public static string UrlServer(IMemoryCache cache)
    {
        string NoUrlBase = null;
        //if (cache == null) cache = CacheMemory;

        if (cache.TryGetValue("HttpConnect_UrlServer", out NoUrlBase))
        {
        }
        else
        {
            var IsHttps = bool.Parse(Configuration.GetSection("Server").GetSection("IsHttps").Value);
            NoUrlBase = string.Concat(IsHttps ? "https://" : "http://",
                Configuration.GetSection("Server").GetSection("Name").Value.CheckSlashEnd());
            cache.Set("HttpConnect_UrlServer", NoUrlBase);
        }

        return NoUrlBase;
    }
}