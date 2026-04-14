using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Mozo.ApiSeguridad.Helper;
using Mozo.Helper.Global;
using Mozo.HelperWeb.Api;
using Mozo.HelperWeb.Token;
using Mozo.LoginBusiness;
using Mozo.Model.Catalogo;
using Mozo.Model.Seguridad;
using Mozo.Model.Seguridad.Auth;

using System.Security.Claims;

using static System.Net.WebRequestMethods;

namespace Mozo.Api.Login;

public static partial class PermisoEndPoints
{
    public static RouteGroupBuilder MapPermiso(this RouteGroupBuilder g)
    {
        g.DisableAntiforgery().RequireAuthorization();
        g.MapPost("/login", LoginAsync).AllowAnonymous().DocGetByUser();
        g.MapGet("/login-check", LoginCheck);
        g.MapPost("/login-renew", LoginRenew).AllowAnonymous();
        g.MapPatch("/language", UpdateLanguageAsync).UpdateLanguageDoc();
        return g;
    }
}
public static partial class PermisoEndPoints
{
    static async Task<Results<Ok<GlobalCredencialModel>, NotFound, ValidationProblem>> LoginAsync([FromBody] PermisoFilterDto f, IConfiguration configuration, IPermisoBusiness IPermiso, IIngresoBusiness IIngreso)
    {
        GlobalCredencialModel globalCredencial = new();

        string? noTokenRefresh = null;

        PermisoModel? permiso = await IPermiso.SelByUserAsync(f);

        if (permiso == null)
            return Extension.CreateValidationProblem("500", "Usuario no encontrado.");

        IngresoModel? ingreso = null;

        noTokenRefresh = UtilityJwt.GenerateRefreshToken();

        ingreso = new()
        {
            CoEmpresa = permiso.CoEmpresa,
            CoPersona = permiso.CoPersona,
            CoPermiso = permiso.CoPermiso,
            NoRefreshToken = noTokenRefresh,
            NoIp = f.NoIp,
        };
        ingreso.CoIngreso = await IIngreso.InsertAsync(ingreso);
        noTokenRefresh = string.Concat(ingreso.CoEmpresa, "-", noTokenRefresh, "-", ingreso.CoIngreso);

        CredencialModel credential = new()
        {
            CoEmpresa = permiso.CoEmpresa,
            CoPersona = permiso.CoPersona,
            CoPermiso = permiso.CoPermiso,
            NoUsuario = permiso.NoUsuario,
            NoNombreCompleto = permiso.NoNombreCompleto,
            CoIngreso = ingreso.CoIngreso
        };

        string token = UtilityJwt.GenerateToken(credential, configuration);
        //credential.FeExpiration = r.Item2;

        globalCredencial.Credencial = credential;
        globalCredencial.NoToken = token;
        globalCredencial.NoTokenRefresh = noTokenRefresh;

        return TypedResults.Ok(globalCredencial);
    }

