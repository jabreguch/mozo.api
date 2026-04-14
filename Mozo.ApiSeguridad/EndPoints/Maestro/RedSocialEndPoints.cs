using Microsoft.AspNetCore.Http.HttpResults;

using Mozo.ApiSeguridad.Helper;
using Mozo.MaestroBusiness;
using Mozo.Model.Maestro;

namespace Mozo.Api.Maestro;

public static partial class RedSocialEndPoints
{
    public static RouteGroupBuilder MapRedSocial(this RouteGroupBuilder g)
    {
        g.DisableAntiforgery().RequireAuthorization();
        g.MapPost("/", InsertAsync);
        g.MapPut("/", UpdateAsync);
        g.MapDelete("/{coredsocial:int}", DeleteByIdAsync);
        g.MapGet("/{coredsocial:int}", SelByIdAsync);
        g.MapGet("/", SelAllAsync);
        g.MapGet("/active", SelAllActiveAsync);
        return g;
    }
}
public static partial class RedSocialEndPoints
{
    static async Task<Results<Created<int?>, ValidationProblem>> InsertAsync(RedSocialModel m, IRedSocialBusiness IRedSocial, UserClaims user)
    {
        
        m.CoRedSocial = await IRedSocial.InsertAsync(m);
        return TypedResults.Created($"/{m.CoRedSocial}", m.CoRedSocial);
    }
    static async Task<Results<Ok<int?>, ValidationProblem>> UpdateAsync(RedSocialModel m, IRedSocialBusiness IRedSocial, UserClaims user)
    {
        
        await IRedSocial.UpdateAsync(m);
        return TypedResults.Ok(m.CoRedSocial);
    }

    static async Task<Results<NoContent, ValidationProblem>> DeleteByIdAsync(int coredsocial, IRedSocialBusiness IRedSocial, UserClaims user)
    {
        RedSocialModel m = new() { CoRedSocial = coredsocial };
        
        await IRedSocial.DeleteByIdAsync(m);
        return TypedResults.NoContent();
    }

    static async Task<Results<Ok<IEnumerable<RedSocialModel>>, ValidationProblem>> SelAllAsync([AsParameters] RedSocialFilter f, IRedSocialBusiness IRedSocial, UserClaims user)
    {        
        IEnumerable<RedSocialModel> r = await IRedSocial.SelAllAsync(new());
        r = r.OrderBy(x => x.CoTipoRedSocial).ThenBy(y => y.NoRedSocial);
        return TypedResults.Ok(r);
    }

    static async Task<Results<Ok<IEnumerable<RedSocialModel>>, ValidationProblem>> SelAllActiveAsync([AsParameters] RedSocialFilter f, IRedSocialBusiness IRedSocial, UserClaims user)
    {     
        IEnumerable<RedSocialModel> r = await IRedSocial.SelAllActiveAsync(new());
        return TypedResults.Ok(r);
    }

    static async Task<Results<Ok<RedSocialModel>, NotFound, ValidationProblem>> SelByIdAsync(int coredsocial, IRedSocialBusiness IRedSocial, UserClaims user)
    {
        RedSocialModel m = new() { CoRedSocial = coredsocial };
        
        RedSocialModel? i = await IRedSocial.SelByIdAsync(m);
        if (i == null)
            return TypedResults.NotFound();
        return TypedResults.Ok(i);
    }

}



