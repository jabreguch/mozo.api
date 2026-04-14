using Microsoft.AspNetCore.Mvc;
using Mozo.ApiSeguridad.Helper;
using Mozo.AppLogin.Interface.Service;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Seguridad;
using System.Collections.Generic;

namespace Mozo.ApiSeguridad.Controllers.Cuenta;

[Route("Cuenta/[controller]")]
public class MenuApiController : BaseApiController
{
    public IMenuService _menuService;

    public IModuloService _moduloService;
    public IModuloUsuarioService _moduloUsuarioService;
    public IPaginaService _paginaService;
    public IPermisoService _permisoService;


    public MenuApiController(
        IPermisoService permisoService
        , IModuloUsuarioService moduloUsuarioService
        , IModuloService moduloService
        , IMenuService menuService
        , IPaginaService paginaService
    )
    {
        _permisoService = permisoService;
        _moduloUsuarioService = moduloUsuarioService;

        _menuService = menuService;
        _paginaService = paginaService;
        _moduloService = moduloService;
    }


    [HttpGet]
    [Route("[action]")]
    public ActionResult<IEnumerable<ModuloUsuarioModel>> GetAllModulo([FromQuery] ModuloUsuarioModel c)
    {
        return MenuLogin.GetAllModulo(c, _moduloUsuarioService, _menuService, _paginaService);
    }
}