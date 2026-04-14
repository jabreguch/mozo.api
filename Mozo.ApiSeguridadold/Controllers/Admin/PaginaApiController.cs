using Microsoft.AspNetCore.Mvc;
using Mozo.AppSeguridad.Interface.Service;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Seguridad;

namespace Mozo.ApiSeguridad.Controllers.Admin;

[Route("[controller]")]
public class PaginaApiController : BaseApiController
{
    public readonly IPaginaService _paginaService;

    public PaginaApiController(IPaginaService paginaService)
    {
        _paginaService = paginaService;
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult Insert([FromBody] PaginaModel c)
    {
        c.CoPagina = _paginaService.Insert(c);
        return Created(Request.Path, c.GetBasicAttr());
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult Update([FromBody] PaginaModel c)
    {
        _paginaService.Update(c);
        return Ok(c);
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult Delete([FromBody] PaginaModel c)
    {
        _paginaService.Delete(c);
        return Ok(c);
    }


    [HttpPost]
    [Route("[action]")]
    public IActionResult UpdateState([FromBody] PaginaModel c)
    {
        _paginaService.UpdateState(c);
        return Ok(c);
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetById([FromQuery] PaginaModel c)
    {
        c = _paginaService.GetById(c);
        if (c == null)
            return NotFound2();
        return Ok(c);
    }
}