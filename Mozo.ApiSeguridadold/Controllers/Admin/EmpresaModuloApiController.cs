using Microsoft.AspNetCore.Mvc;
using Mozo.AppSeguridad.Interface.Service;
using Mozo.Comun.Implement.Api;
using Mozo.Model.Seguridad;
using System.Linq;

namespace Mozo.ApiSeguridad.Controllers.Admin;

[Route("[controller]")]
public class EmpresaModuloApiController : BaseApiController
{
    public IEmpresaModuloService _empresaModuloService;
    public IModuloService _moduloService;

    public EmpresaModuloApiController(
        IEmpresaModuloService empresaModuloService
        , IModuloService moduloService
    )
    {
        _moduloService = moduloService;
        _empresaModuloService = empresaModuloService;
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult Update([FromBody] EmpresaModuloModel c)
    {
        _empresaModuloService.Update(c);
        return Ok(c);
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult UpdateOnlyTypeMasterTable([FromBody] EmpresaModuloModel c)
    {
        _empresaModuloService.UpdateOnlyTypeMasterTable(c);
        return Ok(c);
    }


    [HttpGet]
    [Route("[action]")]
    public IActionResult GetAllActivo([FromQuery] EmpresaModuloModel c)
    {
        var EmpresaModuloCol = _empresaModuloService.GetAllActivo(c);
        EmpresaModuloCol = EmpresaModuloCol.OrderBy(x => x.NuOrden);
        return Ok(EmpresaModuloCol);
    }


    [HttpGet]
    [Route("[action]")]
    public IActionResult GetAll([FromQuery] EmpresaModuloModel c)
    {
        var ModuloCol = _moduloService.GetAllActivo().ToList();
        var EmpresaModuloCol = _empresaModuloService.GetAll(c).ToList();
        foreach (var Item in ModuloCol)
        {
            var EmpresaModulo = EmpresaModuloCol.Find(x => x.CoModulo == Item.CoModulo);
            if (EmpresaModulo != null)
            {
                Item.CoEstReg = 1;
                Item.FlOnlyTypeMasterTable = EmpresaModulo.FlOnlyTypeMasterTable;
            }
            else
            {
                Item.CoEstReg = 0;
                Item.FlOnlyTypeMasterTable = 0;
            }
        }

        foreach (var Item in ModuloCol) Item.CoEmpresa = c.CoEmpresa;
        return Ok(ModuloCol);
    }
}