using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mozo.AppEmpresaWeb.Interface.Service;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Empresa;
using System.Collections.Generic;

namespace Mozo.ApiEmpresa.Controllers.Web
{
    [Route("Web/[controller]")]
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


        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllLast([FromQuery] BlogModel c)
        {
            IEnumerable<BlogModel> r = _blogService.GetAllLast(c);
            //return PartialView(r.Paginar(c.NuPagina));
            return Ok(r);
        }


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
