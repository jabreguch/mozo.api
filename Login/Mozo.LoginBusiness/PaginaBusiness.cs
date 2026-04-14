using Mozo.LoginData;
using Mozo.Model.Seguridad;

namespace Mozo.LoginBusiness;

public interface IPaginaBusiness
{
    Task<IEnumerable<PaginaModel>> SelAllPaginaAsync(PaginaModel c);
    Task<IEnumerable<SubPaginaModel>> SelAllSubPaginaAsync(SubPaginaModel c);
    Task<IEnumerable<PaginaFlotanteModel>> SelAllPaginaFlotanteAsync(PaginaFlotanteModel c);
    Task<IEnumerable<ServicioWebModel>> SelAllServicioWebAsync(ServicioWebModel c);
}
public class PaginaBusiness : IPaginaBusiness
{
    private readonly IPaginaData _data;
    public PaginaBusiness(IPaginaData data)
    {
        _data = data;
    }


    public async Task<IEnumerable<PaginaModel>> SelAllPaginaAsync(PaginaModel c)
    {
        IEnumerable<PaginaModel> r = await _data.SelAllPaginaAsync(c);
        return r.OrderBy(x => x.NuOrden);
    }


    public async Task<IEnumerable<PaginaFlotanteModel>> SelAllPaginaFlotanteAsync(PaginaFlotanteModel c)
    {
        IEnumerable<PaginaFlotanteModel> r = await _data.SelAllPaginaFlotanteAsync(c);
        return r.OrderBy(x => x.NuOrden);
    }


    public async Task<IEnumerable<SubPaginaModel>> SelAllSubPaginaAsync(SubPaginaModel c)
    {
        IEnumerable<SubPaginaModel> r = await _data.SelAllSubPaginaAsync(c);
        return r.OrderBy(x => x.NuOrden);
    }

    public async Task<IEnumerable<ServicioWebModel>> SelAllServicioWebAsync(ServicioWebModel c)
    {
        IEnumerable<ServicioWebModel> r = await _data.SelAllServicioWebAsync(c);
        return r.OrderBy(x => x.NuOrden);
    }
}