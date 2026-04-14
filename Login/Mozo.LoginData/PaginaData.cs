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
namespace Mozo.LoginData;

public interface IPaginaData
{
    Task<IEnumerable<PaginaModel>> SelAllPaginaAsync(PaginaModel c);
    Task<IEnumerable<SubPaginaModel>> SelAllSubPaginaAsync(SubPaginaModel c);
    Task<IEnumerable<PaginaFlotanteModel>> SelAllPaginaFlotanteAsync(PaginaFlotanteModel c);
    Task<IEnumerable<ServicioWebModel>> SelAllServicioWebAsync(ServicioWebModel c);
}
public partial class PaginaData : IPaginaData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Login;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public PaginaData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }
    public async Task<IEnumerable<PaginaModel>> SelAllPaginaAsync(PaginaModel c)
    {
        c.CoTipoPagina = EnuTipoGeneral.Seguridad.Pagina.Paginaa;
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoModulo",
            "CoPersona"
        );
        string sql = $"SELECT * FROM {_schema}.fn_pagina_sel_all({args})";
        return await _connection.QueryAsync<PaginaModel>(sql, pr);
    }
    public async Task<IEnumerable<SubPaginaModel>> SelAllSubPaginaAsync(SubPaginaModel c)
    {
        c.CoTipoPagina = EnuTipoGeneral.Seguridad.Pagina.SubPagina;
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoModulo",
            "CoPersona"
        );
        string sql = $"SELECT * FROM {_schema}.fn_subpagina_sel_all({args})";
        return await _connection.QueryAsync<SubPaginaModel>(sql, pr);
    }
    public async Task<IEnumerable<PaginaFlotanteModel>> SelAllPaginaFlotanteAsync(PaginaFlotanteModel c)
    {
        c.CoTipoPagina = EnuTipoGeneral.Seguridad.Pagina.VistaFlotante;
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoModulo",
            "CoPersona"
        );
        string sql = $"SELECT * FROM {_schema}.fn_paginaflotante_sel_all({args})";
        return await _connection.QueryAsync<PaginaFlotanteModel>(sql, pr);
    }
    public async Task<IEnumerable<ServicioWebModel>> SelAllServicioWebAsync(ServicioWebModel c)
    {
        c.CoTipoPagina = EnuTipoGeneral.Seguridad.Pagina.ServicioWeb;
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoModulo",
            "CoPersona"
        );
        string sql = $"SELECT * FROM {_schema}.fn_servicioweb_sel_all({args})";
        return await _connection.QueryAsync<ServicioWebModel>(sql, pr);
    }

}