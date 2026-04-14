using Microsoft.AspNetCore.Mvc;
using Mozo.AppEmpresaWeb.Interface.Service;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Empresa;
using System.Collections.Generic;
using System.Linq;

namespace Mozo.ApiEmpresa.Controllers.Web
{
    [Route("Web/[controller]")]
    public class HorarioApiController : BaseApiController
    {
        private readonly IHorarioService _horarioService;
        public HorarioApiController(IHorarioService horarioService)
        {
            _horarioService = horarioService;

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
