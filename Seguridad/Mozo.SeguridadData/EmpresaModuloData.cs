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

public interface IEmpresaModuloData
{
    Task<int> InsertAsync(EmpresaModuloModel c);
    Task UpdateAsync(EmpresaModuloModel c);
    Task<IEnumerable<EmpresaModuloModel>> SelAllAsync(EmpresaModuloModel c);
    Task<IEnumerable<EmpresaModuloModel>> SelAllActiveAsync(EmpresaModuloModel c);
}
public partial class EmpresaModuloData : IEmpresaModuloData
{

    private readonly string _schema = EnuCommon.BdDefault.Schema.Seguridad;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public EmpresaModuloData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }

    public async Task<int> InsertAsync(EmpresaModuloModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoEmpresa",
           "CoModulo"
        );
        string sql = $"SELECT {_schema}.fn_empresamodulo_insert({args})";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }

    public async Task UpdateAsync(EmpresaModuloModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoEmpresa",
           "CoEmpresaModulo",
           "FlOnlyTypeMasterTable"
        );
        string sql = $"CALL {_schema}.usp_empresamodulo_update({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }


    public async Task<IEnumerable<EmpresaModuloModel>> SelAllAsync(EmpresaModuloModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoEmpresa"
        );
        string sql = $"SELECT * FROM {_schema}.fn_empresamodulo_sel_all({args})";
        return await _connection.QueryAsync<EmpresaModuloModel>(sql, pr);
    }

    public async Task<IEnumerable<EmpresaModuloModel>> SelAllActiveAsync(EmpresaModuloModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoEmpresa"
        );
        string sql = $"SELECT * FROM {_schema}.fn_empresamodulo_sel_all_active({args})";
        return await _connection.QueryAsync<EmpresaModuloModel>(sql, pr);
    }

}