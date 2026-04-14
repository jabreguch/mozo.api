using Mozo.Model.Seguridad;
using Mozo.SeguridadData;

namespace Mozo.SeguridadBusiness;

public interface IModuloUsuarioBusiness
{
    Task<int> InsertAsync(ModuloUsuarioModel c);
    Task UpdateAsync(ModuloUsuarioModel c);
    Task UpdateConfiguracionAsync(ModuloUsuarioModel c);
    Task DeleteByIdAsync(ModuloUsuarioModel c);
    Task<ModuloUsuarioModel?> SelByIdAsync(ModuloUsuarioModel c);
    Task<IEnumerable<ModuloUsuarioModel>> SelAllByModuloAsync(ModuloUsuarioModel c);
    Task<IEnumerable<ModuloUsuarioModel>> SelAllByPersonaAsync(ModuloUsuarioModel c);
    Task<ModuloUsuarioModel?> SelByModuloAndPersonaAsync(ModuloUsuarioModel c);
}
public class ModuloUsuarioBusiness : IModuloUsuarioBusiness
{
    private readonly IModuloUsuarioData _data;
    public ModuloUsuarioBusiness(IModuloUsuarioData data)
    {
        _data = data;
    }


    public async Task<int> InsertAsync(ModuloUsuarioModel c) => await _data.InsertAsync(c);

    public async Task UpdateAsync(ModuloUsuarioModel c) => await _data.UpdateAsync(c);

    public async Task UpdateConfiguracionAsync(ModuloUsuarioModel c) => await _data.UpdateConfiguracionAsync(c);

    public async Task DeleteByIdAsync(ModuloUsuarioModel c) => await _data.DeleteByIdAsync(c);

    public async Task<IEnumerable<ModuloUsuarioModel>> SelAllByModuloAsync(ModuloUsuarioModel c)
    {
        IEnumerable<ModuloUsuarioModel> r = await _data.SelAllByModuloAsync(c);
        return r;
    }

    public async Task<IEnumerable<ModuloUsuarioModel>> SelAllByPersonaAsync(ModuloUsuarioModel c)
    {
        IEnumerable<ModuloUsuarioModel> r = await _data.SelAllByPersonaAsync(c);
        return r;
    }

    public async Task<ModuloUsuarioModel?> SelByIdAsync(ModuloUsuarioModel c)
    {
        ModuloUsuarioModel? r = await _data.SelByIdAsync(c);
        return r;
    }

    public async Task<ModuloUsuarioModel?> SelByModuloAndPersonaAsync(ModuloUsuarioModel c)
    {
        ModuloUsuarioModel? r = await _data.SelByModuloAndPersonaAsync(c);
        return r;
    }

}