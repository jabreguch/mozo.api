using Mozo.Model.Seguridad;
using Mozo.SeguridadData;

namespace Mozo.SeguridadBusiness;

public interface IModuloBusiness
{
    Task<int> InsertAsync(ModuloModel c);
    Task UpdateAsync(ModuloModel c);
    Task UpdateStateAsync(ModuloModel c);
    Task DeleteByIdAsync(ModuloModel c);
    Task<IEnumerable<ModuloModel>> SelAllAsync(ModuloModel c);
    Task<IEnumerable<ModuloModel>> SelAllActiveAsync();
    Task<IEnumerable<ModuloModel>> SelAllActiveAreaAsync();
    Task<ModuloModel?> SelByIdAsync(ModuloModel c);
}
public class ModuloBusiness : IModuloBusiness
{
    private readonly IModuloData _data;
    public ModuloBusiness(IModuloData data)
    {
        _data = data;
    }


    public async Task<int> InsertAsync(ModuloModel c) => await _data.InsertAsync(c);
    public async Task UpdateAsync(ModuloModel c) => await _data.UpdateAsync(c);
    public async Task UpdateStateAsync(ModuloModel c) => await _data.UpdateStateAsync(c);
    public async Task DeleteByIdAsync(ModuloModel c) => await _data.DeleteByIdAsync(c);
    public async Task<IEnumerable<ModuloModel>> SelAllAsync(ModuloModel c)
    {
        IEnumerable<ModuloModel> r = await _data.SelAllAsync(c);
        return r.OrderBy(s => s.NuOrden);
    }
    public async Task<IEnumerable<ModuloModel>> SelAllActiveAsync()
    {
        IEnumerable<ModuloModel> r = await _data.SelAllActiveAsync();
        return r.OrderBy(s => s.NuOrden);
    }
    public async Task<IEnumerable<ModuloModel>> SelAllActiveAreaAsync()
    {
        IEnumerable<ModuloModel> r = await _data.SelAllActiveAreaAsync();
        return r.OrderBy(s => s.NuOrden);
    }
    public async Task<ModuloModel?> SelByIdAsync(ModuloModel c) => await _data.SelByIdAsync(c);



}