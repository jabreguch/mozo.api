using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mozo.AppLogin.Interface.Service;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Seguridad;
using System.Collections.Generic;

namespace Mozo.ApiSeguridad.Controllers.Cuenta;

[Route("Cuenta/[controller]")]
public class EmpresaApiController : BaseApiController
{
    public IEmpresaService _empresaService;

    public EmpresaApiController(
        IEmpresaService empresaService
    )
    {
        _empresaService = empresaService;
    }


    [AllowAnonymous]
    [HttpGet]
    [Route("[action]")]
    public IActionResult GetById([FromQuery] EmpresaModel c)
    {
        c = _empresaService.GetById(c);
        if (c == null)
            return NotFound2();

        return Ok(c);
    }



    [AllowAnonymous]
    [HttpGet]
    [Route("[action]")]
    public ActionResult<IEnumerable<EmpresaModel>> GetAll([FromQuery] EmpresaModel c)
    {
        var r = _empresaService.GetAll(c);
        return Ok(r);
    }
}