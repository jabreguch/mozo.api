using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Mozo.Filtro.Mvc;

public class MvcFilterResult : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        //IActionResult dd = context.Result;
        await next();
    }
}