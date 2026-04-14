using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Mozo.AppSeguridad.Interface.Service;
using Mozo.Comun.Helper.Enu;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Seguridad;

namespace Mozo.ApiSeguridad.Controllers.Admin;

[Route("[controller]")]
public class EmpresaApiController : BaseApiController
{
    private readonly IEmpresaService _empresaService;
    private readonly ILogger<EmpresaApiController> _logger;
    private readonly IConfiguration _configuration;

    public EmpresaApiController(IEmpresaService empresaService
        , IConfiguration configuration
        , ILogger<EmpresaApiController> logger)
    {
        _empresaService = empresaService;
        _configuration = configuration;
        _logger = logger;
    }


    //private IConfiguration _configuration;
    //public UploadApiController(
    // IConfiguration configuration
    //)
    //{
    //    _configuration = configuration;
    //}

    [HttpPost]
    [Route("[action]")]
    public IActionResult Insert([FromBody] EmpresaModel c)
    {
        c.CoUbigeo = string.Concat(c.CoDepartamento, c.CoProvincia, c.CoDistrito);
        c.CoEmpresa = _empresaService.Insert(c);
        if (c.NoArchivo.NoNulo())
        {
            EnuTipoGeneral.FormatoArchivo.Seguridad.LogoDefaultEmpresa.With(x =>
            {
                x.Id = c.CoEmpresa;
                x.NoArchivo = c.NoArchivo;
                x.NoExtension = c.NoExtension;
            }).SetArchivoFile(_configuration);
            c.NoArchivo = c.CoEmpresa.Text();
            _empresaService.UpdateFile(c);
        }

        if (c.NoLogoLigthArchivo.NoNulo())
        {
            EnuTipoGeneral.FormatoArchivo.Seguridad.LogoDefaultEmpresa.With(x =>
            {
                x.Id = c.CoEmpresa;
                x.NoArchivo = c.NoLogoLigthArchivo;
                x.NoExtension = c.NoLogoLigthExtension;
            }).SetArchivoFile(_configuration);
            c.NoLogoLigthArchivo = string.Concat("LogoLight", c.CoEmpresa.Text());
            _empresaService.UpdateFileLogoLight(c);
        }

        //if (c.Archivo != null)
        //{
        //    c.Archivo.SetArchivoBlob(_configuration);
        //    _empresaService.UpdateArchivo(c);
        //}
        return Created(Request.Path, c.GetBasicAttr());
    }


    [HttpPost]
    [Route("[action]")]
    public IActionResult Update([FromBody] EmpresaModel c)
    {
        c.CoUbigeo = string.Concat(c.CoDepartamento, c.CoProvincia, c.CoDistrito);
        _empresaService.Update(c);
        if (c.NoArchivo.NoNulo())
        {
            EnuTipoGeneral.FormatoArchivo.Seguridad.LogoDefaultEmpresa.With(x =>
            {
                x.Id = c.CoEmpresa;
                x.NoArchivo = c.NoArchivo;
                x.NoExtension = c.NoExtension;
            }).SetArchivoFile(_configuration);
            c.NoArchivo = c.CoEmpresa.Text();
            _empresaService.UpdateFile(c);
        }

        if (c.NoLogoLigthArchivo.NoNulo())
        {
            EnuTipoGeneral.FormatoArchivo.Seguridad.LogoLigthEmpresa.With(x =>
            {
                x.Id = c.CoEmpresa;
                x.NoArchivo = c.NoLogoLigthArchivo;
                x.NoExtension = c.NoLogoLigthExtension;
            }).SetArchivoFile(_configuration);
            c.NoLogoLigthArchivo = c.CoEmpresa.Text();
            _empresaService.UpdateFileLogoLight(c);
        }

        //c.SetArchivoFile(_configuration, EnuTipoGeneral.FormatoArchivo.Seguridad.LogoEmpresa.With(x => { x.NoArchivo = c.CoEmpresa.Text(); x.NoExtension = c.NoExtension; }));


        //if (c.NoArchivo.NoNulo())
        //{
        //    //c.Archivo.TipoSeleccion = EnuTipoGeneral.FormatoArchivo.Seguridad.LogoEmpresa;
        //    //c.Archivo.TipoSeleccion.CoEmpresa = c.CoEmpresa;
        //    //c.Archivo.TipoSeleccion.Id = c.CoEmpresa;
        //    c.SetArchivoFile(_configuration, EnuTipoGeneral.FormatoArchivo.Seguridad.LogoEmpresa);
        //    //public TipoSeleccionModel TipoSeleccion { get; set; }
        //    //_empresaService.UpdateArchivo(c);
        //}
        return Ok(c.GetBasicAttr());
    }

