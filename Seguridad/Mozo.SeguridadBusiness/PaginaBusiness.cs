using Mozo.Model.Seguridad;
using Mozo.SeguridadData;

namespace Mozo.SeguridadBusiness;

public interface IPaginaBusiness
{
    Task<int> InsertAsync(PaginaModel c);
    Task UpdateAsync(PaginaModel c);
    Task UpdateStateAsync(PaginaModel c);
    Task DeleteByIdAsync(PaginaModel c);
    Task<PaginaModel?> SelByIdAsync(PaginaModel c);
    Task<IEnumerable<PaginaModel>> SelAllPaginaAsync(PaginaModel c);
    Task<IEnumerable<SubPaginaModel>> SelAllSubPaginaAsync(SubPaginaModel c);
    Task<IEnumerable<PaginaFlotanteModel>> SelAllPaginaFlotanteAsync(PaginaFlotanteModel c);
    Task<IEnumerable<ServicioWebModel>> SelAllServicioWebAsync(ServicioWebModel c);
    Task<IEnumerable<PaginaModel>> SelAllActivePaginaAsync(PaginaModel c);
    Task<IEnumerable<SubPaginaModel>> SelAllActiveSubPaginaAsync(SubPaginaModel c);
    Task<IEnumerable<PaginaFlotanteModel>> SelAllActivePaginaFlotanteAsync(PaginaFlotanteModel c);
    Task<IEnumerable<ServicioWebModel>> SelAllActiveServicioWebAsync(ServicioWebModel c);
}
public class PaginaBusiness : IPaginaBusiness
{
    private readonly IPaginaData _data;
    public PaginaBusiness(IPaginaData data)
    {
        _data = data;
    }
    public async Task<int> InsertAsync(PaginaModel c) => await _data.InsertAsync(c);
    public async Task UpdateAsync(PaginaModel c) => await _data.UpdateAsync(c);
    public async Task UpdateStateAsync(PaginaModel c) => await _data.UpdateStateAsync(c);
    public async Task DeleteByIdAsync(PaginaModel c) => await _data.DeleteByIdAsync(c);
    public async Task<PaginaModel?> SelByIdAsync(PaginaModel c)
    {
        PaginaModel? r = await _data.SelByIdAsync(c);
        return r;
    }
    public async Task<IEnumerable<PaginaModel>> SelAllPaginaAsync(PaginaModel c)
    {
        IEnumerable<PaginaModel> r = await _data.SelAllPaginaAsync(c);
        return r;
    }
    public async Task<IEnumerable<PaginaFlotanteModel>> SelAllPaginaFlotanteAsync(PaginaFlotanteModel c)
    {
        IEnumerable<PaginaFlotanteModel> r = await _data.SelAllPaginaFlotanteAsync(c);
        return r;
    }
    public async Task<IEnumerable<SubPaginaModel>> SelAllSubPaginaAsync(SubPaginaModel c)
    {
        IEnumerable<SubPaginaModel> r = await _data.SelAllSubPaginaAsync(c);
        return r;
    }
    public async Task<IEnumerable<ServicioWebModel>> SelAllServicioWebAsync(ServicioWebModel c)
    {
        IEnumerable<ServicioWebModel> r = await _data.SelAllServicioWebAsync(c);
        return r;
    }
    public async Task<IEnumerable<PaginaModel>> SelAllActivePaginaAsync(PaginaModel c)
    {
        IEnumerable<PaginaModel> r = await _data.SelAllActivePaginaAsync(c);
        return r;
    }
    public async Task<IEnumerable<PaginaFlotanteModel>> SelAllActivePaginaFlotanteAsync(PaginaFlotanteModel c)
    {
        IEnumerable<PaginaFlotanteModel> r = await _data.SelAllActivePaginaFlotanteAsync(c);
        return r;
    }
    public async Task<IEnumerable<SubPaginaModel>> SelAllActiveSubPaginaAsync(SubPaginaModel c)
    {
        IEnumerable<SubPaginaModel> r = await _data.SelAllActiveSubPaginaAsync(c);
        return r;
    }
    public async Task<IEnumerable<ServicioWebModel>> SelAllActiveServicioWebAsync(ServicioWebModel c)
    {
        IEnumerable<ServicioWebModel> r = await _data.SelAllActiveServicioWebAsync(c);
        return r;
    }
}