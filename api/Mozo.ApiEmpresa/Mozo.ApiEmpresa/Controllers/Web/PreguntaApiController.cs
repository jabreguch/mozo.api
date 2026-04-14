using Microsoft.AspNetCore.Mvc;
using Mozo.AppEmpresaWeb.Interface.Service;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Empresa;
using System.Collections.Generic;
using System.Linq;

namespace Mozo.ApiEmpresa.Controllers.Web
{
    [Route("Web/[controller]")]
    public class PreguntaApiController : BaseApiController
    {
        private readonly IPreguntaService _preguntaService;
        public PreguntaApiController(IPreguntaService preguntaService)
        {
            _preguntaService = preguntaService;
        }


        [HttpGet]
        [Route("[action]")]
        public IActionResult GetById([FromQuery] PreguntaModel c)
        {
            c = _preguntaService.GetById(c);
            if (c == null)
                return NotFound2();
            return Ok(c);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAll([FromQuery] PreguntaModel c)
        {
            IEnumerable<PreguntaModel> r = _preguntaService.GetAll(c);
            r = r.OrderBy(x => x.NuOrden);
            return Ok(r);
        }


    }
}
