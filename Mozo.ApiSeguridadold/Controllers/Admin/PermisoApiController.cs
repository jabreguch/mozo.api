using Microsoft.AspNetCore.Mvc;
using Mozo.AppSeguridad.Interface.Service;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Maestro;
using Mozo.Model.Seguridad;
using System.Collections.Generic;
using System.Linq;

namespace Mozo.ApiSeguridad.Controllers.Admin;

[Route("[controller]")]
public class PermisoApiController : BaseApiController
{
    public IModuloUsuarioService _moduloUsuarioService;
    public IPermisoService _permisoService;

    public PermisoApiController(
        IPermisoService permisoService
        , IModuloUsuarioService moduloUsuarioService
    )
    {
        _permisoService = permisoService;
        _moduloUsuarioService = moduloUsuarioService;
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult Insert([FromBody] PermisoModel c)
    {
        if (c.NoUsuario.EsNulo()) return BadRequest2("Ingrese nombre del Usuario");
        if (c.NoClave.EsNulo()) return BadRequest2("Ingrese clave del usuario");        
        c.CoPermiso = _permisoService.Insert(c);
        return Created(Request.Path, c.GetBasicAttr());
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult Update([FromBody] PermisoModel c)
    {
        if (c.NoUsuario.EsNulo()) return BadRequest2("Ingrese nombre del Usuario");
        if (c.NoClave.EsNulo()) return BadRequest2("Ingrese clave del usuario");
        
        _permisoService.Update(c);
        return Ok(c);
    }


    [HttpPost]
    [Route("[action]")]
    public IActionResult UpdateState([FromBody] PermisoModel c)
    {
        _permisoService.UpdateState(c);
        return Ok(c);
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetAll([FromQuery] PermisoModel c)
    {
        List<PersonaModel> r = _permisoService.GetAll(c).ToList();
        foreach (PersonaModel item in r)
        {
            ModuloUsuarioModel ModuloUsuario = new();
            ModuloUsuario.CoEmpresa = item.CoEmpresa;
            ModuloUsuario.CoPersona = item.CoPersona;
            item.Permiso.ModuloUsuarioCol = _moduloUsuarioService.GetAllPersona(ModuloUsuario).ToList();
        }

        // return PartialView(r.Paginar(c.NuPagina));
        return Ok(r);
    }


    [HttpGet]
    [Route("[action]")]
    public ActionResult GetById([FromQuery] PermisoModel c)
    {
        PersonaModel x = _permisoService.GetById(c);
        return Ok(x);
    }
}