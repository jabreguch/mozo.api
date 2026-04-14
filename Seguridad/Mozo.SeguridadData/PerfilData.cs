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

public interface IPerfilData
{
    Task<int> InsertAsync(PerfilModel c);
    Task UpdateAsync(PerfilModel c);
    Task UpdateStateAsync(PerfilModel c);
    Task DeleteByIdAsync(PerfilModel c);
    Task<PerfilModel?> SelByIdAsync(PerfilModel c);
    Task<IEnumerable<PerfilModel>> SelAllAsync(PerfilModel c);
    Task<IEnumerable<PerfilModel>> SelAllActiveAsync(PerfilModel c);
    Task<PerfilModel?> SelDefaultAsync(PerfilModel c);
}
public partial class PerfilData : IPerfilData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Seguridad;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public PerfilData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }

    public async Task<int> InsertAsync(PerfilModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo",
           "NoPerfil",
           "FlDefault",
           "CoUsuCre"
        );
        string sql = $"SELECT {_schema}.fn_perfil_insert({args})";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }
    public async Task UpdateAsync(PerfilModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo",
           "NoPerfil",
           "FlDefault",
           "CoUsuMod"
        );
        string sql = $"CALL {_schema}.usp_perfil_update({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task UpdateStateAsync(PerfilModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo",
           "CoModulo",
           "FlEstReg",
           "CoUsuMod"
        );
        string sql = $"CALL {_schema}.usp_perfil_update_state({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task DeleteByIdAsync(PerfilModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoPerfil",
           "CoUsuEli"
        );
        string sql = $"CALL {_schema}.usp_perfil_delete_by_id({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task<IEnumerable<PerfilModel>> SelAllAsync(PerfilModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo",
           "CoPerfil"
        );
        string sql = $"SELECT * FROM {_schema}.fn_perfil_sel_all({args})";
        return await _connection.QueryAsync<PerfilModel>(sql, pr);
    }
    public async Task<IEnumerable<PerfilModel>> SelAllActiveAsync(PerfilModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo"
        );
        string sql = $"SELECT * FROM {_schema}.fn_perfil_sel_all_active({args})";
        return await _connection.QueryAsync<PerfilModel>(sql, pr);
    }
    public async Task<PerfilModel?> SelByIdAsync(PerfilModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo",
           "CoPerfil"
        );
        string sql = $"SELECT * FROM {_schema}.fn_perfil_sel_by_id({args})";
        return await _connection.QueryFirstOrDefaultAsync<PerfilModel>(sql, pr);
    }
    public async Task<PerfilModel?> SelDefaultAsync(PerfilModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo"
        );
        string sql = $"SELECT * FROM {_schema}.fn_perfil_sel_default({args})";
        return await _connection.QueryFirstOrDefaultAsync<PerfilModel>(sql, pr);
    }
}