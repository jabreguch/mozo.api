using Microsoft.AspNetCore.Mvc;
using Mozo.ApiSeguridad.Helper;
using Mozo.AppSeguridad.Interface.Service;
using Mozo.Comun.Helper.Global;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Seguridad;
using System.Collections.Generic;

namespace Mozo.ApiSeguridad.Controllers.Admin;

[Route("[controller]")]
public class MenuApiController : BaseApiController
{
    private readonly IMenuService _menuService;
    private readonly IPaginaService _paginaService;

    public MenuApiController(
        IMenuService menuService
        , IPaginaService paginaService
    )
    {
        _paginaService = paginaService;
        _menuService = menuService;
    }


    [HttpPost]
    [Route("[action]")]
    public IActionResult Insert([FromBody] MenuModel c)
    {
        c.CoMenu = _menuService.Insert(c);
        return Created(Request.Path, c.GetBasicAttr());
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult Update([FromBody] MenuModel c)
    {
        _menuService.Update(c);
        return Ok(c);
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult Delete([FromBody] MenuModel c)
    {
        _menuService.Delete(c);
        return Ok(c);
    }


    [HttpPost]
    [Route("[action]")]
    public IActionResult UpdateState([FromBody] MenuModel c)
    {
        _menuService.UpdateState(c);
        return Ok(c);
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetById([FromQuery] MenuModel c)
    {
        c = _menuService.GetById(c);
        if (c == null)
            return NotFound2();
        return Ok(c);
    }


    [HttpGet]
    [Route("[action]")]
    public ActionResult<IEnumerable<MenuModel>> GetAllMenu([FromQuery] MenuModel c)
    {
        var r = MenuSeguridad.GetAllMenu(c, _menuService, _paginaService);
        return Ok(r);

        //  IEnumerable<MenuModel> r = HelperMenu.GetAllMenu(c, _serviceMenu, _servicePagina, 0);
    }
}