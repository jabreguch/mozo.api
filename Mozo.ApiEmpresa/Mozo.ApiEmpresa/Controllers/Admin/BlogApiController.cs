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
    public class BlogApiController : BaseApiController
    {
        private readonly IBlogService _blogService;
        private IConfiguration _configuration;
        public BlogApiController(IBlogService blogService
            , IConfiguration configuration
            )
        {
            _blogService = blogService;
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
        public IActionResult Insert([FromBody] BlogModel c)
        {
            //  int ss = int.Parse("ff");

            c.CoBlog = _blogService.Insert(c);

            if (c.NoArchivo.NoNulo())
            {
                EnuTipoGeneral.FormatoArchivo.Empresa.BlogImagen.With(x => { x.CoEmpresa = c.CoEmpresa; x.Id = c.CoBlog; x.NoArchivo = c.NoArchivo; x.NoExtension = c.NoExtension; }).SetArchivoFile(_configuration);
                c.NoArchivo = c.CoBlog.Text();
                _blogService.UpdateArchivo(c);
            }

            return Created(Request.Path, c.GetBasicAttr());

        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Update([FromBody] BlogModel c)
        {
            _blogService.Update(c);
            if (c.NoArchivo.NoNulo())
            {
                EnuTipoGeneral.FormatoArchivo.Empresa.BlogImagen.With(x => { x.CoEmpresa = c.CoEmpresa; x.Id = c.CoBlog; x.NoArchivo = c.NoArchivo; x.NoExtension = c.NoExtension; }).SetArchivoFile(_configuration);
                c.NoArchivo = c.CoBlog.Text();
                _blogService.UpdateArchivo(c);
            }
            return Ok(c.GetBasicAttr());
        }

        //[HttpPost]
        //[Route("[action]")]
        //public IActionResult UpdateArchivo([FromBody] BlogModel c)
        //{
        //    c.Archivo.SetArchivoBlob(_configuration);
        //    _blogService.UpdateArchivo(c);
        //    return Ok(c);
        //}

        [HttpPost]
        [Route("[action]")]
        public IActionResult UpdateState([FromBody] BlogModel c)
        {
            _blogService.UpdateState(c);
            return Ok(c);

        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Delete([FromBody] BlogModel c)
        {
            var result = _blogService.Delete(c);
            return Ok(result);
        }



        //[HttpGet]
        //[Route("[action]")]
        //public IActionResult GetByIdArchivo([FromQuery] BlogModel c)
        //{
        //    ArchivoModel Archivo = _blogService.GetByIdArchivo(c);

        //    if (Archivo == null)
        //        return NotFound2();

        //    return new JsonResult(new
        //    {
        //        file = File(Archivo.BlArchivo.Resize(c.w, c.h), Archivo.NoExtension.TypeMime(), Archivo.NoArchivoConExtension)
        //    });

        //    //ArchivoModel Archivo = _blogService.GetByIdArchivo(c);
        //    //return Archivo != null ? "data:" + Archivo.NoExtension.TypeMime() + ";base64," + Convert.ToBase64String(Archivo.BlArchivo) : null;
        //}


        [HttpGet]
        [Route("[action]")]
        public IActionResult GetById([FromQuery] BlogModel c)
        {
            c = _blogService.GetById(c);
            if (c == null)
                return NotFound2();
            return Ok(c);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAll([FromQuery] BlogModel c)
        {
            IEnumerable<BlogModel> r = _blogService.GetAll(c);
            //return PartialView(r.Paginar(c.NuPagina));
            return Ok(r);
        }

    }
}
