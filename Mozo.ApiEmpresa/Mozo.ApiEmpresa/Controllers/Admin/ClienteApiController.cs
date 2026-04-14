using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mozo.AppEmpresa.Interface.Service;
using Mozo.Comun.Helper.Enu;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Empresa;
using System.Collections.Generic;

namespace Mozo.ApiEmpresa.Controllers.Admin
{
    [Route("[controller]")]
    public class ClienteApiController : BaseApiController
    {
        private readonly IClienteService _clienteService;
        private IConfiguration _configuration;
        public ClienteApiController(IClienteService clienteService
            , IConfiguration configuration
            )
        {
            _clienteService = clienteService;

            _configuration = configuration;
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
        public IActionResult Insert([FromBody] ClienteModel c)
        {
            c.CoCliente = _clienteService.Insert(c);

            if (c.NoArchivo.NoNulo())
            {
                EnuTipoGeneral.FormatoArchivo.Empresa.ClienteImagen.With(x => { x.CoEmpresa = c.CoEmpresa; x.Id = c.CoCliente; x.NoArchivo = c.NoArchivo; x.NoExtension = c.NoExtension; }).SetArchivoFile(_configuration);
                c.NoArchivo = c.CoCliente.Text();
                _clienteService.UpdateArchivo(c);
            }


            return Created(Request.Path, c.GetBasicAttr());
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Update([FromBody] ClienteModel c)
        {
            _clienteService.Update(c);
            if (c.NoArchivo.NoNulo())
            {
                EnuTipoGeneral.FormatoArchivo.Empresa.ClienteImagen.With(x => { x.CoEmpresa = c.CoEmpresa; x.Id = c.CoCliente; x.NoArchivo = c.NoArchivo; x.NoExtension = c.NoExtension; }).SetArchivoFile(_configuration);
                c.NoArchivo = c.CoCliente.Text();
                _clienteService.UpdateArchivo(c);
            }
            return Ok(c.GetBasicAttr());
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult UpdateState([FromBody] ClienteModel c)
        {
            int result = _clienteService.UpdateState(c);
            return Ok(c);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Delete([FromBody] ClienteModel c)
        {
            var result = _clienteService.Delete(c);
            return Ok(c);
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetById([FromQuery] ClienteModel c)
        {
            c = _clienteService.GetById(c);
            if (c == null)
                return NotFound2();
            return Ok(c);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAll([FromQuery] ClienteModel c)
        {
            IEnumerable<ClienteModel> r = _clienteService.GetAll(c);
            //return PartialView(r.Paginar(c.NuPagina));
            return Ok(r);
        }


        //[HttpGet]
        //[Route("[action]")]
        //public IActionResult GetByIdArchivo([FromQuery] ClienteModel c)       
        //{
        //    ArchivoModel Archivo = _clienteService.GetByIdArchivo(c);

        //    if (Archivo == null)
        //        return NotFound2();

        //    return new JsonResult(new
        //    {
        //        file = File(Archivo.BlArchivo.Resize(c.w, c.h), Archivo.NoExtension.TypeMime(), Archivo.NoArchivoConExtension)
        //    });
        //    //ArchivoModel Archivo = _clienteService.GetByIdArchivo(c);
        //    //return Archivo != null ? "data:" + Archivo.NoExtension.TypeMime() + ";base64," + Convert.ToBase64String(Archivo.BlArchivo) : null;
        //}


    }
}

