using Mozo.MaestroData;
using Mozo.Model.Maestro;

namespace Mozo.MaestroBusiness;

public interface IRedSocialBusiness
{
    Task<int> InsertAsync(RedSocialModel c);
    Task UpdateAsync(RedSocialModel c);
    Task DeleteByIdAsync(RedSocialModel c);
    Task<RedSocialModel?> SelByIdAsync(RedSocialModel c);
    Task<IEnumerable<RedSocialModel>> SelAllAsync(RedSocialModel c);
    Task<IEnumerable<RedSocialModel>> SelAllActiveAsync(RedSocialModel c);
}

public class RedSocialBusiness : IRedSocialBusiness
{
    private readonly IRedSocialData _data;
    public RedSocialBusiness(IRedSocialData data)
    {
        _data = data;
    }
    public async Task<int> InsertAsync(RedSocialModel c) => await _data.InsertAsync(c);
    public async Task UpdateAsync(RedSocialModel c) => await _data.UpdateAsync(c);
    public async Task DeleteByIdAsync(RedSocialModel c) => await _data.DeleteByIdAsync(c);
    public async Task<IEnumerable<RedSocialModel>> SelAllAsync(RedSocialModel c) => await _data.SelAllAsync(c);
    public async Task<IEnumerable<RedSocialModel>> SelAllActiveAsync(RedSocialModel c) => await _data.SelAllActiveAsync(c);
    public async Task<RedSocialModel?> SelByIdAsync(RedSocialModel c) => await _data.SelByIdAsync(c);

}
