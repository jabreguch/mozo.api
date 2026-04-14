using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Web;

namespace Mozo.Comun.Helper.Global;

public static class Url2
{
    private static IHttpContextAccessor HttpContextAccessor;

    public static void Configure(IHttpContextAccessor httpContextAccessor)
    {
        HttpContextAccessor = httpContextAccessor;
    }


    public static string Get(string[] FolderList)
    {
        string NoFolders = null;
        foreach (var NoFolder in FolderList) NoFolders = NoFolders + NoFolder + "/";

        if (NoFolders.NoNulo()) NoFolders = NoFolders.Substring(0, NoFolders.Length - 1);


        var req = HttpContextAccessor.HttpContext.Request;
        var absoluteUri = string.Concat(
            req.Scheme,
            "://",
            req.Host.ToUriComponent(),
            req.PathBase.ToUriComponent(),
            "/", NoFolders
        );


        //HttpRequest req = HttpContextAccessor.HttpContext.Request;
        //string absoluteUri = string.Concat(
        //            //req.Scheme,
        //            //"://",
        //            //req.Host.ToUriComponent(),
        //            req.PathBase.ToUriComponent(),
        //            "/", NoFolders
        //           );


        return absoluteUri;
    }


    public static string Get(
        //this IUrlHelper url,
        string NoAccion,
        string NoControlador,
        object Parametros)
    {
        //HttpRequest req = HttpContextAccessor.HttpContext.Request;
        //string absoluteUri = string.Concat(
        //            req.Scheme,
        //            "://",
        //            req.Host.ToUriComponent(),
        //            req.PathBase.ToUriComponent(),
        //            "/", NoArea,
        //            "/", NoControlador,
        //            "/", NoAccion
        //           );


        var req = HttpContextAccessor.HttpContext.Request;
        var absoluteUri = string.Concat(
            req.Scheme,
            "://",
            req.Host.ToUriComponent(),
            req.PathBase.ToUriComponent(),
            "/", NoControlador,
            "/", NoAccion
        );


        var properties = from p in Parametros.GetType().GetProperties()
                         where p.GetValue(Parametros, null) != null
                         select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(Parametros, null).ToString());

        var queryString = string.Join("&", properties.ToArray());

        return string.Concat(absoluteUri, "?", queryString);
    }


    public static string Get(
        string NoUrl
    )
    {
        //string scheme = HttpContextAccessor.HttpContext.Request.Scheme;
        var req = HttpContextAccessor.HttpContext.Request;
        var absoluteUri = string.Concat(
            req.Scheme,
            "://",
            req.Host.ToUriComponent(),
            req.PathBase.ToUriComponent(),
            "/", NoUrl
        );
        // req.Path.ToUriComponent()
        //req.QueryString.ToUriComponent()
        //return url.Action(actionName, controllerName, routeValues, scheme);
        return absoluteUri;
    }

    public static string Get(
        //this IUrlHelper url,
        string NoAccion,
        string NoControlador)
    {
        //string scheme = HttpContextAccessor.HttpContext.Request.Scheme;
        var req = HttpContextAccessor.HttpContext.Request;
        var absoluteUri = string.Concat(
            req.Scheme,
            "://",
            req.Host.ToUriComponent(),
            req.PathBase.ToUriComponent(),
            "/", NoControlador,
            "/", NoAccion
        );
        // req.Path.ToUriComponent()
        //req.QueryString.ToUriComponent()
        //return url.Action(actionName, controllerName, routeValues, scheme);
        return absoluteUri;
    }

    public static string Get(
        //this IUrlHelper url,
        string NoAccion,
        string NoControlador,
        string NoArea)
    {
        //string scheme = HttpContextAccessor.HttpContext.Request.Scheme;
        var req = HttpContextAccessor.HttpContext.Request;
        var absoluteUri = string.Concat(
            req.Scheme,
            "://",
            req.Host.ToUriComponent(),
            req.PathBase.ToUriComponent(),
            "/", NoArea,
            "/", NoControlador,
            "/", NoAccion
        );
        // req.Path.ToUriComponent()
        //req.QueryString.ToUriComponent()
        //return url.Action(actionName, controllerName, routeValues, scheme);
        return absoluteUri;
    }


    public static string Get(
        //this IUrlHelper url,
        string NoAccion,
        string NoControlador,
        string NoArea,
        object Parametros)
    {
        //string scheme = HttpContextAccessor.HttpContext.Request.Scheme;
        var req = HttpContextAccessor.HttpContext.Request;
        var absoluteUri = string.Concat(
            req.Scheme,
            "://",
            req.Host.ToUriComponent(),
            req.PathBase.ToUriComponent(),
            "/", NoArea,
            "/", NoControlador,
            "/", NoAccion
        );


        // req.Path.ToUriComponent()
        //req.QueryString.ToUriComponent()
        //return url.Action(actionName, controllerName, routeValues, scheme);

        //string output = JsonConvert.SerializeObject(Parametros);
        //dynamic jsonObj = JsonConvert.DeserializeObject(output);

        var properties = from p in Parametros.GetType().GetProperties()
                         where p.GetValue(Parametros, null) != null
                         select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(Parametros, null).ToString());

        var queryString = string.Join("&", properties.ToArray());

        //    +"&d=" + DateTime.Now.ToString("yyyymmddhhmmsimm")

        return string.Concat(absoluteUri, "?", queryString, "&d=", DateTime.Now.ToString("yyyymmddhhmmsimm"));
    }
}