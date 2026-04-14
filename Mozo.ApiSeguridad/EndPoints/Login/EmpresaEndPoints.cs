using Microsoft.AspNetCore.Http.HttpResults;

using Mozo.ApiSeguridad.Helper;
using Mozo.LoginBusiness;
using Mozo.Model.Seguridad;

namespace Mozo.Api.Login;

public static partial class EmpresaEndPoints
{
    /// <summary>
    /// Función que devuelve el grupo de end points de la entidad "tblempresa".
    /// </summary>
    public static RouteGroupBuilder MapEmpresa(this RouteGroupBuilder g)
    {
        g.RequireAuthorization();
        g.MapGet("/{coempresa:int}", SelByIdAsync);
        g.MapGet("/", SelAllAsync);
        return g;
    }

}

public partial class EmpresaEndPoints
{
    static async Task<Results<Ok<EmpresaModel>, NotFound, ValidationProblem>> SelByIdAsync(int coempresa, IEmpresaBusiness IEmpresa, UserClaims user)
    {
        EmpresaModel m = new() { CoEmpresa = coempresa };     
        EmpresaModel? i = await IEmpresa.SelByIdAsync(m);
        if (i == null)
            return TypedResults.NotFound();
        return TypedResults.Ok(i);
    }

    static async Task<Results<Ok<IEnumerable<EmpresaModel>>, ValidationProblem>> SelAllAsync(IEmpresaBusiness IEmpresa)
    {
        IEnumerable<EmpresaModel> r = await IEmpresa.SelAllAsync();
        return TypedResults.Ok(r);
    }

}