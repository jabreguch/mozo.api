using Microsoft.AspNetCore.Http.HttpResults;

using Mozo.ApiSeguridad.Helper;
using Mozo.LoginBusiness;
using Mozo.Model.Catalogo;
using Mozo.Model.Seguridad;

namespace Mozo.Api.Login;

public static partial class ModuloEndPoints
{
    public static RouteGroupBuilder MapModulo(this RouteGroupBuilder g)
    {
        g.DisableAntiforgery().RequireAuthorization();
        g.MapGet("/{comodulo:int}", SelByIdAsync);
        g.MapGet("/", SelAllAsync);
        return g;
    }

}
public partial class ModuloEndPoints
{
    static async Task<Results<Ok<ModuloModel>, NotFound, ValidationProblem>> SelByIdAsync(int comodulo, IModuloBusiness IModulo, UserClaims user)
    {
        ModuloModel m = new() { CoModulo = comodulo };        
        ModuloModel? i = await IModulo.SelByIdAsync(m);
        if (i == null)
            return TypedResults.NotFound();
        return TypedResults.Ok(i);
    }

    static async Task<Results<Ok<IEnumerable<ModuloModel>>, ValidationProblem>> SelAllAsync([AsParameters] ModuloFilter f, IModuloBusiness IModulo, UserClaims user)
    {        
        IEnumerable<ModuloModel> r = await IModulo.SelAllAsync();
        return TypedResults.Ok(r);
    }

}