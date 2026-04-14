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

public interface IModuloData
{
    Task<int> InsertAsync(ModuloModel c);
    Task UpdateAsync(ModuloModel c);
    Task UpdateStateAsync(ModuloModel c);
    Task DeleteByIdAsync(ModuloModel c);
    Task<IEnumerable<ModuloModel>> SelAllAsync(ModuloModel c);
    Task<IEnumerable<ModuloModel>> SelAllActiveAsync();
    Task<IEnumerable<ModuloModel>> SelAllActiveAreaAsync();
    Task<ModuloModel?> SelByIdAsync(ModuloModel c);
}
public partial class ModuloData : IModuloData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Seguridad;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public ModuloData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }
    public async Task<int> InsertAsync(ModuloModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,           
            "NoModulo",
            "NoArea",
            "NoModuloDescripcion",
            "NuOrden",
            "NoIcono",
            "FlArea",
            "CoUsuCre"
        );
        string sql = $"SELECT {_schema}.fn_modulo_insert({args})";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }
    public async Task UpdateAsync(ModuloModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoModulo",
            "NoModulo",
            "NoArea",
            "NoModuloDescripcion",
            "NuOrden",
            "NoIcono",
            "FlArea",
            "CoUsuMod"         
        );
        string sql = $"CALL {_schema}.usp_modulo_update({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }

    public async Task UpdateStateAsync(ModuloModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo",
           "FlEstReg",         
           "CoUsuMod"
       );

        string sql = $"CALL {_schema}.usp_modulo_update_state({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task DeleteByIdAsync(ModuloModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoModulo",           
            "CoUsuEli"
        );
        string sql = $"CALL {_schema}.usp_modulo_delete_by_id({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task<IEnumerable<ModuloModel>> SelAllAsync(ModuloModel c)
    {
        string sql = $"SELECT * FROM {_schema}.fn_modulo_sel_all(@CoModulo)";

        return await _connection.QueryAsync<ModuloModel>(sql, c);
    }
    public async Task<IEnumerable<ModuloModel>> SelAllActiveAsync()
    {
        string sql = $"SELECT * FROM {_schema}.fn_modulo_sel_all_active()";
        return await _connection.QueryAsync<ModuloModel>(sql);
    }
    public async Task<IEnumerable<ModuloModel>> SelAllActiveAreaAsync()
    {
        string sql = $"SELECT * FROM {_schema}.fn_modulo_sel_all_active_area()";
        return await _connection.QueryAsync<ModuloModel>(sql);
    }
    public async Task<ModuloModel?> SelByIdAsync(ModuloModel c)
    {
        string sql = $"SELECT * FROM {_schema}.fn_modulo_sel_by_id(@CoModulo)";
        return await _connection.QueryFirstOrDefaultAsync<ModuloModel>(sql, c);
    }

}