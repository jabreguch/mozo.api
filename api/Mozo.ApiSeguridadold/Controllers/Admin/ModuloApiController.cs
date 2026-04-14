using Microsoft.AspNetCore.Mvc;
using Mozo.AppSeguridad.Interface.Service;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Seguridad;

namespace Mozo.ApiSeguridad.Controllers.Admin;

[Route("[controller]")]
public class ModuloApiController : BaseApiController
{
    public IModuloService _moduloService;

    public ModuloApiController(IModuloService c)
    {
        _moduloService = c;
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult Insert([FromBody] ModuloModel c)
    {
        c.CoModulo = _moduloService.Insert(c);
        return Created(Request.Path, c.GetBasicAttr());
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult Update([FromBody] ModuloModel c)
    {
        _moduloService.Update(c);
        return Ok(c);
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult Delete([FromBody] ModuloModel c)
    {
        _moduloService.Delete(c);
        return Ok(c);
    }


    [HttpPost]
    [Route("[action]")]
    public IActionResult UpdateState([FromBody] ModuloModel c)
    {
        _moduloService.UpdateState(c);
        return Ok(c);
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetById([FromQuery] ModuloModel c)
    {
        c = _moduloService.GetById(c);
        if (c == null)
            return NotFound2();
        return Ok(c);
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetAll([FromQuery] ModuloModel c)
    {
        var r = _moduloService.GetAll();
        return Ok(r);
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetAllActivo()
    {
        var r = _moduloService.GetAllActivo();
        return Ok(r);
    }


    [HttpGet]
    [Route("[action]")]
    public IActionResult GetAllActivoSinArea()
    {
        var r = _moduloService.GetAllActivoSinArea();
        return Ok(r);
    }
}