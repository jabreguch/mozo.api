using Mozo.Model.Seguridad;
using Mozo.SeguridadData;

namespace Mozo.SeguridadBusiness;

public interface IEmpresaModuloBusiness
{
    Task<int> InsertAsync(EmpresaModuloModel c);
    Task UpdateAsync(EmpresaModuloModel c);
    Task<IEnumerable<EmpresaModuloModel>> SelAllAsync(EmpresaModuloModel c);
    Task<IEnumerable<EmpresaModuloModel>> SelAllActiveAsync(EmpresaModuloModel c);
}
public class EmpresaModuloBusiness : IEmpresaModuloBusiness
{
    private readonly IEmpresaModuloData _data;
    public EmpresaModuloBusiness(IEmpresaModuloData data)
    {
        _data = data;
    }

    public async Task<int> InsertAsync(EmpresaModuloModel c) => await _data.InsertAsync(c);
    public async Task UpdateAsync(EmpresaModuloModel c) => await _data.UpdateAsync(c);
    public async Task<IEnumerable<EmpresaModuloModel>> SelAllAsync(EmpresaModuloModel c)
    {
        IEnumerable<EmpresaModuloModel> r = await _data.SelAllAsync(c);
        return r.OrderBy(s => s.NuOrden);
    }
    public async Task<IEnumerable<EmpresaModuloModel>> SelAllActiveAsync(EmpresaModuloModel c)
    {
        IEnumerable<EmpresaModuloModel> r = await _data.SelAllActiveAsync(c);
        return r.OrderBy(s => s.NuOrden);
    }
}