    //[HttpPost]
    //[Route("[action]")]
    //private void UpdateArchivo(EmpresaModel c)
    //{
    //    c.Archivo.SetArchivoBlob();
    //    _empresaService.UpdateArchivo(c);
    //    //return Ok(c);
    //}

    [HttpPost]
    [Route("[action]")]
    public IActionResult Delete([FromBody] EmpresaModel c)
    {
        var result = _empresaService.Delete(c);
        return Ok(result);
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult UpdateState([FromBody] EmpresaModel c)
    {
        var result = _empresaService.UpdateState(c);
        return Ok(result);
    }


    //[AllowAnonymous]
    //[HttpGet]
    //[Route("[action]")]
    //public IActionResult GetByIdArchivo([FromQuery] EmpresaModel c)
    //{
    //    ArchivoModel Archivo = _empresaService.GetByIdArchivo(c);
    //    if (Archivo == null)
    //        return NotFound2();

    //    return new JsonResult(new
    //    {
    //        file = File(Archivo.BlArchivo.Resize(c.w, c.h), Archivo.NoExtension.TypeMime(), Archivo.NoArchivoConExtension)
    //    });

    //}


    //[AllowAnonymous]
    //[HttpGet]
    //[Route("[action]")]
    //public string GetByIdArchivo([FromQuery] EmpresaModel c)
    //{
    //    ArchivoModel Archivo = _empresaService.GetByIdArchivo(c);
    //    return Archivo != null ? "data:" + Archivo.NoExtension.TypeMime() + ";base64," + Convert.ToBase64String(Archivo.BlArchivo): null;
    //}

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetById([FromQuery] EmpresaModel c)
    {
        c = _empresaService.GetById(c);
        if (c == null)
            return NotFound2();
        return Ok(c);
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetAll([FromQuery] EmpresaModel c)
    {
        var r = _empresaService.GetAll(c);
        //return PartialView(EmpresaCol.Paginar(c.NuPagina));
        return Ok(r);
        //foreach (EmpresaModel Empresa in EmpresaCol)
        //{
        //    List<EmpresaModuloModel> TmpEmpresaModuloCol = _empresaModuloService.GetAll(new EmpresaModuloModel()
        //    {
        //        CoEmpresa = Empresa.CoEmpresa
        //    }).ToList();

        //    Empresa.EmpresaModuloCol = new List<EmpresaModulo2>();

        //    foreach (EmpresaModuloModel EmpresaModulo in TmpEmpresaModuloCol)
        //    {
        //        Empresa.EmpresaModuloCol.Add(
        //            new EmpresaModulo2()
        //            {
        //                NoModuloDescripcion = EmpresaModulo.NoModuloDescripcion,
        //                NoModulo = EmpresaModulo.NoModulo
        //            }
        //            );
        //    }

        //}
    }


    [AllowAnonymous]
    [HttpGet]
    [Route("[action]")]
    public IActionResult GetAllActivo([FromQuery] EmpresaModel c)
    {
        var r = _empresaService.GetAllActivo(c);
        return Ok(r);
    }
}