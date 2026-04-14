using Microsoft.AspNetCore.Http.HttpResults;

using Mozo.ApiSeguridad.Helper;
using Mozo.Model.Seguridad;
using Mozo.SeguridadBusiness;

namespace Mozo.Api.Seguridad;

public static partial class PerfilEndPoints
{
    public static RouteGroupBuilder MapPerfil(this RouteGroupBuilder g)
    {
        g.DisableAntiforgery().RequireAuthorization();
        g.MapPost("/", InsertAsync);
        g.MapPut("/", UpdateAsync);
        g.MapDelete("/{coperfil:int}", DeleteByIdAsync);
        g.MapPatch("/UpdateState", UpdateStateAsync);
        g.MapGet("/default", SelDefaultAsync);
        g.MapGet("/{coperfil:int}", SelByIdAsync);
        g.MapGet("/", SelAllAsync);
        g.MapGet("/active", SelAllActiveAsync);
        return g;
    }

}
public partial class PerfilEndPoints
{
    static async Task<Results<Created<int?>, ValidationProblem>> InsertAsync(PerfilModel m, IPerfilBusiness IPerfil, UserClaims user)
    {
        
        m.CoPerfil = await IPerfil.InsertAsync(m);
        return TypedResults.Created($"/{m.CoPerfil}", m.CoPerfil);
    }
    static async Task<Results<Ok<int?>, ValidationProblem>> UpdateAsync(PerfilModel m, IPerfilBusiness IPerfil, UserClaims user)
    {       
        await IPerfil.UpdateAsync(m);
        return TypedResults.Ok(m.CoPerfil);
    }
    static async Task<Results<NoContent, ValidationProblem>> DeleteByIdAsync(int coperfil, IPerfilBusiness IPerfil, UserClaims user)
    {
        PerfilModel m = new() { CoPerfil = coperfil };        
        await IPerfil.DeleteByIdAsync(m);
        return TypedResults.NoContent();
    }


    static async Task<Results<Ok<int?>, ValidationProblem>> UpdateStateAsync(PerfilModel m, IPerfilBusiness IPerfil, UserClaims user)
    {      
        await IPerfil.UpdateStateAsync(new());
        return TypedResults.Ok(m.CoPerfil);
    }

    static async Task<Results<Ok<IEnumerable<PerfilModel>>, ValidationProblem>> SelAllAsync([AsParameters] PerfilFilter f, IPerfilBusiness IPerfil, UserClaims user)
    {
      
        IEnumerable<PerfilModel> r = await IPerfil.SelAllAsync(new());
        return TypedResults.Ok(r);
    }
    static async Task<Results<Ok<PerfilModel>, NotFound, ValidationProblem>> SelByIdAsync(int coperfil, IPerfilBusiness IPerfil, UserClaims user)
    {
        PerfilModel m = new() { CoPerfil = coperfil };
     
        PerfilModel? i = await IPerfil.SelByIdAsync(m);
        if (i == null)
            return TypedResults.NotFound();
        return TypedResults.Ok(i);
    }

    static async Task<Results<Ok<IEnumerable<PerfilModel>>, ValidationProblem>> SelAllActiveAsync([AsParameters] PerfilFilter f, IPerfilBusiness IPerfil, UserClaims user)
    {
      
        IEnumerable<PerfilModel> r = await IPerfil.SelAllActiveAsync(new());
        return TypedResults.Ok(r);
    }

    static async Task<Results<Ok<PerfilModel>, NotFound, ValidationProblem>> SelDefaultAsync([AsParameters] PerfilFilter f, IPerfilBusiness IPerfil, UserClaims user)
    {
       
        PerfilModel? i = await IPerfil.SelDefaultAsync(new());
        if (i == null)
            return TypedResults.NotFound();
        return TypedResults.Ok(i);
    }


}