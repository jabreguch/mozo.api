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
    public class EquipoApiController : BaseApiController
    {
        private readonly IEquipoService _equipoService;
        private IConfiguration _configuration;
        public EquipoApiController(IEquipoService equipoService, IConfiguration configuration)
        {
            _equipoService = equipoService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Insert([FromBody] EquipoModel c)
        {
            c.CoEquipo = _equipoService.Insert(c);
            return Created(Request.Path, c.GetBasicAttr());
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Update([FromBody] EquipoModel c)
        {
            _equipoService.Update(c);
            return Ok(c.GetBasicAttr());
        }


        [HttpPost]
        [Route("[action]")]
        public IActionResult UpdateState([FromBody] EquipoModel c)
        {
            _equipoService.UpdateState(c);
            return Ok(c.GetBasicAttr());
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Delete([FromBody] EquipoModel c)
        {
            _equipoService.Delete(c);
            return Ok(c);
        }

        //******************************//
        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAll([FromQuery] EquipoModel c)
        {
            IEnumerable<EquipoModel> r = _equipoService.GetAll(c);
            return Ok(r);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetById([FromQuery] EquipoModel c)
        {
            c = _equipoService.GetById(c);
            if (c == null) return NotFound2();
            return Ok(c);
        }

    }
}
