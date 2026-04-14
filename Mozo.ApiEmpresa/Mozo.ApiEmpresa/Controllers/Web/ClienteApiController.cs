using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mozo.AppEmpresaWeb.Interface.Service;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Empresa;
using System.Collections.Generic;

namespace Mozo.ApiEmpresa.Controllers.Web
{
    [Route("Web/[controller]")]
    public class ClienteApiController : BaseApiController
    {
        private readonly IClienteService _clienteService;
        private IConfiguration _configuration;
        public ClienteApiController(IClienteService clienteService
            , IConfiguration configuration
            )
        {
            _clienteService = clienteService;
            _configuration = configuration;
        }



        [HttpGet]
        [Route("[action]")]
        public IActionResult GetById([FromQuery] ClienteModel c)
        {
            c = _clienteService.GetById(c);
            if (c == null)
                return NotFound2();
            return Ok(c);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAll([FromQuery] ClienteModel c)
        {
            IEnumerable<ClienteModel> r = _clienteService.GetAll(c);
            //return PartialView(r.Paginar(c.NuPagina));
            return Ok(r);
        }




    }
}

