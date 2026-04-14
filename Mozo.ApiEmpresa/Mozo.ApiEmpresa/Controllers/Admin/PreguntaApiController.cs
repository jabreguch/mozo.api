using Microsoft.AspNetCore.Mvc;

using Mozo.AppEmpresa.Interface.Service;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Empresa;
using System.Collections.Generic;
using System.Linq;

namespace Mozo.ApiEmpresa.Controllers.Admin
{
    [Route("[controller]")]
    public class PreguntaApiController : BaseApiController
    {
        private readonly IPreguntaService _preguntaService;

        public PreguntaApiController(IPreguntaService preguntaService)
        {
            _preguntaService = preguntaService;

        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Insert([FromBody] PreguntaModel c)
        {
            c.CoPregunta = _preguntaService.Insert(c);
            return Created(Request.Path, c.GetBasicAttr());
        }


        [HttpPost]
        [Route("[action]")]
        public IActionResult Update([FromBody] PreguntaModel c)
        {
            _preguntaService.Update(c);
            return Ok(c);
        }



        [HttpPost]
        [Route("[action]")]
        public IActionResult UpdateState([FromBody] PreguntaModel c)
        {
            _preguntaService.UpdateState(c);
            return Ok(c);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Delete([FromBody] PreguntaModel c)
        {
            var result = _preguntaService.Delete(c);
            return Ok(result);
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
