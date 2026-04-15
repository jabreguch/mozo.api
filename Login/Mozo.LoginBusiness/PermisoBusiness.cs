using Mozo.Helper.Global;
using Mozo.LoginData;
using Mozo.Model.Seguridad;

namespace Mozo.LoginBusiness;

public interface IPermisoBusiness
{    
    Task<PermisoModel?> SelByUserAsync(PermisoFilterDto c);
    Task<PermisoModel?> SelByIdAsync(PermisoFilterDto c);
    Task UpdateLanguageAsync(PermisoModel c);
}
public class PermisoBusiness : IPermisoBusiness
{
    private readonly IPermisoData _data;
    private readonly UserContext _user;
    public PermisoBusiness(IPermisoData data, UserContext user)
    {
        _data = data;
        _user = user;
    }

    public async Task<PermisoModel?> SelByIdAsync(PermisoFilterDto c)
    {
        if (c == null)
            throw new ArgumentNullException(nameof(c));
        Glo.ValidateUserContext(_user);

        return await _data.SelByIdAsync(c);
    }

    public async Task<PermisoModel?> SelByUserAsync(PermisoFilterDto c)
    {
        if (c == null)
            throw new ArgumentNullException(nameof(c));

        return await _data.SelByUserAsync(c);
    }
    public async Task UpdateLanguageAsync(PermisoModel c)
    {
        if (c == null)
            throw new ArgumentNullException(nameof(c));
        Glo.ValidateUserContext(_user);

        await _data.UpdateLanguageAsync(c);
    }

}
