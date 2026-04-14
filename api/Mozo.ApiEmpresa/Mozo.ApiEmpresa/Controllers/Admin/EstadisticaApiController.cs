using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mozo.AppEmpresa.Interface.Service;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Empresa;
using System.Collections.Generic;
namespace Mozo.ApiEmpresa.Controllers.Admin
{
    [Route("Empresa/[controller]")]
    public class EstadisticaApiController : BaseApiController
    {
        private readonly IEstadisticaService _estadisticaService;
        private IConfiguration _configuration;
        public EstadisticaApiController(IEstadisticaService estadisticaService, IConfiguration configuration)
        {
            _estadisticaService = estadisticaService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Insert([FromBody] EstadisticaModel c)
        {
            c.CoEstadistica = _estadisticaService.Insert(c);
            return Created(Request.Path, c.GetBasicAttr());
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Update([FromBody] EstadisticaModel c)
        {
            _estadisticaService.Update(c);
            return Ok(c.GetBasicAttr());
        }


        [HttpPost]
        [Route("[action]")]
        public IActionResult UpdateState([FromBody] EstadisticaModel c)
        {
            _estadisticaService.UpdateState(c);
            return Ok(c.GetBasicAttr());
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Delete([FromBody] EstadisticaModel c)
        {
            _estadisticaService.Delete(c);
            return Ok(c);
        }

        //******************************//
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAll([FromQuery] EstadisticaModel c)
        {
            IEnumerable<EstadisticaModel> r = _estadisticaService.GetAll(c);
            return Ok(r);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetById([FromQuery] EstadisticaModel c)
        {
            c = _estadisticaService.GetById(c);
            if (c == null) return NotFound2();
            return Ok(c);
        }

    }
}
