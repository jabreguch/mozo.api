using Microsoft.AspNetCore.Http.HttpResults;

using Mozo.ApiSeguridad.Helper;
using Mozo.Model.Seguridad;
using Mozo.SeguridadBusiness;

namespace Mozo.Api.Seguridad;

///<summary>
/// ModuloEndPointsAdmin
///</summary>
///<history>
/// Create by Jonatan Abregu
///</history>
public static partial class ModuloEndPoints
{
    ///<summary>
    /// MapModuloAdmin
    ///</summary>
    ///<history>
    /// Create by Jonatan Abregu
    ///</history>
    public static RouteGroupBuilder MapModulo(this RouteGroupBuilder g)
    {
        g.DisableAntiforgery().RequireAuthorization();
        g.MapPost("/", InsertAsync);
        g.MapPut("/", UpdateAsync);
        g.MapPatch("/state", UpdateStateAsync);
        g.MapDelete("/{comodulo:int}", DeleteByIdAsync);
        g.MapGet("/{comodulo:int}", SelByIdAsync);
        g.MapGet("/", SelAllAsync);
        g.MapGet("/active", SelAllActiveAsync);
        g.MapGet("/active/areas", SelAllActiveAreaAsync);
        return g;
    }

}



public partial class ModuloEndPoints
{
    static async Task<Results<Created<int?>, ValidationProblem>> InsertAsync(ModuloModel m, IModuloBusiness IModulo, UserClaims user)
    {
        
        m.CoModulo = await IModulo.InsertAsync(m);
        return TypedResults.Created($"/{m.CoModulo}", m.CoModulo);
    }

    static async Task<Results<Ok<int?>, ValidationProblem>> UpdateAsync(ModuloModel m, IModuloBusiness IModulo, UserClaims user)
    {
      
        await IModulo.UpdateAsync(m);
        return TypedResults.Ok(m.CoModulo);
    }


    static async Task<Results<NoContent, ValidationProblem>> DeleteByIdAsync(int comodulo, IModuloBusiness IModulo, UserClaims user)
    {
        ModuloModel m = new() { CoModulo = comodulo };       
        await IModulo.DeleteByIdAsync(m);
        return TypedResults.NoContent();
    }


    static async Task<Results<Ok<int?>, ValidationProblem>> UpdateStateAsync(ModuloModel m, IModuloBusiness IModulo, UserClaims user)
    {
     
        await IModulo.UpdateStateAsync(m);
        return TypedResults.Ok(m.CoModulo);
    }


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
      
        IEnumerable<ModuloModel> r = await IModulo.SelAllAsync(new());
        return TypedResults.Ok(r);
    }


    static async Task<Results<Ok<IEnumerable<ModuloModel>>, ValidationProblem>> SelAllActiveAsync(IModuloBusiness IModulo, UserClaims user)
    {
        IEnumerable<ModuloModel> r = await IModulo.SelAllActiveAsync();
        return TypedResults.Ok(r);
    }

    static async Task<Results<Ok<IEnumerable<ModuloModel>>, ValidationProblem>> SelAllActiveAreaAsync(IModuloBusiness IModulo, UserClaims user)
    {
        IEnumerable<ModuloModel> r = await IModulo.SelAllActiveAreaAsync();
        return TypedResults.Ok(r);
    }

}