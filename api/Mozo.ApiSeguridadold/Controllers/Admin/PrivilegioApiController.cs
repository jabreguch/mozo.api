using Microsoft.AspNetCore.Mvc;
using Mozo.ApiSeguridad.Helper;
using Mozo.AppSeguridad.Interface.Service;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Seguridad;
using System.Linq;

namespace Mozo.ApiSeguridad.Controllers.Admin;

[Route("[controller]")]
public class PrivilegioApiController : BaseApiController
{
    public readonly IMenuService _menuService;
    public readonly IPaginaService _paginaService;
    public readonly IPerfilPrivilegioService _perfilPrivilegioService;
    public readonly IPerfilService _perfilService;

    public PrivilegioApiController(IPerfilService perfilService
        , IPerfilPrivilegioService perfilPrivilegioService
        , IMenuService menuService
        , IPaginaService paginaService
    )
    {
        _perfilService = perfilService;
        _perfilPrivilegioService = perfilPrivilegioService;
        _menuService = menuService;
        _paginaService = paginaService;
    }


    [HttpPost]
    [Route("[action]")]
    public IActionResult Insert([FromBody] PerfilModel c)
    {
        _perfilPrivilegioService.Delete(new PerfilPrivilegioModel { CoPerfil = c.CoPerfil, CoModulo = c.CoModulo });

        foreach (var item in c.PerfilPrivilegioCol)
        {
            item.CoUsuarioLogin = c.CoUsuarioLogin;
            _perfilPrivilegioService.Insert(item);
        }

        return Created(Request.Path, c.GetBasicAttr());
    }


    [HttpGet]
    [Route("[action]")]
    public IActionResult GetAll([FromQuery] PerfilModel c)
    {
        var MenuCol = MenuSeguridad
            .GetAllMenu(new MenuModel { CoModulo = c.CoModulo, CoEstReg = 1 }, _menuService, _paginaService).ToList();

        var PrivilegioPerfilCol = _perfilPrivilegioService
            .GetAll(new PerfilPrivilegioModel { CoModulo = c.CoModulo, CoPerfil = c.CoPerfil }).ToList();

        PerfilPrivilegioModel tmp = null;

        foreach (var Menu in MenuCol.AsNotNull())
            foreach (var Pagina in Menu.PaginalCol.AsNotNull())
            {
                tmp = PrivilegioPerfilCol.FirstOrDefault(x =>
                    x.CoModulo == Pagina.CoModulo && x.CoMenu == Pagina.CoMenu && x.CoPagina == Pagina.CoPagina);
                if (tmp != null)
                    Pagina.CoEstReg = 1;
                else
                    Pagina.CoEstReg = 0;

                foreach (var Flotante in Pagina.PaginaFlotanteCol.AsNotNull())
                {
                    tmp = PrivilegioPerfilCol.FirstOrDefault(x =>
                        x.CoModulo == Flotante.CoModulo && x.CoMenu == Flotante.CoMenu && x.CoPagina == Flotante.CoPagina);
                    if (tmp != null)
                        Flotante.CoEstReg = 1;
                    else
                        Flotante.CoEstReg = 0;
                }

                foreach (var WebService in Pagina.ServicioWebCol.AsNotNull())
                {
                    tmp = PrivilegioPerfilCol.FirstOrDefault(x =>
                        x.CoModulo == WebService.CoModulo && x.CoMenu == WebService.CoMenu &&
                        x.CoPagina == WebService.CoPagina);
                    if (tmp != null)
                        WebService.CoEstReg = 1;
                    else
                        WebService.CoEstReg = 0;
                }


                foreach (var SubPagina in Pagina.SubPaginaCol.AsNotNull())
                {
                    tmp = PrivilegioPerfilCol.FirstOrDefault(x =>
                        x.CoModulo == SubPagina.CoModulo && x.CoMenu == SubPagina.CoMenu &&
                        x.CoPagina == SubPagina.CoPagina);
                    if (tmp != null)
                        SubPagina.CoEstReg = 1;
                    else
                        SubPagina.CoEstReg = 0;


                    foreach (var Flotante in SubPagina.PaginaFlotanteCol.AsNotNull())
                    {
                        tmp = PrivilegioPerfilCol.FirstOrDefault(x =>
                            x.CoModulo == Flotante.CoModulo && x.CoMenu == Flotante.CoMenu &&
                            x.CoPagina == Flotante.CoPagina);
                        if (tmp != null)
                            Flotante.CoEstReg = 1;
                        else
                            Flotante.CoEstReg = 0;
                    }

                    foreach (var WebService in SubPagina.ServicioWebCol.AsNotNull())
                    {
                        tmp = PrivilegioPerfilCol.FirstOrDefault(x =>
                            x.CoModulo == WebService.CoModulo && x.CoMenu == WebService.CoMenu &&
                            x.CoPagina == WebService.CoPagina);
                        if (tmp != null)
                            WebService.CoEstReg = 1;
                        else
                            WebService.CoEstReg = 0;
                    }
                }
            }


        return Ok(MenuCol);
    }
}