    static async Task<Results<Ok<GlobalCredencialModel>, NotFound, ValidationProblem>> LoginRenew([FromBody] IngresoFilterDto f, IConfiguration configuration, IPermisoBusiness IPermiso, IIngresoBusiness IIngreso)
    {
        if (f == null || f.NoRefreshToken == null)
            return Extension.CreateValidationProblem("401", "Falta el token refresh.");

        string[] tokenRefresh = f.NoRefreshToken.Split("-");

        if (tokenRefresh.Length != 3)
            return Extension.CreateValidationProblem("401", "Falta el token refresh.");

        int? coEmpresa = int.Parse(tokenRefresh[0]);
        int? coIngreso = int.Parse(tokenRefresh[2]);

        IngresoModel? ingreso = await IIngreso.SelByIdAsync(new() { CoEmpresa = coEmpresa, CoIngreso = coIngreso });
        if (ingreso == null)
            return Extension.CreateValidationProblem("401", "Token refresh no valido.");

        await IIngreso.UpdateCloseTokenAsync(new() { CoEmpresa = ingreso.CoEmpresa, CoPermiso = ingreso.CoPermiso, NoRefreshTokenPrevious = f.NoRefreshToken });

        GlobalCredencialModel globalCredencial = new();

        string? noTokenRefresh = null;

        PermisoModel? permiso = await IPermiso.SelByIdAsync(new() { CoEmpresa = ingreso.CoEmpresa, CoPermiso = ingreso.CoPermiso });

        if (permiso == null)
            return Extension.CreateValidationProblem("500", "Usuario no encontrado.");


              

        noTokenRefresh = UtilityJwt.GenerateRefreshToken();
        noTokenRefresh = string.Concat(ingreso.CoEmpresa, "-", noTokenRefresh, "-", ingreso.CoIngreso);

        ingreso = new()
        {
            CoEmpresa = permiso.CoEmpresa,
            CoPersona = permiso.CoPersona,
            CoPermiso = permiso.CoPermiso,
            NoRefreshToken = noTokenRefresh,          
            NoIp = ingreso.NoIp,
        };
        ingreso.CoIngreso = await IIngreso.InsertAsync(ingreso);


        CredencialModel credential = new()
        {
            CoEmpresa = permiso.CoEmpresa,
            CoPersona = permiso.CoPersona,
            CoPermiso = permiso.CoPermiso,
            NoUsuario = permiso.NoUsuario,
            NoNombreCompleto = permiso.NoNombreCompleto,
            CoIngreso = ingreso.CoIngreso           
        };

        string token = UtilityJwt.GenerateToken(credential, configuration);
        //credential.FeExpiration = r.Item2;

        globalCredencial.Credencial = credential;
        globalCredencial.NoToken = token;
        globalCredencial.NoTokenRefresh = noTokenRefresh;

        //credential.Token = token;

        //using (ModuloUsuarioBusiness b = new())
        //{
        //    moduloUsuario = b.GetByModulo(new()
        //    {
        //        CoEmpresa = persona.CoEmpresa,
        //        CoPersona = persona.CoPersona,
        //        CoModulo = c.CoModulo
        //    });


        //    if (moduloUsuario == null)
        //        return Extension.CreateValidationProblem("400", "Su clave ha expirado");


        //    if (moduloUsuario.FeExpiracion.EsNumero())
        //    {
        //        if (moduloUsuario.FeExpiracion.Num() < DateTime.Now.ToString("yyyyMMdd").Num())
        //            return Extension.CreateValidationProblem("400", "El Usuario ha caducado.");
        //    }
        //    else
        //        return Extension.CreateValidationProblem("400", "Fecha de caducidad del usuario incorrecta.");
        //}
        return TypedResults.Ok(globalCredencial);
    }


    static async Task<Results<Ok<GlobalCredencialModel>, ValidationProblem>> LoginCheck(ClaimsPrincipal user, HttpContext httpContext)
    {
        GlobalCredencialModel globalCredencial = new();
        //ClaimsPrincipal user = httpContext.User;

        if (!user.Identity?.IsAuthenticated ?? true)
            return Extension.CreateValidationProblem("401", "No autorizado.");


        var coEmpresa = user.Claims.FirstOrDefault(c => c.Type == "CoEmpresa")?.Value;
        var coPersona = user.Claims.FirstOrDefault(c => c.Type == "CoPersona")?.Value;
        var coPermiso = user.Claims.FirstOrDefault(c => c.Type == "CoPermiso")?.Value;
        var coIngreso = user.Claims.FirstOrDefault(c => c.Type == "CoIngreso")?.Value;
        var noUsuario = user.Claims.FirstOrDefault(c => c.Type == "NoUsuario")?.Value;

        string token = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

       
      
        CredencialModel credential = new()
        {
            CoEmpresa = int.Parse(coEmpresa!),
            CoPersona = int.Parse(coPersona!),
            CoPermiso = int.Parse(coPermiso!),
            NoUsuario = noUsuario,      
            CoIngreso = int.Parse(coIngreso!)            
        };

        globalCredencial.Credencial = credential;
        globalCredencial.NoToken = token;

     
        return TypedResults.Ok(globalCredencial);
    }

    static async Task<Results<Ok<PermisoModel>, ValidationProblem>> UpdateLanguageAsync(PermisoModel m, IPermisoBusiness IPermiso, UserClaims user)
    {       
        await IPermiso.UpdateLanguageAsync(m);
        return TypedResults.Ok(m);
    }


}


