using Microsoft.AspNetCore.Http.HttpResults;

using Mozo.ApiSeguridad.Helper;
using Mozo.Model.Seguridad;
using Mozo.SeguridadBusiness;

namespace Mozo.Api.Seguridad;


public static partial class PerfilPaginaEndPoints
{
    public static RouteGroupBuilder MapPerfilPagina(this RouteGroupBuilder g)
    {
        g.DisableAntiforgery().RequireAuthorization();
        g.MapPost("/", SaveAsync);
        g.MapGet("/", SelAllAsync);
        return g;
    }

}

public partial class PerfilPaginaEndPoints
{
    static async Task<Results<Created<int?>, ValidationProblem>> SaveAsync(PerfilModel m, IPerfilPaginaBusiness IPerfilPagina, UserClaims user)
    {
        
        foreach (PerfilPaginaModel item in m.PerfilPaginaLst ?? new())
        {
            //item.CoEmpresa = m.CoEmpresa;
            //item.CoUsuCre = m.CoUsuCre;
            await IPerfilPagina.InsertAsync(item);
        }
        return TypedResults.Created($"/{m.CoPerfil}", m.CoPerfil);
    }

    static async Task<Results<Ok<List<MenuModel>>, ValidationProblem>> SelAllAsync([AsParameters] PerfilPaginaFilter f, IMenuBusiness IMenu, IPaginaBusiness IPagina, IPerfilPaginaBusiness IPerfilPagina, UserClaims user)
    {
        PerfilPaginaModel m = new();
        List<MenuModel> MenuLst = (await MenuSeguridad.SelAllArbolAsync(new MenuModel { CoModulo = m.CoModulo, FlEstReg = 1 }, IMenu, IPagina)) ?? new();


        List<PerfilPaginaModel> PrivilegioPerfilLst = (await IPerfilPagina.SelAllAsync(new() { CoModulo = m.CoModulo, CoPerfil = m.CoPerfil })).ToList();

        PerfilPaginaModel? tmp = null;

        foreach (MenuModel Menu in MenuLst ?? new())
        {

            tmp = PrivilegioPerfilLst.FirstOrDefault(x => x.CoModulo == Menu.CoModulo && x.CoMenu == Menu.CoMenu);
            if (tmp != null)
                Menu.FlEstReg = 1;
            else
                Menu.FlEstReg = 0;


            foreach (PaginaModel Pagina in Menu.PaginaLst ?? new())
            {
                tmp = PrivilegioPerfilLst.FirstOrDefault(x => x.CoModulo == Pagina.CoModulo && x.CoMenu == Pagina.CoMenu && x.CoPagina == Pagina.CoPagina);
                if (tmp != null)
                    Pagina.FlEstReg = 1;
                else
                    Pagina.FlEstReg = 0;

                foreach (PaginaFlotanteModel Flotante in Pagina.PaginaFlotanteLst ?? new())
                {
                    tmp = PrivilegioPerfilLst.FirstOrDefault(x => x.CoModulo == Flotante.CoModulo && x.CoMenu == Flotante.CoMenu && x.CoPagina == Flotante.CoPagina);
                    if (tmp != null)
                        Flotante.FlEstReg = 1;
                    else
                        Flotante.FlEstReg = 0;
                }

                //foreach (ServicioWebModel WebService in Pagina.ServicioWebLst ?? new())
                //{
                //    tmp = PrivilegioPerfilLst.FirstOrDefault(x => x.CoModulo == WebService.CoModulo && x.CoMenu == WebService.CoMenu && x.CoPagina == WebService.CoPagina);
                //    if (tmp != null)
                //        WebService.CoEstReg = 1;
                //    else
                //        WebService.CoEstReg = 0;
                //}


                foreach (SubPaginaModel SubPagina in Pagina.SubPaginaLst ?? new())
                {
                    tmp = PrivilegioPerfilLst.FirstOrDefault(x => x.CoModulo == SubPagina.CoModulo && x.CoMenu == SubPagina.CoMenu && x.CoPagina == SubPagina.CoPagina);
                    if (tmp != null)
                        SubPagina.FlEstReg = 1;
                    else
                        SubPagina.FlEstReg = 0;


                    //foreach (PaginaFlotanteModel Flotante in SubPagina.PaginaFlotanteLst ?? new())
                    //{
                    //    tmp = PrivilegioPerfilLst.FirstOrDefault(x => x.CoModulo == Flotante.CoModulo && x.CoMenu == Flotante.CoMenu && x.CoPagina == Flotante.CoPagina);
                    //    if (tmp != null)
                    //    {
                    //        Flotante.CoEstReg = 1;
                    //        Flotante.CoEstReg2 = true;
                    //    }
                    //    else
                    //    {
                    //        Flotante.CoEstReg = 0;
                    //        Flotante.CoEstReg2 = false;
                    //    }
                    //}

                    //foreach (var WebService in SubPagina.ServicioWebLst.AsNotNull())
                    //{
                    //    tmp = PrivilegioPerfilLst.FirstOrDefault(x => x.CoModulo == WebService.CoModulo && x.CoMenu == WebService.CoMenu && x.CoPagina == WebService.CoPagina);
                    //    if (tmp != null)
                    //        WebService.CoEstReg = 1;
                    //    else
                    //        WebService.CoEstReg = 0;
                    //}
                }
            }
        }
        return TypedResults.Ok(MenuLst);
    }

}