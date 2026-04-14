using Microsoft.AspNetCore.Http.HttpResults;

using Mozo.ApiSeguridad.Helper;
using Mozo.LoginBusiness;
using Mozo.Model.Seguridad;

namespace Mozo.Api.Login;

public static partial class MenuEndPoints
{
    public static RouteGroupBuilder MapMenu(this RouteGroupBuilder g)
    {
        g.DisableAntiforgery().RequireAuthorization();
        g.MapGet("/modulos", SelAllModuloAsync);
        return g;
    }
}

public partial class MenuEndPoints
{
    static async Task<Results<Ok<List<ModuloUsuarioModel>>, ValidationProblem>> SelAllModuloAsync(IModuloUsuarioBusiness IModuloUsuario, IPerfilPaginaBusiness IPerfilPagina, UserClaims user)
    {
        return TypedResults.Ok(await MenuLogin.SelAllModuloAsync(new() { CoPersona = user.CoPersona }, IModuloUsuario, IPerfilPagina));
    }
}