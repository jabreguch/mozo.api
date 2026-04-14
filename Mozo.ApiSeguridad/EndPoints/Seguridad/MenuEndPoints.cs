using Microsoft.AspNetCore.Http.HttpResults;

using Mozo.ApiSeguridad.Helper;
using Mozo.Model.Seguridad;
using Mozo.SeguridadBusiness;

namespace Mozo.Api.Seguridad;
///<summary>
/// MenuEndPointsAdmin
///</summary>
///<history>
/// Create by Jonatan Abregu
///</history>
public static partial class MenuEndPoints
{///<summary>
/// MapMenuAdmin
///</summary>
///<history>
/// Create by Jonatan Abregu
///</history>
    public static RouteGroupBuilder MapMenu(this RouteGroupBuilder g)
    {
        g.DisableAntiforgery().RequireAuthorization();
        g.MapPost("/", InsertAsync);
        g.MapPut("/", UpdateAsync);
        g.MapDelete("/{comenu:int}", DeleteByIdAsync);
        g.MapPatch("/state", UpdateStateAsync);
        g.MapGet("/{comenu:int}", SelByIdAsync);
        g.MapGet("/arbol", SelAllArbolAsync);
        return g;
    }

}
public partial class MenuEndPoints
{

    static async Task<Results<Created<int?>, ValidationProblem>> InsertAsync(MenuModel m, IMenuBusiness IMenu, UserClaims user)
    {
        
        m.CoMenu = await IMenu.InsertAsync(m);
        return TypedResults.Created($"/{m.CoMenu}", m.CoMenu);
    }

    static async Task<Results<Ok<int?>, ValidationProblem>> UpdateAsync(MenuModel m, IMenuBusiness IMenu, UserClaims user)
    {
     
        await IMenu.UpdateAsync(m);
        return TypedResults.Ok(m.CoMenu);
    }

    static async Task<Results<NoContent, ValidationProblem>> DeleteByIdAsync(int comenu, IMenuBusiness IMenu, UserClaims user)
    {
        MenuModel m = new() { CoMenu = comenu };
       
        await IMenu.DeleteByIdAsync(m);
        return TypedResults.NoContent();
    }


    static async Task<Results<Ok<int?>, ValidationProblem>> UpdateStateAsync(MenuModel m, IMenuBusiness IMenu, UserClaims user)
    {
       
        await IMenu.UpdateStateAsync(m);
        return TypedResults.Ok(m.CoMenu);
    }

    static async Task<Results<Ok<MenuModel>, NotFound, ValidationProblem>> SelByIdAsync(int comenu, IMenuBusiness IMenu, UserClaims user)
    {
        MenuModel m = new() { CoMenu = comenu };
       
        MenuModel? i = await IMenu.SelByIdAsync(m);
        if (i == null)
            return TypedResults.NotFound();
        return TypedResults.Ok(i);
    }


    static async Task<Results<Ok<List<MenuModel>>, ValidationProblem>> SelAllArbolAsync([AsParameters] MenuFilter f, IMenuBusiness IMenu, IPaginaBusiness IPagina, UserClaims user)
    {
        
        return TypedResults.Ok(await MenuSeguridad.SelAllArbolAsync(new(), IMenu, IPagina));
    }

}