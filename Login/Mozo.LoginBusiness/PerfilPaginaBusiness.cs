using Mozo.LoginData;
using Mozo.Model.Seguridad;

namespace Mozo.LoginBusiness;

public interface IPerfilPaginaBusiness
{
    Task<IEnumerable<PerfilPaginaModel>> SelAllByModuloAndPerfilAsync(PerfilPaginaModel c);
}
public class PerfilPaginaBusiness : IPerfilPaginaBusiness
{
    private readonly IPerfilPaginaData _data;
    public PerfilPaginaBusiness(IPerfilPaginaData data)
    {
        _data = data;
    }


    public async Task<IEnumerable<PerfilPaginaModel>> SelAllByModuloAndPerfilAsync(PerfilPaginaModel c)
    {
        IEnumerable<PerfilPaginaModel> r = await _data.SelAllByModuloAndPerfilAsync(c);
        return r;
    }
}