using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mozo.AppLogin.Interface.Service;
using Mozo.Comun.Helper.Enu;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Maestro;
using Mozo.Model.Seguridad;
using System;

namespace Mozo.ApiSeguridad.Controllers.Cuenta;

[Route("Cuenta/[controller]")]
public class PermisoApiController : BaseApiController
{
    public IPermisoService _permisoService;
    public IModuloUsuarioService _moduloUsuarioService;
    public PermisoApiController(
        IPermisoService permisoService,
        IModuloUsuarioService moduloUsuarioService
    )
    {
        _moduloUsuarioService = moduloUsuarioService;
        _permisoService = permisoService;
    }


    [AllowAnonymous]
    [HttpGet]
    [Route("[action]")]
    public ActionResult<PermisoModel> GetById([FromQuery] PermisoModel c)
    {
        var Permiso = _permisoService.GetById(c);
        if (Permiso == null)
            return NotFound2("Cuenta no encontrada.");
        return Ok(Permiso);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("[action]")]
    public ActionResult<PersonaModel> GetByCuenta([FromQuery] PermisoModel c)
    {
        PersonaModel Persona = _permisoService.GetByCuenta(c);
        if (Persona == null)
            return NotFound2("Cuenta no encontrada.");
        return Ok(Persona);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("[action]")]
    public ActionResult<GlobalCredentialModel> GetByUser([FromQuery] PermisoModel c)
    {
        PersonaModel Persona = _permisoService.GetByUser(new PermisoModel
        { CoEmpresa = c.CoEmpresa, NuDocumento = c.NuDocumento, NoUsuario = c.NoUsuario, NoClave = c.NoClave });
        if (Persona == null)
            return NotFound2("Usuario no encontrado.");

        ModuloUsuarioModel ModuloUsuario = _moduloUsuarioService.GetByModulo(new ModuloUsuarioModel() { CoEmpresa = Persona.CoEmpresa, CoPersona = Persona.CoPersona, CoModulo = c.CoModulo});
        if (ModuloUsuario == null)
        {
            return BadRequest2("El usuario no tiene acceso al Módulo.");
        }
        else
        {

            if (ModuloUsuario.FeExpiracion.EsNumero())
            {
                if (ModuloUsuario.FeExpiracion.Num() < DateTime.Now.ToString("yyyyMMdd").Num()) return BadRequest2("El Usuario ha caducado.");
            }
            else
            {
                return BadRequest2("Fecha de caducidad del usuario incorrecta.");
            }
        }               

        GlobalCredentialModel GlobalCredential = new()
        {
            CoEmpresa = Persona.CoEmpresa,
            CoPersona = Persona.CoPersona,
            NoPersona = Persona.NoPersona,
            NoApellidoP = Persona.NoApellidoP,
            NoApellidoM = Persona.NoApellidoM,

            NoArchivo = Persona.NoArchivo,
            NoExtension = Persona.NoExtension
        };
        GlobalCredential.Permiso = new GlobalCredentialPermisoModel();
        GlobalCredential.Permiso.CoPermiso = Persona.Permiso.CoPermiso;
        GlobalCredential.Permiso.NoUsuario = Persona.Permiso.NoUsuario;
        GlobalCredential.Permiso.CoLang = Persona.Permiso.CoLang;

        GlobalCredential.Token = UtilityJwt.GenerateToken(Persona);
        return Ok(GlobalCredential);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("[action]")]
    public ActionResult<GlobalCredentialModel> GetUserSystem()
    {


        PermisoModel permiso = new PermisoModel()
        {
            CoEmpresa = EnuCommon.UserSystem.CoEmpresa,
            NoUsuario = EnuCommon.UserSystem.NoUsuario,
            NoClave = EnuCommon.UserSystem.NoClave,
        };

        PersonaModel Persona = _permisoService.GetByUser(permiso);
        if (Persona == null)
            return NotFound2("Usuario no encontrado.");
              

        GlobalCredentialModel GlobalCredential = new()
        {
            CoEmpresa = Persona.CoEmpresa,
            CoPersona = Persona.CoPersona,
            NoPersona = Persona.NoPersona,
            NoApellidoP = Persona.NoApellidoP,
            NoApellidoM = Persona.NoApellidoM,

            NoArchivo = Persona.NoArchivo,
            NoExtension = Persona.NoExtension
        };
        GlobalCredential.Permiso = new GlobalCredentialPermisoModel();
        GlobalCredential.Permiso.CoPermiso = Persona.Permiso.CoPermiso;
        GlobalCredential.Permiso.NoUsuario = Persona.Permiso.NoUsuario;
        GlobalCredential.Permiso.CoLang = Persona.Permiso.CoLang;

        GlobalCredential.Token = UtilityJwt.GenerateToken(Persona);
        return Ok(GlobalCredential);
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult UpdateLanguage([FromBody] PermisoModel c)
    {
        _permisoService.UpdateLanguage(c);
        return Ok(c);
    }
}