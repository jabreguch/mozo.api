using Microsoft.AspNetCore.Http.HttpResults;

using Mozo.ApiSeguridad.Helper;
using Mozo.Helper.Global;
using Mozo.HelperWeb.Api;
using Mozo.Model.Seguridad;
using Mozo.SeguridadBusiness;

namespace Mozo.Api.Seguridad;


public static partial class PermisoEndPoints
{
    public static RouteGroupBuilder MapPermiso(this RouteGroupBuilder g)
    {
        g.DisableAntiforgery().RequireAuthorization();
        g.MapPost("/", InsertAsync);
        g.MapPut("/", UpdateAsync);
        g.MapPatch("/state", UpdateStateAsync);
        g.MapGet("/{copermiso:int}", SelByIdAsync);       
        return g;
    }

}
public partial class PermisoEndPoints
{

    static async Task<Results<Created<int?>, ValidationProblem>> InsertAsync(PermisoModel m, IPermisoBusiness IPermiso, UserClaims user)
    {
        
        if (m.NoUsuario.EsNulo())
            return Extension.CreateValidationProblem("500", "Ingrese nombre del Usuario");

        if (m.NoClave.EsNulo())
            return Extension.CreateValidationProblem("500", "Ingrese clave del usuario");

        m.CoPermiso = await IPermiso.InsertAsync(m);

        return TypedResults.Created($"/{m.CoPermiso}", m.CoPermiso);
    }

    static async Task<Results<Ok<int?>, ValidationProblem>> UpdateAsync(PermisoModel m, IPermisoBusiness IPermiso, UserClaims user)
    {
        
        if (m.NoUsuario.EsNulo())
            return Extension.CreateValidationProblem("500", "Ingrese nombre del Usuario");

        if (m.NoClave.EsNulo())
            return Extension.CreateValidationProblem("500", "Ingrese clave del usuario");

        await IPermiso.UpdateAsync(m);
        return TypedResults.Ok(m.CoPermiso);
    }
    static async Task<Results<Ok<int?>, ValidationProblem>> UpdateStateAsync(PermisoModel m, IPermisoBusiness IPermiso, UserClaims user)
    {
        
        await IPermiso.UpdateStateAsync(m);
        return TypedResults.Ok(m.CoPermiso);
    }

   


    static async Task<Results<Ok<PermisoModel>, NotFound, ValidationProblem>> SelByIdAsync(int copermiso, IPermisoBusiness IPermiso, UserClaims user)
    {
        PermisoModel m = new() { CoPermiso = copermiso };   
        PermisoModel? i = await IPermiso.SelByIdAsync(m);
        if (i == null)
            return TypedResults.NotFound();
        return TypedResults.Ok(i);
    }


}