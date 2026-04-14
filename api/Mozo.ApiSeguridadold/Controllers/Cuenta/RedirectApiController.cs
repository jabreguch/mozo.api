using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mozo.AppLogin.Interface.Service;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Seguridad;

namespace Mozo.ApiSeguridad.Controllers.Cuenta;

[Route("Cuenta/[controller]")]
public class RedirectApiController : BaseApiController
{
    public IRedirectService _redirectService;

    public RedirectApiController(
        IRedirectService redirectService
    )
    {
        _redirectService = redirectService;
    }


    [HttpPost]
    [Route("[action]")]
    public IActionResult Insert([FromBody] RedirectModel c)
    {
        c.CoRedirect = _redirectService.Insert(c);
        return Created(Request.Path, c);
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("[action]")]
    public IActionResult Delete([FromBody] RedirectModel c)
    {
        _redirectService.Delete(c);
        return Ok(c);
    }


    [AllowAnonymous]
    [HttpGet]
    [Route("[action]")]
    public IActionResult GetById([FromQuery] RedirectModel c)
    {
        c = _redirectService.GetById(c);
        if (c == null)
            return NotFound2();
        return Ok(c);
    }
}