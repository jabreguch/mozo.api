using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using Mozo.Comun.Helper.Enu;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement.Http;
using Mozo.Comun.Implement.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Mozo.Filtro.Mvc;

public class MvcFilterAction : IAsyncActionFilter //, IAuthorizationFilter IAsyncActionFilter
{
    //public void OnActionExecuting(ActionExecutingContext context)
    //{
    //    //if (!context.ModelState.IsValid)
    //    //{
    //    //    context.Result = new BadRequestObjectResult(new ApiBadRequestResponse(context.ModelState));
    //    //    return;
    //    //}

    //    context.HttpContext.Session.Remove(EnuCommon.GLOBAL_NAME_SESSION_MESSAGE);


    //    //return;
    //    RouteValueDictionary rv = context.RouteData.Values;
    //    //object NoArea, NoControlador, NoAccion;
    //    rv.TryGetValue("area", out object NoArea);
    //    rv.TryGetValue("controller", out object NoControlador);
    //    rv.TryGetValue("action", out object NoAccion);

    //    bool FlAllowAnonymous = context.ActionDescriptor.EndpointMetadata.Any(em => em.GetType() == typeof(AllowAnonymousAttribute)); //< -- Here it is
    //    if (FlAllowAnonymous) return;


    //    //if (PaginasExceptuadas(NoArea.Text(), NoControlador.Text(), NoAccion.Text())) { return; }


    //    PersonaModel GlobalSession = Yo.GetCredential();


    //    if (GlobalSession != null)
    //    {


    //        //End Verify Authotization

    //        if (Yo.FindPage(NoArea.Text(), NoControlador.Text(), NoAccion.Text()) == false)
    //        {
    //            return;
    //            if (IsAjaxRequest(context))
    //            {
    //                var data = new
    //                {
    //                    success = false,
    //                    error = true,
    //                    message = new ProblemDetails() { Detail = "No tiene permiso para acceder a esta pagina", Status = 400 }.Serializa()
    //                };
    //                context.Result = new JsonResult(data);

    //            }
    //            else
    //            {
    //                context.Result = new RedirectToRouteResult(new RouteValueDictionary{
    //                                { "controller", "Account" },
    //                                { "action", "NoAcceso" },
    //                                { "area","" }
    //                });

    //            }
    //        }
    //    }
    //    else
    //    {
    //        StringValues Header;
    //        context.HttpContext.Request.Headers.TryGetValue("Authorization", out Header);

    //        if (Header.Count == 0)
    //        {

    //            context.Result = new RedirectToRouteResult(new RouteValueDictionary{
    //            { "controller", "Account" },
    //            { "action", "Logout" },
    //            { "area","" }
    //        });
    //        }


    //    }


    //}

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        //if (!context.ModelState.IsValid)
        //{
        //    context.Result = new BadRequestObjectResult(new ApiBadRequestResponse(context.ModelState));
        //    return;
        //}

        EnuCommon.GLOBAL_IS_AJAX = HttpClientUtil.IsAjaxRequest(context.HttpContext);


        //context.HttpContext.Session.Remove(EnuCommon.GLOBAL_NAME_SESSION_MESSAGE);


        //return;
        var rv = context.RouteData.Values;
        //object NoArea, NoControlador, NoAccion;
        rv.TryGetValue("area", out var NoArea);
        rv.TryGetValue("controller", out var NoControlador);
        rv.TryGetValue("action", out var NoAccion);

        var FlAllowAnonymous =
            context.ActionDescriptor.EndpointMetadata.Any(em =>
                em.GetType() == typeof(AllowAnonymousAttribute)); //< -- Here it is
        if (FlAllowAnonymous)
        {
            await next(); //need to pass the execution to next
            return;
        }

        ;


        //if (PaginasExceptuadas(NoArea.Text(), NoControlador.Text(), NoAccion.Text())) { return; }


        var Credential = Yo.GetCredential();


        if (Credential != null)
        {
            //End Verify Authotization

            if (Yo.FindPage(NoArea.Text(), NoControlador.Text(), NoAccion.Text()) == false)
            {
                await next();
                return;
                if (HttpClientUtil.IsAjaxRequest(context.HttpContext))
                {
                    var data = new
                    {
                        success = false,
                        error = true,
                        message = new ProblemDetails
                        { Detail = "No tiene permiso para acceder a esta pagina", Status = 400 }.Serializa()
                    };
                    context.Result = new JsonResult(data);
                }
                else
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary
                    {
                        { "controller", "Account" },
                        { "action", "NoAcceso" },
                        { "area", "" }
                    });
                }
            }
        }
        else
        {
            StringValues Header;
            context.HttpContext.Request.Headers.TryGetValue("Authorization", out Header);

            if (Header.Count == 0)
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller", "Account" },
                    { "action", "Logout" },
                    { "area", "" }
                });
        }


        await next(); //need to pass the execution to next
    }
}