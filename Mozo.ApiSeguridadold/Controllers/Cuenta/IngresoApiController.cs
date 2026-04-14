using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mozo.AppLogin.Interface.Service;
using Mozo.Comun.Implement;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Maestro;
using Mozo.Model.Seguridad;
using System;

namespace Mozo.ApiSeguridad.Controllers.Cuenta;

[Route("Cuenta/[controller]")]
public class IngresoApiController : BaseApiController
{
    public IIngresoService _ingresoService;

    public IngresoApiController(
        IIngresoService ingresoService
    )
    {
        _ingresoService = ingresoService;
    }

    //        @CoEmpresa Int,
    //@CoIngreso Int,
    //@CoPermiso Int,
    //@CoPersona Int,
    //   @FeUtcIngreso Int,
    //   @NoIp varchar(30),
    //   @NoRefreshToken VARCHAR(32)

    [HttpPost]
    [Route("[action]")]
    public IActionResult Insert([FromBody] IngresoModel c)
    {
        c.FeUtcIngreso = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        c.CoIngreso = _ingresoService.Insert(c);
        return Created(Request.Path, c);
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult CloseToken([FromBody] IngresoModel c)
    {
        c.FeUtcSalida = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        _ingresoService.UpdateCloseToken(c);
        return Ok(c);
    }


    //[AllowAnonymous]
    [HttpPost]
    [Route("[action]")]
    public IActionResult RevokeToken([FromBody] IngresoModel c)
    {
        _ingresoService.UpdateRevokeToken(c);
        return Ok(c);
    }
    //      Ing.CoEmpresa,
    //Ing.CoIngreso,
    //Ing.CoPermiso,
    //Ing.CoPersona,
    //Ing.NoRefreshToken,
    //Ing.FeUtcIngreso,
    //Ing.FeUtcSalida,
    //Ing.NoIp

    [AllowAnonymous]
    [HttpPost]
    [Route("[action]")]
    public ActionResult<GlobalCredentialTokenModel> RenewToken([FromBody] IngresoModel c)
    {
        IngresoModel Ingreso = _ingresoService.GetByIdActive(c);
        //CoEmpresa = c.CoEmpresa, CoIngreso = c.CoIngreso, copersona
        if (c.NoRefreshToken != Ingreso.NoRefreshToken) return BadRequest2("Token no coincide");

        PersonaModel Persona = new(); Persona.Permiso = new(); Persona.Ingreso = new();
        Persona.CoPersona = Ingreso.CoEmpresa;
        Persona.CoPersona = Ingreso.CoPersona;
        Persona.Permiso.CoPermiso = Ingreso.CoPermiso;
        Persona.Ingreso.CoIngreso = Ingreso.CoIngreso;

        GlobalCredentialTokenModel GlobalToken = UtilityJwt.GenerateToken(Persona);

        _ingresoService.UpdateReNewToken(new IngresoModel
        { CoEmpresa = c.CoEmpresa, CoIngreso = c.CoIngreso, NoRefreshToken = GlobalToken.NoRefresh });

        return Ok(GlobalToken);
    }
}