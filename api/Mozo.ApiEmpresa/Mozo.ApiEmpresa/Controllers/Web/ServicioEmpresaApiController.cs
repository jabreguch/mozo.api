using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mozo.AppEmpresaWeb.Interface.Service;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Empresa;
using System.Collections.Generic;

namespace Mozo.ApiEmpresa.Controllers.Web
{
    [Route("Web/[controller]")]
    public class ServicioEmpresaApiController : BaseApiController
    {
        private readonly IServicioEmpresaService _servicioService;
        private IConfiguration _configuration;
        public ServicioEmpresaApiController(IServicioEmpresaService servicioService
            , IConfiguration configuration
           )
        {
            _servicioService = servicioService;
            _configuration = configuration;
        }



        [HttpGet]
        [Route("[action]")]
        public IActionResult GetById([FromQuery] ServicioEmpresaModel c)
        {
            c = _servicioService.GetById(c);
            if (c == null)
                return NotFound2();
            return Ok(c);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAll([FromQuery] ServicioEmpresaModel c)
        {
            IEnumerable<ServicioEmpresaModel> r = _servicioService.GetAll(c);
            //return PartialView(r.Paginar(c.NuPagina));
            return Ok(r);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllList([FromQuery] ServicioEmpresaModel c)
        {
            IEnumerable<ServicioEmpresaModel> r = _servicioService.GetAllList(c);
            //return PartialView(r.Paginar(c.NuPagina));
            return Ok(r);
        }

    }
}
