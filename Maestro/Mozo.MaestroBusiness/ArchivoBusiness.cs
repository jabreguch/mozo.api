using Mozo.MaestroData;
using Mozo.Model.Maestro;

namespace Mozo.MaestroBusiness;

public interface IArchivoBusiness
{
    Task<int> InsertAsync(ArchivoModel c);
    Task UpdateAsync(ArchivoModel c);
    Task DeleteByIdAsync(ArchivoModel c);
    Task<ArchivoModel?> SelByIdAsync(ArchivoModel c);
    Task<IEnumerable<ArchivoModel>> SelAllAsync(ArchivoModel c);
    Task<IEnumerable<ArchivoModel>> SelAllActiveAsync(ArchivoModel c);
}
public class ArchivoBusiness : IArchivoBusiness
{
    private readonly IArchivoData _data;
    public ArchivoBusiness(IArchivoData data)
    {
        _data = data;
    }
    public async Task<int> InsertAsync(ArchivoModel c) => await _data.InsertAsync(c);
    public async Task UpdateAsync(ArchivoModel c) => await _data.UpdateAsync(c);
    public async Task DeleteByIdAsync(ArchivoModel c) => await _data.DeleteByIdAsync(c);
    public async Task<IEnumerable<ArchivoModel>> SelAllAsync(ArchivoModel c) => await _data.SelAllAsync(c);
    public async Task<IEnumerable<ArchivoModel>> SelAllActiveAsync(ArchivoModel c) => await _data.SelAllActiveAsync(c);
    public async Task<ArchivoModel?> SelByIdAsync(ArchivoModel c) => await _data.SelByIdAsync(c);

}
