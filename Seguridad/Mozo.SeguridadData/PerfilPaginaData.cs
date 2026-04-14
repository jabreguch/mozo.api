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

public interface IPerfilPaginaData
{
    Task<int> InsertAsync(PerfilPaginaModel c);
    Task DeleteByIdAsync(PerfilPaginaModel c);
    Task<IEnumerable<PerfilPaginaModel>> SelAllAsync(PerfilPaginaModel c);
}
public partial class PerfilPaginaData : IPerfilPaginaData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Seguridad;
    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public PerfilPaginaData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }

    public async Task<int> InsertAsync(PerfilPaginaModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo",
           "CoPerfil",
           "CoPagina",
           "CoPaginaPadre",
           "CoMenu",
           "CoUsuCre"
        );
        string sql = $"SELECT {_schema}.fn_perfilprivilegio_insert({args})";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }
    public async Task DeleteByIdAsync(PerfilPaginaModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoPerfilPrivilegio"
        );
        string sql = $"CALL {_schema}.usp_perfilprivilegio_delete_by_id({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task<IEnumerable<PerfilPaginaModel>> SelAllAsync(PerfilPaginaModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo",
           "CoPerfil"
        );
        string sql = $"SELECT * FROM {_schema}.fn_perfilprivilegio_sel_all({args})";
        return await _connection.QueryAsync<PerfilPaginaModel>(sql, pr);
    }

}