using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mozo.AppLogin.Interface.Service;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Seguridad;
using System.Collections.Generic;

namespace Mozo.ApiSeguridad.Controllers.Cuenta;

[Route("Cuenta/[controller]")]
public class ModuloUsuarioApiController : BaseApiController
{

    public IModuloUsuarioService _moduloUsuarioService;
    public ModuloUsuarioApiController(
         IModuloUsuarioService moduloUsuarioService
    )
    {
        _moduloUsuarioService = moduloUsuarioService;
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("[action]")]
    public ActionResult<ModuloUsuarioModel> GetByModulo([FromQuery] ModuloUsuarioModel c)
    {
        c = _moduloUsuarioService.GetByModulo(c);
        if (c == null)
            return NotFound2();

        return Ok(c);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("[action]")]
    public ActionResult<IEnumerable<ModuloUsuarioModel>> GetAllPersona([FromQuery]  ModuloUsuarioModel c)
    {
        IEnumerable<ModuloUsuarioModel> r = _moduloUsuarioService.GetAllPersona(c);
        return Ok(r);
    }

}