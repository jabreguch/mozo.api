using Microsoft.AspNetCore.Http.HttpResults;

using Mozo.ApiSeguridad.Helper;
using Mozo.HelperWeb.Api;
using Mozo.HelperWeb.Token;
using Mozo.LoginBusiness;
using Mozo.Model.Seguridad;
using Mozo.Model.Seguridad.Auth;

namespace Mozo.Api.Login;
/// <summary>
/// Clase estatica que para llamar al grupo de end points "tblingreso".
/// </summary>
public static partial class IngresoEndPoints
{
    /// <summary>
    /// Función que devuelve el grupo de end points de la entidad "tblingreso".
    /// </summary>
    public static RouteGroupBuilder MapIngreso(this RouteGroupBuilder g)
    {
        g.DisableAntiforgery().RequireAuthorization();
        g.MapDelete("/tokens/current/{coingreso:int}", RevokeTokenAsync);
        g.MapDelete("/tokens/session/{coingreso:int}", CloseTokenAsync);
        g.MapPost("/tokens/renew", RenewTokenAsync);
        return g;
    }
}

public static partial class IngresoEndPoints
{

    static async Task<Results<NoContent, ValidationProblem>> CloseTokenAsync(int coingreso, IIngresoBusiness IIngreso, UserClaims user)
    {
        IngresoModel m = new() { CoIngreso = coingreso };
        m = m.SetClaims(user);
        m.FeUtcSalida = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        await IIngreso.UpdateCloseTokenAsync(m);
        return TypedResults.NoContent();
    }

    static async Task<Results<NoContent, ValidationProblem>> RevokeTokenAsync(int coingreso, IIngresoBusiness IIngreso, UserClaims user)
    {
        IngresoModel m = new() { CoIngreso = coingreso };
        m = m.SetClaims(user);
        await IIngreso.UpdateRevokeTokenAsync(m);
        return TypedResults.NoContent();
    }

    static async Task<Results<Ok<GlobalCredencialModel>, ValidationProblem>> RenewTokenAsync(IngresoModel m, IConfiguration configuration, IIngresoBusiness IIngreso, UserClaims user)
    {

        IngresoModel? ingreso = await IIngreso.SelByIdAsync(new() { CoEmpresa = m.CoEmpresa, CoIngreso = m.CoIngreso });
        if (ingreso != null)
        {
            if (m.NoRefreshToken != ingreso.NoRefreshToken)
                return Extension.CreateValidationProblem("400", "Token no coincide");
            string noRefresh = UtilityJwt.GenerateRefreshToken();

            await IIngreso.UpdateReNewTokenAsync(new() { CoEmpresa = m.CoEmpresa, CoIngreso = m.CoIngreso, NoRefreshToken = noRefresh });

            //if (credential.CoEmpresa != null)
            //    claimCollection.Add(new Claim("CoEmpresa", credential.CoEmpresa!.Text()));

            //if (credential.CoPersona != null)
            //    claimCollection.Add(new Claim("CoPersona", credential.CoPersona!.Text()));

            //if (credential.CoPermiso != null)
            //    claimCollection.Add(new Claim("CoPermiso", credential.CoPermiso!.Text()));

            //if (credential.CoIngreso != null)
            //    claimCollection.Add(new Claim("CoIngreso", credential.CoIngreso!.Text()));

            //if (credential.NoUsuario != null)
            //    claimCollection.Add(new Claim("NoUsuario", credential.NoUsuario!));

            //ing.coempresa,
            //ing.copersona,
            //ing.coingreso,
            //ing.copermiso,
            //ing.norefreshtoken,
            //ing.feutcingreso,
            //ing.feutcsalida,
            //ing.noip

            CredencialModel credencial = new()
            {
                CoEmpresa = ingreso.CoEmpresa,
                CoPersona = ingreso.CoPersona,
                CoPermiso = ingreso.CoPermiso,
                CoIngreso = ingreso.CoIngreso
                //NoUsuario = ingreso.NoUsuario

            };
            
            (string, long) r = UtilityJwt.GenerateToken(credencial, configuration);

            credencial.NoTokenRefresh = noRefresh;
            credencial.FeExpiration = r.Item2;

            GlobalCredencialModel globalCredencial = new();
            globalCredencial.Credencial = credencial;
            globalCredencial.NoToken = r.Item1;


            return TypedResults.Ok(globalCredencial);
        }
        else
            return Extension.CreateValidationProblem("501", "Ingreso no existe");

    }
}