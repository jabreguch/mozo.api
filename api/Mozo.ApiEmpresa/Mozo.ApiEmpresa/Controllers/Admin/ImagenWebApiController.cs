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
    [Route("Empresa/[controller]")]
    public class ImagenwebApiController : BaseApiController
    {
        private readonly IImagenWebService _imagenwebService;
        private IConfiguration _configuration;
        public ImagenwebApiController(IImagenWebService imagenwebService
             , IConfiguration configuration)
        {
            _imagenwebService = imagenwebService;
            _configuration = configuration;
        }


        [HttpPost]
        [Route("[action]")]
        public IActionResult Insert([FromBody] ImagenWebModel c)
        {
            c.CoImagenWeb = _imagenwebService.Insert(c);


            if (c.NoArchivo.NoNulo())
            {
                EnuTipoGeneral.FormatoArchivo.Empresa.ImagenPageWeb.With(x => { x.CoEmpresa = c.CoEmpresa; x.Id = c.CoImagenWeb; x.NoArchivo = c.NoArchivo; x.NoExtension = c.NoExtension; }).SetArchivoFile(_configuration);
                c.NoArchivo = c.CoImagenWeb.Text();
                _imagenwebService.UpdateArchivo(c);
            }

            return Created(Request.Path, c.GetBasicAttr());
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Update([FromBody] ImagenWebModel c)
        {
            _imagenwebService.Update(c);

            if (c.NoArchivo.NoNulo())
            {
                EnuTipoGeneral.FormatoArchivo.Empresa.ImagenPageWeb.With(x => { x.CoEmpresa = c.CoEmpresa; x.Id = c.CoImagenWeb; x.NoArchivo = c.NoArchivo; x.NoExtension = c.NoExtension; }).SetArchivoFile(_configuration);
                c.NoArchivo = c.CoImagenWeb.Text();
                _imagenwebService.UpdateArchivo(c);
            }
            return Ok(c.GetBasicAttr());
        }


        [HttpPost]
        [Route("[action]")]
        public IActionResult UpdateState([FromBody] ImagenWebModel c)
        {
            _imagenwebService.UpdateState(c);
            return Ok(c.GetBasicAttr());
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Delete([FromBody] ImagenWebModel c)
        {
            _imagenwebService.Delete(c);
            return Ok(c);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAll([FromQuery] ImagenWebModel c)
        {
            IEnumerable<ImagenWebModel> r = _imagenwebService.GetAll(c);
            return Ok(r);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetById([FromQuery] ImagenWebModel c)
        {
            c = _imagenwebService.GetById(c);
            if (c == null) return NotFound2();
            return Ok(c);
        }

    }
}
