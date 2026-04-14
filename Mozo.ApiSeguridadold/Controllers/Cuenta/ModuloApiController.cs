using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mozo.AppLogin.Interface.Service;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Seguridad;
using System.Collections.Generic;

namespace Mozo.ApiSeguridad.Controllers.Cuenta;

[Route("Cuenta/[controller]")]
public class ModuloApiController : BaseApiController
{
    public IMenuService _menuService;

    public IModuloService _moduloService;
    public IModuloUsuarioService _moduloUsuarioService;
    public IPaginaService _paginaService;
    public IPermisoService _permisoService;


    public ModuloApiController(
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


    [AllowAnonymous]
    [HttpGet]
    [Route("[action]")]
    public ActionResult<IEnumerable<ModuloModel>> GetAll()
    {
        var r = _moduloService.GetAll();
        //c.NoUrlWebApi
        return Ok(r);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("[action]")]
    public IActionResult GetById([FromQuery] ModuloModel c)
    {
        c = _moduloService.GetById(c);
        if (c == null)
            return NotFound2();
        return Ok(c);
    }
}