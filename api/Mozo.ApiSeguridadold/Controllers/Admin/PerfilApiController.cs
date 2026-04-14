using Microsoft.AspNetCore.Mvc;
using Mozo.AppSeguridad.Interface.Service;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Seguridad;

namespace Mozo.ApiSeguridad.Controllers.Admin;

[Route("[controller]")]
public class PerfilApiController : BaseApiController
{
    public IPerfilService _perfilService;

    public PerfilApiController(IPerfilService c)
    {
        _perfilService = c;
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult Insert([FromBody] PerfilModel c)
    {
        c.CoPerfil = _perfilService.Insert(c);
        return Created(Request.Path, c.GetBasicAttr());
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult Update([FromBody] PerfilModel c)
    {
        _perfilService.Update(c);
        return Ok(c);
    }


    [HttpPost]
    [Route("[action]")]
    public IActionResult Delete([FromBody] PerfilModel c)
    {
        _perfilService.Delete(c);
        return Ok(c);
    }


    [HttpPost]
    [Route("[action]")]
    public IActionResult UpdateState([FromBody] PerfilModel c)
    {
        _perfilService.UpdateState(c);
        return Ok(c);
    }


    [HttpPost]
    [Route("[action]")]
    public IActionResult SetDefault([FromBody] PerfilModel c)
    {
        _perfilService.SetDefault(c);
        return Ok(c);
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetAll([FromQuery] PerfilModel c)
    {
        var r = _perfilService.GetAll(c);
        //return PartialView(r.Paginar(c.NuPagina));           
        return Ok(r);
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetById([FromQuery] PerfilModel c)
    {
        c = _perfilService.GetById(c);
        if (c == null)
            return NotFound2();
        return Ok(c);
    }


    [HttpGet]
    [Route("[action]")]
    public IActionResult GetAllActivo([FromQuery] PerfilModel c) //(int CoGrupo)
    {
        var r = _perfilService.GetAllActivo(c);
        return Ok(r);
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetDefault([FromQuery] PerfilModel c)
    {
        var r = _perfilService.GetDefault(c);
        if (c == null)
            return NotFound2();
        //return PartialView(r.Paginar(c.NuPagina));           
        return Ok(r);
    }
}