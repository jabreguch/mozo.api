using Microsoft.AspNetCore.Mvc;
using Mozo.AppSeguridad.Interface.Service;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Seguridad;

namespace Mozo.ApiSeguridad.Controllers.Admin;

[Route("[controller]")]
public class ModuloUsuarioApiController : BaseApiController
{
    public IModuloUsuarioService _moduloUsuarioService;

    public ModuloUsuarioApiController(IModuloUsuarioService moduloUsuarioService)
    {
        _moduloUsuarioService = moduloUsuarioService;
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult Insert([FromBody] ModuloUsuarioModel c)
    {
        //if (c.FeExpiracion.EsNulo()) return BadRequest2("Ingrese fecha de expiración del usuario");
        c.CoModuloUsuario = _moduloUsuarioService.Insert(c);
        return Created(Request.Path, c.GetBasicAttr());
    }


    [HttpPost]
    [Route("[action]")]
    public IActionResult Update([FromBody] ModuloUsuarioModel c)
    {
        //if (c.FeExpiracion.EsNulo()) return BadRequest2("Ingrese fecha de expiración del usuario");
        _moduloUsuarioService.Update(c);
        return Ok(c);
    }


    [HttpPost]
    [Route("[action]")]
    public IActionResult UpdateConfiguracion([FromBody] ModuloUsuarioModel c)
    {
        //c.TxConfiguracion = c.Serializa();
        _moduloUsuarioService.UpdateConfiguracion(c);
        return Ok(c);
    }


    [HttpPost]
    [Route("[action]")]
    public IActionResult Delete([FromBody] ModuloUsuarioModel c)
    {
        _moduloUsuarioService.Delete(c);
        return Ok(c);
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetById([FromQuery] ModuloUsuarioModel c)
    {
        c = _moduloUsuarioService.GetById(c);
        if (c == null)
            return NotFound2();
        return Ok(c);
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetByModuloAndPersona([FromQuery] ModuloUsuarioModel c)
    {
        c = _moduloUsuarioService.SelByModuloAndPersona(c);
        if (c == null)
            return NotFound2();
        return Ok(c);
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetAllPersona([FromQuery] ModuloUsuarioModel c)
    {
        var r = _moduloUsuarioService.GetAllPersona(c);
        return Ok(r);
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetAllModulo([FromQuery] ModuloUsuarioModel c)
    {
        var r = _moduloUsuarioService.GetAllModulo(c);
        return Ok(r);
    }
}