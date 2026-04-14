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

public interface IEmpresaData
{
    Task<int> InsertAsync(EmpresaModel c);
    Task UpdateAsync(EmpresaModel c);
    Task UpdateStateAsync(EmpresaModel c);
    Task DeleteByIdAsync(EmpresaModel c);
    Task<IEnumerable<EmpresaModel>> SelAllAsync(EmpresaModel c);
    Task<IEnumerable<EmpresaModel>> SelAllActiveAsync();
    Task<EmpresaModel?> SelByIdAsync(EmpresaModel c);
}

public partial class EmpresaData : IEmpresaData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Seguridad;
    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public EmpresaData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }

    public async Task<int> InsertAsync(EmpresaModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, null, _user.CoPersona,
           "NoEmpresa",
           "NoEmpresacorto",
           "NoMision",
           "NoVision",
           "TxQuiensoy",
           "NoSeo",
           "CoPais",
           "CoDocumentoIdentidad",
           "NuDocumentoFiscal",
           "CoMoneda",
           "CoUsuCre"
        );
        string sql = @$"SELECT {_schema}.fn_empresa_insert({args})";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }
    public async Task UpdateAsync(EmpresaModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, null, _user.CoPersona,
           "CoEmpresa",
           "NoEmpresa",
           "NoEmpresacorto",
           "NoMision",
           "NoVision",
           "TxQuiensoy",
           "NoSeo",
           "CoPais",
           "CoDocumentoIdentidad",
           "NuDocumentoFiscal",
           "CoMoneda",
           "CoUsuCre"
        );
        string sql = @$"CALL {_schema}.usp_empresa_update({args})";
        await _connection.ExecuteAsync(sql, pr);
    }
    public async Task UpdateStateAsync(EmpresaModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, null, _user.CoPersona,
           "CoEmpresa",
           "FlEstReg",
           "CoUsuMod"
        );
        string sql = @$"CALL {_schema}.usp_empresa_update_state({args})";
        await _connection.ExecuteAsync(sql, pr);
    }
    public async Task DeleteByIdAsync(EmpresaModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, null, _user.CoPersona,
           "CoEmpresa",
           "CoUsuEli"
        );
        string sql = $"CALL {_schema}.usp_empresa_delete_by_id({args})";
        await _connection.ExecuteAsync(sql, pr);
    }
    public async Task<IEnumerable<EmpresaModel>> SelAllAsync(EmpresaModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, null, _user.CoPersona,
           "CoEmpresa",
           "NoInputSearch",
           "PageSize",
           "PageIndex"
        );
        string sql = $"SELECT * FROM {_schema}.fn_empresa_sel_all({args})";
        return await _connection.QueryAsync<EmpresaModel>(sql, pr);
    }
    public async Task<IEnumerable<EmpresaModel>> SelAllActiveAsync()
    {
        string sql = $"SELECT * FROM {_schema}.fn_empresa_sel_all_active()";
        return await _connection.QueryAsync<EmpresaModel>(sql);
    }
    public async Task<EmpresaModel?> SelByIdAsync(EmpresaModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, null, _user.CoPersona,
           "CoEmpresa"
        );
        string sql = $"SELECT * FROM {_schema}.fn_empresa_sel_by_id({args})";
        return await _connection.QueryFirstOrDefaultAsync<EmpresaModel>(sql, pr);
    }

}