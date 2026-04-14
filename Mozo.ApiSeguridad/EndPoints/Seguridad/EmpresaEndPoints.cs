using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;

using Mozo.ApiSeguridad.Helper;
using Mozo.Model.Seguridad;
using Mozo.SeguridadBusiness;

namespace Mozo.Api.Seguridad;

///<summary>
/// EmpresaEndPointsAdmin
///</summary>
///<history>
/// Create by Jonatan Abregu
///</history>
public static partial class EmpresaEndPoints
{
    ///<summary>
    /// MapEmpresaAdmin
    ///</summary>
    ///<history>
    /// Create by Jonatan Abregu
    ///</history>
    public static RouteGroupBuilder MapEmpresa(this RouteGroupBuilder g)
    {
        g.DisableAntiforgery();//.RequireAuthorization();
        g.MapPost("/", InsertAsync);
        
        g.MapPut("/", UpdateAsync);
        g.MapPatch("/state", UpdateStateAsync);
        g.MapDelete("/{coempresa:int}", DeleteByIdAsync);

        g.MapGet("/{coempresa:int}", SelByIdAsync);
         
        g.MapGet("/", SelAllAsync);
        g.MapGet("/active", SelAllActiveAsync)            
            .CacheOutput(x => x.Expire(TimeSpan.FromHours(24)).Tag("Empresa_SelAllActive"));

        return g;
    }

}


public static partial class EmpresaEndPoints
{
    static async Task<Results<Created<int?>, ValidationProblem>> InsertAsync(EmpresaModel m, IOutputCacheStore outputCacheStore, IConfiguration configuration, IEmpresaBusiness IEmpresa, UserClaims user)
    {       
        m.CoEmpresa = await IEmpresa.InsertAsync(m);
        await outputCacheStore.EvictByTagAsync("Empresa_SelAllActive", default);
        // return TypedResults.Created($"/empresas/{c.CoEmpresaForeignKey}/tipos/{c.CoEmpresa}", c.CoEmpresa);
        return TypedResults.Created($"/{m.CoEmpresa}", m.CoEmpresa);
    }


    static async Task<Results<Ok<int?>, ValidationProblem>> UpdateAsync(EmpresaModel m, IOutputCacheStore outputCacheStore, IConfiguration configuration, IEmpresaBusiness IEmpresa, UserClaims user)
    {
       
        await IEmpresa.UpdateAsync(m);
        await outputCacheStore.EvictByTagAsync("Empresa_SelAllActive", default);
        return TypedResults.Ok(m.CoEmpresa);
    }

    static async Task<Results<Ok<int?>, ValidationProblem>> UpdateStateAsync(EmpresaModel m, IEmpresaBusiness IEmpresa, IOutputCacheStore outputCacheStore, UserClaims user)
    {
     
        await IEmpresa.UpdateStateAsync(m);
        await outputCacheStore.EvictByTagAsync("Empresa_SelAllActive", default);
        return TypedResults.Ok(m.CoEmpresa);
    }

    static async Task<Results<NoContent, ValidationProblem>> DeleteByIdAsync(int coempresa, IEmpresaBusiness IEmpresa, IOutputCacheStore outputCacheStore, UserClaims user)
    {
        EmpresaModel m = new() { CoEmpresa = coempresa };
       
        await IEmpresa.DeleteByIdAsync(m);
        await outputCacheStore.EvictByTagAsync("Empresa_SelAllActive", default);
        return TypedResults.NoContent();
    }


    static async Task<Results<Ok<EmpresaModel>, NotFound, ValidationProblem>> SelByIdAsync(int coempresa, IEmpresaBusiness IEmpresa, UserClaims user)
    {
        EmpresaModel m = new() { CoEmpresa = coempresa };      
        EmpresaModel? i = await IEmpresa.SelByIdAsync(m);
        if (i == null)
            return TypedResults.NotFound();
        return TypedResults.Ok(i);
    }
    // [AsParameters] EmpresaFilterDto filter,
    static async Task<Results<Ok<IEnumerable<EmpresaModel>>, ValidationProblem>> SelAllAsync([AsParameters] EmpresaFilter f, IEmpresaBusiness IEmpresa, UserClaims user)
    {
     
        IEnumerable<EmpresaModel> r = await IEmpresa.SelAllAsync(new());
        return TypedResults.Ok(r);
    }


    static async Task<Results<Ok<IEnumerable<EmpresaModel>>, ValidationProblem>> SelAllActiveAsync(IEmpresaBusiness IEmpresa)
    {
        IEnumerable<EmpresaModel> r = await IEmpresa.SelAllActiveAsync();
        return TypedResults.Ok(r);
    }

}