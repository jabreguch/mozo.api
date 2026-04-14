using Mozo.Model.Seguridad;
using Mozo.SeguridadData;

namespace Mozo.SeguridadBusiness;

public interface IPerfilBusiness
{
    Task<int> InsertAsync(PerfilModel c);
    Task UpdateAsync(PerfilModel c);
    Task UpdateStateAsync(PerfilModel c);
    Task DeleteByIdAsync(PerfilModel c);
    Task<PerfilModel?> SelByIdAsync(PerfilModel c);
    Task<IEnumerable<PerfilModel>> SelAllAsync(PerfilModel c);
    Task<IEnumerable<PerfilModel>> SelAllActiveAsync(PerfilModel c);
    Task<PerfilModel?> SelDefaultAsync(PerfilModel c);
}
public class PerfilBusiness : IPerfilBusiness
{
    private readonly IPerfilData _data;
    public PerfilBusiness(IPerfilData data)
    {
        _data = data;
    }
    public async Task<int> InsertAsync(PerfilModel c) => await _data.InsertAsync(c);
    public async Task UpdateAsync(PerfilModel c) => await _data.UpdateAsync(c);
    public async Task UpdateStateAsync(PerfilModel c) => await _data.UpdateStateAsync(c);
    public async Task DeleteByIdAsync(PerfilModel c) => await _data.DeleteByIdAsync(c);
    public async Task<IEnumerable<PerfilModel>> SelAllAsync(PerfilModel c) => await _data.SelAllAsync(c);
    public async Task<IEnumerable<PerfilModel>> SelAllActiveAsync(PerfilModel c) => await _data.SelAllActiveAsync(c);
    public async Task<PerfilModel?> SelByIdAsync(PerfilModel c) => await _data.SelByIdAsync(c);
    public async Task<PerfilModel?> SelDefaultAsync(PerfilModel c) => await _data.SelDefaultAsync(c);
}