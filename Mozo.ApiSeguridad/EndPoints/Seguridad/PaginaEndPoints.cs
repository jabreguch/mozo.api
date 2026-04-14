using Microsoft.AspNetCore.Http.HttpResults;

using Mozo.ApiSeguridad.Helper;
using Mozo.Model.Seguridad;
using Mozo.SeguridadBusiness;

namespace Mozo.Api.Seguridad;

public static partial class PaginaEndPoints
{
    public static RouteGroupBuilder MapPagina(this RouteGroupBuilder g)
    {
        g.DisableAntiforgery().RequireAuthorization();
        g.MapPost("/", InsertAsync);
        g.MapPut("/", UpdateAsync);
        g.MapDelete("/{copagina:int}", DeleteByIdAsync);
        g.MapPatch("/state", UpdateStateAsync);
        g.MapGet("/{copagina:int}", GetByIdAsync);
        return g;
    }

}
public partial class PaginaEndPoints
{
    static async Task<Results<Created<int?>, ValidationProblem>> InsertAsync(PaginaModel m, IPaginaBusiness IPagina, UserClaims user)
    {
        
        m.CoPagina = await IPagina.InsertAsync(m);
        return TypedResults.Created($"/{m.CoPagina}", m.CoPagina);
    }
    static async Task<Results<Ok<int?>, ValidationProblem>> UpdateAsync(PaginaModel m, IPaginaBusiness IPagina, UserClaims user)
    {
     
        await IPagina.UpdateAsync(new());
        return TypedResults.Ok(m.CoPagina);
    }

    static async Task<Results<NoContent, ValidationProblem>> DeleteByIdAsync(int copagina, IPaginaBusiness IPagina, UserClaims user)
    {
        PaginaModel m = new() { CoPagina = copagina };
        
        await IPagina.DeleteByIdAsync(m);
        return TypedResults.NoContent();
    }


    static async Task<Results<Ok<int?>, ValidationProblem>> UpdateStateAsync(PaginaModel m, IPaginaBusiness IPagina, UserClaims user)
    {
       
        await IPagina.UpdateStateAsync(m);
        return TypedResults.Ok(m.CoMenu);
    }

    static async Task<Results<Ok<PaginaModel>, NotFound, ValidationProblem>> GetByIdAsync(int copagina, IPaginaBusiness IPagina, UserClaims user)
    {
        PaginaModel m = new() { CoPagina = copagina };
     
        PaginaModel? i = await IPagina.SelByIdAsync(m);
        if (i == null)
            return TypedResults.NotFound();
        return TypedResults.Ok(i);
    }


}