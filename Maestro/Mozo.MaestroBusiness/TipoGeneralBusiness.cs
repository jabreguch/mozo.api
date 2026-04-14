using Mozo.MaestroData;
using Mozo.Model.Maestro;

namespace Mozo.MaestroBusiness;

public interface ITipoGeneralBusiness
{
    Task<IEnumerable<TipoGeneralModel>> SelAllActiveAsync(TipoGeneralModel c);
    Task<IEnumerable<TipoGeneralModel>> SelAllActiveByModuloAsync(TipoGeneralModel c);
    Task<TipoGeneralModel?> SelByIdAsync(TipoGeneralModel c);
}
public class TipoGeneralBusiness : ITipoGeneralBusiness
{
    private readonly ITipoGeneralData _data;
    public TipoGeneralBusiness(ITipoGeneralData data)
    {
        _data = data;
    }
    public async Task<IEnumerable<TipoGeneralModel>> SelAllActiveAsync(TipoGeneralModel c) => await _data.SelAllActiveAsync(c);
    public async Task<IEnumerable<TipoGeneralModel>> SelAllActiveByModuloAsync(TipoGeneralModel c) => await _data.SelAllActiveByModuloAsync(c);
    public async Task<TipoGeneralModel?> SelByIdAsync(TipoGeneralModel c) => await _data.SelByIdAsync(c);

}
