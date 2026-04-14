using Mozo.AppSeguridad.Interface.Service;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Helper.util;
using Mozo.Model.Seguridad;
using System.Collections.Generic;
using System.Linq;

namespace Mozo.ApiSeguridad.Helper;

public static class MenuSeguridad
{
    public static IEnumerable<MenuModel> GetAllMenu(
        MenuModel c
        , IMenuService _serviceMenu
        , IPaginaService _servicePagina
    )
    {
        List<MenuModel> MenuCol = null;
        List<PaginaModel> PaginaCol = null;
        List<SubPaginaModel> SubPaginaCol = null;
        List<PaginaFlotanteModel> PaginaFlotanteCol = null;
        List<ServicioWebModel> ServicioWebCol = null;
        if (c.CoEstReg == 1)
        {
            MenuCol = _serviceMenu.GetAllActivo(c).ToList();
            PaginaCol = _servicePagina.GetAllPaginaActivo(new PaginaModel { CoModulo = c.CoModulo }).ToList();
            SubPaginaCol = _servicePagina.GetAllSubPaginaActivo(new SubPaginaModel { CoModulo = c.CoModulo }).ToList();
            PaginaFlotanteCol = _servicePagina
                .GetAllPaginaFlotanteActivo(new PaginaFlotanteModel { CoModulo = c.CoModulo }).ToList();
            ServicioWebCol = _servicePagina.GetAllServicioWebActivo(new ServicioWebModel { CoModulo = c.CoModulo })
                .ToList();
        }
        else
        {
            MenuCol = _serviceMenu.GetAll(c).ToList();
            PaginaCol = _servicePagina.GetAllPagina(new PaginaModel { CoModulo = c.CoModulo }).ToList().ToList();
            SubPaginaCol = _servicePagina.GetAllSubPagina(new SubPaginaModel { CoModulo = c.CoModulo }).ToList();
            PaginaFlotanteCol = _servicePagina.GetAllPaginaFlotante(new PaginaFlotanteModel { CoModulo = c.CoModulo })
                .ToList();
            ServicioWebCol = _servicePagina.GetAllServicioWeb(new ServicioWebModel { CoModulo = c.CoModulo }).ToList();
        }

        MenuCol.Sort(new Sorter<MenuModel>("NuOrden"));
        PaginaCol.Sort(new Sorter<PaginaModel>("NuOrden, NoArea, NoControlador, NoAccion, NoParametro"));
        SubPaginaCol.Sort(new Sorter<SubPaginaModel>("NuOrden, NoArea, NoControlador, NoAccion, NoParametro"));
        PaginaFlotanteCol.Sort(
            new Sorter<PaginaFlotanteModel>("NuOrden, NoArea, NoControlador, NoAccion, NoParametro"));
        ServicioWebCol.Sort(new Sorter<ServicioWebModel>("NuOrden, NoArea, NoControlador, NoAccion, NoParametro"));

        //PaginaAllCol = PaginaAllCol.OrderBy(x => x.CoTipoPagina).ThenBy(x => x.NuOrden).ThenBy(x => x.NoArea).ThenBy(x => x.NoControlador).ThenBy(x => x.NoAccion).ThenBy(x => x.NoParametro).ToList();
        foreach (var Menu in MenuCol)
        {
            // Menu.PaginalCol = PaginaCol.FindAll(x => x.CoMenu == Menu.CoMenu && (x.CoPaginaPadre == 0 || x.CoPaginaPadre == null) );
            Menu.PaginalCol = PaginaCol.FindAll(x =>
                x.CoMenu == Menu.CoMenu && x.CoTipoPagina == 1 && (x.CoPaginaPadre == 0 || x.CoPaginaPadre == null));

            foreach (var Pagina in Menu.PaginalCol)
            {
                Pagina.SubPaginaCol =
                    SubPaginaCol.FindAll(x => x.CoMenu == Pagina.CoMenu && x.CoPaginaPadre == Pagina.CoPagina);
                Pagina.PaginaFlotanteCol =
                    PaginaFlotanteCol.FindAll(x => x.CoMenu == Pagina.CoMenu && x.CoPaginaPadre == Pagina.CoPagina);
                Pagina.ServicioWebCol =
                    ServicioWebCol.FindAll(x => x.CoMenu == Pagina.CoMenu && x.CoPaginaPadre == Pagina.CoPagina);
                //SubPaginaCol.OrderBy(x => x.CoTipoPagina).ThenBy(x => x.NuOrden).ThenBy(x => x.NoArea).ThenBy(x => x.NoControlador).ThenBy(x => x.NoAccion).ThenBy(x => x.NoParametro);

                //if (SubPaginaCol.Count > 0) Pagina.SubPaginaCol = new List<SubPaginaModel>();


                foreach (var SubPagina in Pagina.SubPaginaCol.AsNotNull())
                {
                    SubPagina.PaginaFlotanteCol = PaginaFlotanteCol.FindAll(x =>
                        x.CoMenu == SubPagina.CoMenu && x.CoPaginaPadre == SubPagina.CoPagina);
                    SubPagina.ServicioWebCol = ServicioWebCol.FindAll(x =>
                        x.CoMenu == SubPagina.CoMenu && x.CoPaginaPadre == SubPagina.CoPagina);


                    //SubPaginaModel SubPaginaInsert = new SubPaginaModel();
                    //SubPaginaInsert.CoMenu = SubPagina.CoMenu;
                    //SubPaginaInsert.CoPagina = SubPagina.CoPagina;
                    //SubPaginaInsert.CoModulo = SubPagina.CoModulo;

                    //SubPaginaInsert.CoPaginaPadre = SubPagina.CoPaginaPadre;

                    //SubPaginaInsert.NoControlador = SubPagina.NoControlador;
                    //SubPaginaInsert.NoAccion = SubPagina.NoAccion;
                    //SubPaginaInsert.NoArea = SubPagina.NoArea;
                    //SubPaginaInsert.NoOpcion = SubPagina.NoOpcion;
                    //SubPaginaInsert.NuOrden = SubPagina.NuOrden;
                    //SubPaginaInsert.TxDescripcion = SubPagina.TxDescripcion;
                    //SubPaginaInsert.NoParametro = SubPagina.NoParametro;
                    //SubPaginaInsert.CoTipoPagina = SubPagina.CoTipoPagina;
                    //SubPaginaInsert.CoEstReg = SubPagina.CoEstReg;
                    //Pagina.SubPaginaCol.Add(SubPaginaInsert);
                }
            }
        }

        return MenuCol;
    }
}