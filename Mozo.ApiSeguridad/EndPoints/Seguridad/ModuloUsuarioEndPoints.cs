using Microsoft.AspNetCore.Http.HttpResults;

using Mozo.ApiSeguridad.Helper;
using Mozo.Model.Seguridad;
using Mozo.SeguridadBusiness;

namespace Mozo.Api.Seguridad;

public static partial class ModuloUsuarioEndPoints
{
    public static RouteGroupBuilder MapModuloUsuario(this RouteGroupBuilder g)
    {
        g.MapPost("/", InsertAsync);
        g.MapPut("/", UpdateAsync);
        g.MapPatch("/configuracion", UpdateConfiguracionAsync);
        g.MapDelete("/{comodulousuario:int}", DeleteByIdAsync);
        g.MapGet("/{comodulousuario:int}", SelByIdAsync);
        g.MapGet("/modulos", SelAllByModuloAsync);
        g.MapGet("/modulos/personas", SelByModuloAndPersonaAsync);
        g.MapGet("/personas", SelAllByPersonaAsync);
        return g;
    }

}


public partial class ModuloUsuarioEndPoints
{
    static async Task<Results<Created<int?>, ValidationProblem>> InsertAsync(ModuloUsuarioModel m, IModuloUsuarioBusiness IModuloUsuario, UserClaims user)
    {
        
        m.CoModuloUsuario = await IModuloUsuario.InsertAsync(m);
        return TypedResults.Created($"/{m.CoModuloUsuario}", m.CoModuloUsuario);
    }

    static async Task<Results<Ok<int?>, ValidationProblem>> UpdateAsync(ModuloUsuarioModel m, IModuloUsuarioBusiness IModuloUsuario, UserClaims user)
    {
        
        await IModuloUsuario.UpdateAsync(m);
        return TypedResults.Ok(m.CoModuloUsuario);
    }

    static async Task<Results<Ok<int?>, ValidationProblem>> UpdateConfiguracionAsync(ModuloUsuarioModel m, IModuloUsuarioBusiness IModuloUsuario, UserClaims user)
    {
        
        await IModuloUsuario.UpdateConfiguracionAsync(m);
        return TypedResults.Ok(m.CoModuloUsuario);
    }

    static async Task<Results<NoContent, ValidationProblem>> DeleteByIdAsync(int comodulousuario, IModuloUsuarioBusiness IModuloUsuario, UserClaims user)
    {
        ModuloUsuarioModel m = new() { CoModuloUsuario = comodulousuario };
   
        await IModuloUsuario.DeleteByIdAsync(m);
        return TypedResults.NoContent();
    }
    static async Task<Results<Ok<ModuloUsuarioModel>, NotFound, ValidationProblem>> SelByIdAsync(int comodulousuario, IModuloUsuarioBusiness IModuloUsuario, UserClaims user)
    {
        ModuloUsuarioModel m = new() { CoModuloUsuario = comodulousuario };
        
        ModuloUsuarioModel? i = await IModuloUsuario.SelByIdAsync(m);
        if (i == null)
            return TypedResults.NotFound();
        return TypedResults.Ok(i);
    }

    static async Task<Results<Ok<ModuloUsuarioModel>, NotFound, ValidationProblem>> SelByModuloAndPersonaAsync([AsParameters] ModuloUsuarioFilter f, IModuloUsuarioBusiness IModuloUsuario, UserClaims user)
    {
       
        ModuloUsuarioModel? i = await IModuloUsuario.SelByModuloAndPersonaAsync(new());
        if (i == null)
            return TypedResults.NotFound();
        return TypedResults.Ok(i);
    }

    static async Task<Results<Ok<IEnumerable<ModuloUsuarioModel>>, ValidationProblem>> SelAllByPersonaAsync([AsParameters] ModuloUsuarioFilter f, IModuloUsuarioBusiness IModuloUsuario, UserClaims user)
    {
      
        IEnumerable<ModuloUsuarioModel> r = await IModuloUsuario.SelAllByPersonaAsync(new());
        return TypedResults.Ok(r);
    }

    static async Task<Results<Ok<IEnumerable<ModuloUsuarioModel>>, ValidationProblem>> SelAllByModuloAsync([AsParameters] ModuloUsuarioFilter f, IModuloUsuarioBusiness IModuloUsuario, UserClaims user)
    {   
        IEnumerable<ModuloUsuarioModel> r = await IModuloUsuario.SelAllByModuloAsync(new());
        return TypedResults.Ok(r);
    }

}