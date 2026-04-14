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
    public class HorarioApiController : BaseApiController
    {
        private readonly IHorarioService _horarioService;

        public HorarioApiController(IHorarioService horarioService)
        {
            _horarioService = horarioService;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Insert([FromBody] HorarioModel c)
        {
            c.CoHorario = _horarioService.Insert(c);
            return Created(Request.Path, c.GetBasicAttr());
        }


        [HttpPost]
        [Route("[action]")]
        public IActionResult Update([FromBody] HorarioModel c)
        {
            _horarioService.Update(c);
            return Ok(c);
        }



        [HttpPost]
        [Route("[action]")]
        public IActionResult Delete([FromBody] HorarioModel c)
        {
            var result = _horarioService.Delete(c);
            return Ok(result);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetById([FromQuery] HorarioModel c)
        {
            c = _horarioService.GetById(c);
            if (c == null)
                return NotFound2();
            return Ok(c);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAll([FromQuery] HorarioModel c)
        {
            IEnumerable<HorarioModel> r = _horarioService.GetAll(c);
            r = r.OrderBy(s => s.CoDia);//.ThenBy(s => s.NoTipo).ThenBy(s => s.NuSubOrden);
            //return PartialView(r.Paginar(c.NuPagina));
            return Ok(r);
        }

    }
}
