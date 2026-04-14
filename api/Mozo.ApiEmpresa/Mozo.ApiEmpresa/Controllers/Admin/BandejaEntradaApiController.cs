using Microsoft.AspNetCore.Mvc;
using Mozo.AppEmpresa.Interface.Service;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Empresa;
using System.Collections.Generic;

namespace Mozo.ApiEmpresa.Controllers.Admin
{
    [Route("Empresa/[controller]")]
    public class BandejaEntradaApiController : BaseApiController
    {
        private readonly IBandejaEntradaService _bandejaentradaService;
        public BandejaEntradaApiController(IBandejaEntradaService bandejaentradaService)
        {
            _bandejaentradaService = bandejaentradaService;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult UpdateFavorito([FromBody] BandejaEntradaModel c)
        {
            if (c.FlFavorito == 1)
            {
                c.FlFavorito = 0;
            }
            else
            {
                c.FlFavorito = 1;
            }
            _bandejaentradaService.UpdateFavorito(c);
            return Ok(c);
        }


        [HttpPost]
        [Route("[action]")]
        public IActionResult Delete([FromBody] BandejaEntradaModel c)
        {
            //foreach (BandejaEntradaModel Item in BandejaEntradCol)
            //{
            //    Item.FlSitReg = "I";
            //    int result = _bandejaentradaService.Delete(Item);
            //}
            c.FlSitReg = "I";
            var result = _bandejaentradaService.Delete(c);
            return Ok(result);

        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult UnDelete([FromBody] BandejaEntradaModel c)
        {
            //foreach (BandejaEntradaModel Item in BandejaEntradCol)
            //{
            //    Item.FlSitReg = "A";
            //    int result = _bandejaentradaService.Delete(Item);
            //}

            //return Ok(new
            //{
            //    success = true,
            //});
            c.FlSitReg = "A";
            var result = _bandejaentradaService.Delete(c);
            return Ok(result);
        }


        [HttpGet]
        [Route("[action]")]
        public IActionResult GetById([FromQuery] BandejaEntradaModel c)
        {
            c = _bandejaentradaService.GetById(c);
            if (c == null)
                return NotFound2();

            c.CoEstReg = 0;
            _bandejaentradaService.UpdateState(c);
            return Ok(c);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetAll([FromQuery] BandejaEntradaModel c)
        {
            IEnumerable<BandejaEntradaModel> r = _bandejaentradaService.GetAll(c);
            return Ok(r);
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetQtUnRead([FromQuery] BandejaEntradaModel c)
        {
            int Qt = _bandejaentradaService.GetQtUnRead(c);
            return Ok(Qt);
        }


    }
}
