using Microsoft.AspNetCore.Http.HttpResults;

using Mozo.ApiSeguridad.Helper;
using Mozo.LoginBusiness;
using Mozo.Model.Seguridad;

namespace Mozo.Api.Login;


public static partial class RedirectEndPoints
{
    public static RouteGroupBuilder MapRedirect(this RouteGroupBuilder g)
    {
        g.DisableAntiforgery().RequireAuthorization();
        g.MapPost("/", InsertAsync);
        g.MapDelete("/{coredirect:int}", DeleteByIdAsync);
        g.MapGet("/{coredirect:int}", SelByIdAsync);
        return g;
    }

}
public partial class RedirectEndPoints
{
    static async Task<Results<Created<int?>, ValidationProblem>> InsertAsync(RedirectModel m, IRedirectBusiness IRedirect, UserClaims user)
    {        
        m.CoRedirect = await IRedirect.InsertAsync(m);
        return TypedResults.Created($"/{m.CoRedirect}", m.CoRedirect);
    }

    static async Task<Results<NoContent, ValidationProblem>> DeleteByIdAsync(int coredirect, IRedirectBusiness IRedirect, UserClaims user)
    {
        RedirectModel m = new() { CoRedirect = coredirect };        
        await IRedirect.DeleteByIdAsync(m);
        return TypedResults.NoContent();
    }

    static async Task<Results<Ok<RedirectModel>, NotFound, ValidationProblem>> SelByIdAsync(int coredirect, IRedirectBusiness IRedirect, UserClaims user)
    {
        RedirectModel m = new() { CoRedirect = coredirect };
        
        RedirectModel? i = await IRedirect.SelByIdAsync(m);
        if (i == null)
            return TypedResults.NotFound();
        return TypedResults.Ok(i);
    }


}