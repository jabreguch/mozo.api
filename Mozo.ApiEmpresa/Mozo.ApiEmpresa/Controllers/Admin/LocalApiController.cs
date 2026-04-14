using Microsoft.AspNetCore.Mvc;
using Mozo.AppEmpresa.Interface.Service;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Empresa;
using System.Collections.Generic;

namespace Mozo.ApiEmpresa.Controllers.Admin
{

    [Route("[controller]")]
    public class LocalApiController : BaseApiController
    {
        public ILocalService _localService;

        public LocalApiController(ILocalService c)
        {
            _localService = c;
        }


        [HttpPost]
        [Route("[action]")]
        public IActionResult Insert([FromBody] LocalModel c)
        {
            c.CoUbigeo = string.Concat(c.CoDepartamento, c.CoProvincia, c.CoDistrito);
            c.CoLocal = _localService.Insert(c);
            return Created(Request.Path, c.GetBasicAttr());
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult Update([FromBody] LocalModel c)
        {
            c.CoUbigeo = string.Concat(c.CoDepartamento, c.CoProvincia, c.CoDistrito);
            _localService.Update(c);
            return Ok(c);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Delete([FromBody] LocalModel c)
        {
            _localService.Delete(c);
            return Ok(c);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult UpdateState([FromBody] LocalModel c)
        {
            _localService.UpdateState(c);
            return Ok(c);
        }


        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAll([FromQuery] LocalModel c)
        {
            IEnumerable<LocalModel> r = _localService.GetAll(c);
            return Ok(r);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAllActivo([FromQuery] LocalModel c)
        {
            IEnumerable<LocalModel> r = _localService.GetAllActivo(c);
            return Ok(r);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetById([FromQuery] LocalModel c)
        {
            c = _localService.GetById(c);
            if (c == null)
                return NotFound2();
            return Ok(c);
        }


    }


}
