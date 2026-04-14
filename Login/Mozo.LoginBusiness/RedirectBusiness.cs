using Mozo.LoginData;
using Mozo.Model.Seguridad;

namespace Mozo.LoginBusiness;

public interface IRedirectBusiness
{
    Task<int> InsertAsync(RedirectModel c);
    Task DeleteByIdAsync(RedirectModel c);
    Task<RedirectModel?> SelByIdAsync(RedirectModel c);
}
public class RedirectBusiness : IRedirectBusiness
{
    private readonly IRedirectData _data;
    public RedirectBusiness(IRedirectData data)
    {
        _data = data;
    }
    public async Task<int> InsertAsync(RedirectModel c) => await _data.InsertAsync(c);

    public async Task DeleteByIdAsync(RedirectModel c) => await _data.DeleteByIdAsync(c);

    public async Task<RedirectModel?> SelByIdAsync(RedirectModel c) => await _data.SelByIdAsync(c);
}