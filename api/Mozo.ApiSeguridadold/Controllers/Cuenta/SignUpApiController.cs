using Microsoft.AspNetCore.Mvc;
using Mozo.AppLogin.Interface.Service;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Maestro;
using Mozo.Model.Seguridad;

namespace Mozo.ApiSeguridad.Controllers.Cuenta;

[Route("Cuenta/[controller]")]
public class SignUpApiController : BaseApiController
{
    public ISignUpService _signUpService;

    public SignUpApiController(
        ISignUpService signUpService
    )
    {
        _signUpService = signUpService;
    }


    [HttpGet]
    [Route("[action]")]
    public ActionResult<PermisoModel> Insert([FromQuery] PersonaModel c)
    //public IActionResult Insert([FromBody] PersonaModel c)
    {
        PermisoModel x = _signUpService.Insert(c);
        if (x == null)
            return NotFound2();
        return Ok(x);
    }

    [HttpGet]
    [Route("[action]")]
    public ActionResult<PermisoModel> AddApp([FromQuery] PersonaModel c)
    //public IActionResult Insert([FromBody] PersonaModel c)
    {
        PermisoModel x = _signUpService.AddApp(c);
        if (x == null)
            return NotFound2();
        return Ok(x);
    }

}