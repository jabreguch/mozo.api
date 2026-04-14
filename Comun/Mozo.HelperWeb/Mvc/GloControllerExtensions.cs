using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.IO;

namespace Mozo.Comun.Helper.Global;

public static partial class Glo
{
    //public string RenderRazorViewToString(string viewName, object model)
    //{
    //    ViewData.Model = model;
    //    using (var sw = new StringWriter())
    //    {
    //        var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
    //        var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
    //        viewResult.View.Render(viewContext, sw);
    //        viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
    //        return sw.GetStringBuilder().ToString();
    //    }
    //}
    public static string RenderViewAsync<TModel>(this Controller controller, string viewName, TModel model,
        bool partial = false)
    {
        if (string.IsNullOrEmpty(viewName)) viewName = controller.ControllerContext.ActionDescriptor.ActionName;

        controller.ViewData.Model = model;

        using (var writer = new StringWriter())
        {
            IViewEngine viewEngine =
                controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
            var viewResult = viewEngine.FindView(controller.ControllerContext, viewName, !partial);

            if (viewResult.Success == false) return $"A view with the name {viewName} could not be found";

            var viewContext = new ViewContext(
                controller.ControllerContext,
                viewResult.View,
                controller.ViewData,
                controller.TempData,
                writer,
                new HtmlHelperOptions()
            );

            viewResult.View.RenderAsync(viewContext);

            return writer.GetStringBuilder().ToString();
        }
    }
}