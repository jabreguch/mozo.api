using Microsoft.AspNetCore.Http.HttpResults;

using Mozo.ApiSeguridad.Helper;
using Mozo.LoginBusiness;
using Mozo.Model.Seguridad;

namespace Mozo.Api.Login;

public static partial class ModuloUsuarioEndPoints
{
    public static RouteGroupBuilder MapModuloUsuario(this RouteGroupBuilder g)
    {
        g.DisableAntiforgery().RequireAuthorization();
        g.MapGet("/modulos", SelByModuloAsync);
        g.MapGet("/personas", SelAllByPersonaAsync);
        return g;
    }
}

public partial class ModuloUsuarioEndPoints
{

    static async Task<Results<Ok<ModuloUsuarioModel>, NotFound, ValidationProblem>> SelByModuloAsync([AsParameters] ModuloUsuarioFilter f, IModuloUsuarioBusiness IModuloUsuario, UserClaims user)
    {       
        ModuloUsuarioModel? i = await IModuloUsuario.SelByModuloAsync(new());
        if (i == null)
            return TypedResults.NotFound();
        return TypedResults.Ok(i);
    }

    static async Task<Results<Ok<IEnumerable<ModuloUsuarioModel>>, ValidationProblem>> SelAllByPersonaAsync([AsParameters] ModuloUsuarioFilter f, IModuloUsuarioBusiness IModuloUsuario, UserClaims user)
    {       
        IEnumerable<ModuloUsuarioModel> r = await IModuloUsuario.SelAllByPersonaAsync(new());
        return TypedResults.Ok(r);
    }


}