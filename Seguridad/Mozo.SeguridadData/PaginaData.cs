using Dapper;

using Mozo.Helper.Enu;
using Mozo.Helper.Global;
using Mozo.Model.Seguridad;

using System.Data;

///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	16/11/2018	Created
///</history>
namespace Mozo.SeguridadData;

public interface IPaginaData
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
public partial class PaginaData : IPaginaData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Seguridad;
    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public PaginaData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }

    public async Task<int> InsertAsync(PaginaModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo",
           "CoArea",
           "CoMenu",
           "CoPaginaPadre",
           "NoOpcion",
           "NuOrden",
           "CoTipoPagina",
           "TxDescripcion",
           "NoParametro",
           "NoControlador",
           "NoAccion",
           "CoUsuCre"
        );
        string sql = $"SELECT {_schema}.fn_pagina_insert({args})";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }
    public async Task UpdateAsync(PaginaModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoPagina",
           "CoArea",
           "NoOpcion",
           "NuOrden",
           "TxDescripcion",
           "NoParametro",
           "NoControlador",
           "NoAccion",
           "CoUsuMod"
        );
        string sql = $"CALL {_schema}.usp_pagina_update({args})";
        await _connection.ExecuteAsync(sql, pr);
    }
    public async Task UpdateStateAsync(PaginaModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoPagina",
           "FlEstReg",
           "CoUsuMod"
        );
        string sql = $"CALL {_schema}.usp_pagina_update_state({args})";
        await _connection.ExecuteAsync(sql, pr);
    }
    public async Task DeleteByIdAsync(PaginaModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoPagina",
           "CoUsuEli"
        );
        string sql = $"CALL {_schema}.usp_pagina_delete_by_id({args})";
        await _connection.ExecuteAsync(sql, pr);
    }
    public async Task<PaginaModel?> SelByIdAsync(PaginaModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo",
           "CoPagina"
        );
        string sql = $"SELECT * FROM {_schema}.fn_pagina_sel_by_id({args})";
        return await _connection.QueryFirstOrDefaultAsync<PaginaModel>(sql, pr);
    }
    public async Task<IEnumerable<PaginaModel>> SelAllPaginaAsync(PaginaModel c)
    {
        c.CoTipoPagina = EnuTipoGeneral.Seguridad.Pagina.Paginaa;
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo",
           "CoTipoPagina"
        );
        string sql = $"SELECT * FROM {_schema}.fn_pagina_sel_all({args})";
        return await _connection.QueryAsync<PaginaModel>(sql, pr);
    }
    public async Task<IEnumerable<SubPaginaModel>> SelAllSubPaginaAsync(SubPaginaModel c)
    {
        c.CoTipoPagina = EnuTipoGeneral.Seguridad.Pagina.SubPagina;
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo",
           "CoTipoPagina"
        );
        string sql = $"SELECT * FROM {_schema}.fn_pagina_sel_all({args})";
        return await _connection.QueryAsync<SubPaginaModel>(sql, pr);
    }
    public async Task<IEnumerable<PaginaFlotanteModel>> SelAllPaginaFlotanteAsync(PaginaFlotanteModel c)
    {
        c.CoTipoPagina = EnuTipoGeneral.Seguridad.Pagina.VistaFlotante;
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo",
           "CoTipoPagina"
        );
        string sql = $"SELECT * FROM {_schema}.fn_pagina_sel_all({args})";
        return await _connection.QueryAsync<PaginaFlotanteModel>(sql, pr);
    }
    public async Task<IEnumerable<ServicioWebModel>> SelAllServicioWebAsync(ServicioWebModel c)
    {
        c.CoTipoPagina = EnuTipoGeneral.Seguridad.Pagina.ServicioWeb;
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo",
           "CoTipoPagina"
        );
        string sql = $"SELECT * FROM {_schema}.fn_pagina_sel_all({args})";
        return await _connection.QueryAsync<ServicioWebModel>(sql, pr);
    }
    public async Task<IEnumerable<PaginaModel>> SelAllActivePaginaAsync(PaginaModel c)
    {
        c.CoTipoPagina = EnuTipoGeneral.Seguridad.Pagina.Paginaa;
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo",
           "CoTipoPagina"
        );
        string sql = $"SELECT * FROM {_schema}.fn_pagina_sel_all_active({args})";
        return await _connection.QueryAsync<PaginaModel>(sql, pr);
    }
    public async Task<IEnumerable<SubPaginaModel>> SelAllActiveSubPaginaAsync(SubPaginaModel c)
    {
        c.CoTipoPagina = EnuTipoGeneral.Seguridad.Pagina.SubPagina;
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo",
           "CoTipoPagina"
        );
        string sql = $"SELECT * FROM {_schema}.fn_pagina_sel_all_active({args})";
        return await _connection.QueryAsync<SubPaginaModel>(sql, pr);
    }
    public async Task<IEnumerable<PaginaFlotanteModel>> SelAllActivePaginaFlotanteAsync(PaginaFlotanteModel c)
    {
        c.CoTipoPagina = EnuTipoGeneral.Seguridad.Pagina.VistaFlotante;
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo",
           "CoTipoPagina"
        );
        string sql = $"SELECT * FROM {_schema}.fn_pagina_sel_all_active({args})";
        return await _connection.QueryAsync<PaginaFlotanteModel>(sql, pr);
    }
    public async Task<IEnumerable<ServicioWebModel>> SelAllActiveServicioWebAsync(ServicioWebModel c)
    {
        c.CoTipoPagina = EnuTipoGeneral.Seguridad.Pagina.ServicioWeb;
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo",
           "CoTipoPagina"
        );
        string sql = $"SELECT * FROM {_schema}.fn_pagina_sel_all_active({args})";
        return await _connection.QueryAsync<ServicioWebModel>(sql, pr);
    }

}