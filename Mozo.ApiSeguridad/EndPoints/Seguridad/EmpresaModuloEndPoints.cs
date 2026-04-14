using Microsoft.AspNetCore.Http.HttpResults;

using Mozo.ApiSeguridad.Helper;
using Mozo.Model.Seguridad;
using Mozo.SeguridadBusiness;

namespace Mozo.Api.Seguridad;

///<summary>
/// EmpresaModuloEndPointsAdmin
///</summary>
///<history>
/// Create by Jonatan Abregu
///</history>
public static partial class EmpresaModuloEndPoints
{
    ///<summary>
    /// MapEmpresaModuloAdmin
    ///</summary>
    ///<history>
    /// Create by Jonatan Abregu
    ///</history>
    public static RouteGroupBuilder MapEmpresaModulo(this RouteGroupBuilder g)
    {
        g.DisableAntiforgery().RequireAuthorization();
        g.MapPost("/", InsertAsync);
        g.MapPut("/", UpdateAsync);
        g.MapGet("/", SelAllAsync);
        g.MapGet("/active", SelAllActiveAsync);
        return g;
    }

}
public static partial class EmpresaModuloEndPoints
{
    static async Task<Results<Created<int?>, ValidationProblem>> InsertAsync(EmpresaModuloModel m, IEmpresaModuloBusiness IEmpresaModulo, UserClaims user)
    {
        
        m.CoEmpresaModulo = await IEmpresaModulo.InsertAsync(m);
        return TypedResults.Created($"/{m.CoEmpresaModulo}", m.CoEmpresaModulo);
    }

    static async Task<Results<Ok<int?>, ValidationProblem>> UpdateAsync(EmpresaModuloModel c, IEmpresaModuloBusiness IEmpresaModulo, UserClaims user)
    {
        await IEmpresaModulo.UpdateAsync(c);
        return TypedResults.Ok(c.CoEmpresaModulo);
    }


    static async Task<Results<Ok<IEnumerable<EmpresaModuloModel>>, ValidationProblem>> SelAllActiveAsync(int coempresa, IEmpresaModuloBusiness IEmpresaModulo, UserClaims user)
    {
        EmpresaModuloModel c = new()
        {
            CoEmpresa = coempresa
        };
        IEnumerable<EmpresaModuloModel> r = await IEmpresaModulo.SelAllActiveAsync(c);
        r = r.OrderBy(x => x.NuOrden);
        return TypedResults.Ok(r);
    }

    static async Task<Results<Ok<IEnumerable<ModuloModel>>, ValidationProblem>> SelAllAsync(int coempresa, IModuloBusiness IModulo, IEmpresaModuloBusiness IEmpresaModulo, UserClaims user)
    {
        EmpresaModuloModel c = new()
        {
            CoEmpresa = coempresa
        };

        IEnumerable<ModuloModel> moduloLst = await IModulo.SelAllActiveAsync();

        List<EmpresaModuloModel> empresaModuloLst = (await IEmpresaModulo.SelAllAsync(c)).ToList();
        foreach (ModuloModel item in moduloLst)
        {
            EmpresaModuloModel? EmpresaModulo = empresaModuloLst.Find(x => x.CoModulo == item.CoModulo);
            if (EmpresaModulo != null)
                item.FlEstReg = 1;
            else
                item.FlEstReg = 0;
        }

        foreach (ModuloModel item in moduloLst) item.CoEmpresa = c.CoEmpresa;
        return TypedResults.Ok(moduloLst);

        //var ModuloCol = _moduloService.GetAllActivo().ToList();
        //var EmpresaModuloCol = _empresaModuloService.GetAll(c).ToList();
        //foreach (var Item in ModuloCol)
        //{
        //    var EmpresaModulo = EmpresaModuloCol.Find(x => x.CoModulo == Item.CoModulo);
        //    if (EmpresaModulo != null)
        //    {
        //        Item.CoEstReg = 1;
        //        Item.FlOnlyTypeMasterTable = EmpresaModulo.FlOnlyTypeMasterTable;
        //    }
        //    else
        //    {
        //        Item.CoEstReg = 0;
        //        Item.FlOnlyTypeMasterTable = 0;
        //    }
        //}

        //foreach (var Item in ModuloCol) Item.CoEmpresa = c.CoEmpresa;
        //return Ok(ModuloCol);
    }

}