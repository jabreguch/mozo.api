using Microsoft.AspNetCore.Mvc;
using Mozo.AppEmpresaWeb.Interface.Service;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Empresa;

namespace Mozo.ApiEmpresa.Controllers.Web
{
    [Route("Web/[controller]")]
    public class BandejaEntradaApiController : BaseApiController
    {
        private readonly IBandejaEntradaService _bandejaentradaService;
        public BandejaEntradaApiController(IBandejaEntradaService bandejaentradaService)
        {
            _bandejaentradaService = bandejaentradaService;

        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Insert([FromBody] BandejaEntradaModel c)
        {
            _bandejaentradaService.Insert(c);
            return Created(Request.Path, c.GetBasicAttr());
        }

    }
}
