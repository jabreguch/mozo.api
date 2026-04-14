using Microsoft.AspNetCore.Mvc;
using Mozo.AppEmpresa.Interface.Service;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Empresa;
using System.Collections.Generic;

namespace Mozo.ApiEmpresa.Controllers.Admin
{
    [Route("Empresa/[controller]")]
    public class BlogMensajeApiController : BaseApiController
    {
        private readonly IBlogMensajeService _blogmensajeService;
        public BlogMensajeApiController(IBlogMensajeService blogmensajeService)
        {
            _blogmensajeService = blogmensajeService;
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult Insert([FromBody] BlogMensajeModel c)
        {
            c.CoBlogMensaje = _blogmensajeService.Insert(c);
            return Created(Request.Path, c.GetBasicAttr());
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult Update([FromBody] BlogMensajeModel c)
        {
            _blogmensajeService.Update(c);
            return Ok(c);
        }


        [HttpPost]
        [Route("[action]")]
        public IActionResult Delete([FromBody] BlogMensajeModel c)
        {
            var result = _blogmensajeService.Delete(c);
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetById([FromQuery] BlogMensajeModel c)
        {
            c = _blogmensajeService.GetById(c);
            if (c == null)
                return NotFound2();
            return Ok(c);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAll([FromQuery] BlogMensajeModel c)
        {
            IEnumerable<BlogMensajeModel> r = _blogmensajeService.GetAll(c);
            //return PartialView(r.Paginar(c.NuPagina));
            return Ok(r);
        }

    }
}
