using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement.Http;
using Mozo.Comun.Implement.Log;

namespace Mozo.Filtro.Mvc;

public class MvcFilterException : IExceptionFilter // IAsyncExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var ex = context.Exception;
        var problem = new ProblemDetails();

        var http = Glo.StatusHttp(context.HttpContext.Response.StatusCode);
        problem.Status = int.Parse(http[0]);
        problem.Title = ex.Message.NoNulo() ? ex.Message : http[1];
        problem.Instance = context.HttpContext.Request.Path;
        problem.Type = ex.HelpLink.NoNulo() ? ex.HelpLink : http[2];
        problem.Detail = ex.ToString();
        ELog.Save(problem);
        problem.Detail = ex.Message;
        //Serialize the problem details object to the Response as JSON (using System.Text.Json)

        //Stream stream = httpContext.Response.Body;
        //await JsonSerializer.SerializeAsync(stream, problem);

        //app.Use(async (context, next) =>
        //{
        //    await next();
        //    if (context.Response.StatusCode == 404)
        //    {
        //        context.Request.Path = "/General/Page404";
        //        await next();
        //    }
        //});

        if (HttpClientUtil.IsAjaxRequest(context.HttpContext))
            context.Result = new JsonResult(problem);
        else
            //RedirectToActionResult()
            //context.Result = new RedirectResult("/General/Page404");
            context.Result = new RedirectToActionResult("Page404", "General", new { area = "" });


        //return Task.CompletedTask;
    }

    //public void OnException(ExceptionContext context)
    //{
    //    throw new NotImplementedException();
    //}
}