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

public interface IModuloUsuarioData
{
    Task<int> InsertAsync(ModuloUsuarioModel c);
    Task UpdateAsync(ModuloUsuarioModel c);
    Task DeleteByIdAsync(ModuloUsuarioModel c);
    Task UpdateConfiguracionAsync(ModuloUsuarioModel c);
    Task<ModuloUsuarioModel?> SelByIdAsync(ModuloUsuarioModel c);
    Task<IEnumerable<ModuloUsuarioModel>> SelAllByModuloAsync(ModuloUsuarioModel c);
    Task<IEnumerable<ModuloUsuarioModel>> SelAllByPersonaAsync(ModuloUsuarioModel c);
    Task<ModuloUsuarioModel?> SelByModuloAndPersonaAsync(ModuloUsuarioModel c);
}
public partial class ModuloUsuarioData : IModuloUsuarioData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Seguridad;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public ModuloUsuarioData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }

    public async Task<int> InsertAsync(ModuloUsuarioModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoEmpresa",
           "CoModulo",
           "CoPersona",
           "CoPerfil",
           "FeExpiracion"
        );
        string sql = $"SELECT {_schema}.fn_modulousuario_insert({args})";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }
    public async Task UpdateAsync(ModuloUsuarioModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoEmpresa",
           "CoModuloUsuario",
           "CoModulo",
           "CoPersona",
           "CoPerfil",
           "FeExpiracion"
        );
        string sql = $"CALL {_schema}.usp_modulousuario_update({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task UpdateConfiguracionAsync(ModuloUsuarioModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoEmpresa",
           "CoModuloUsuario",
           "TxConfiguracion"
        );
        string sql = $"CALL {_schema}.usp_modulousuario_update_configuracion({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task DeleteByIdAsync(ModuloUsuarioModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoEmpresa",
           "CoModuloUsuario"
        );
        string sql = $"CALL {_schema}.usp_modulousuario_delete_by_id({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task<IEnumerable<ModuloUsuarioModel>> SelAllByModuloAsync(ModuloUsuarioModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoEmpresa",
           "CoModulo"
        );
        string sql = $"SELECT * FROM {_schema}.fn_modulousuario_sel_all_by_modulo({args})";
        return await _connection.QueryAsync<ModuloUsuarioModel>(sql, pr);
    }
    public async Task<IEnumerable<ModuloUsuarioModel>> SelAllByPersonaAsync(ModuloUsuarioModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoEmpresa",
           "CoPersona"
        );
        string sql = $"SELECT * FROM {_schema}.fn_modulousuario_sel_all_by_persona({args})";
        return await _connection.QueryAsync<ModuloUsuarioModel>(sql, pr);
    }
    public async Task<ModuloUsuarioModel?> SelByIdAsync(ModuloUsuarioModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoEmpresa",
           "CoModuloUsuario"
        );
        string sql = $"SELECT * FROM {_schema}.fn_modulousuario_sel_by_id({args})";
        return await _connection.QueryFirstOrDefaultAsync<ModuloUsuarioModel>(sql, pr);
    }
    public async Task<ModuloUsuarioModel?> SelByModuloAndPersonaAsync(ModuloUsuarioModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoEmpresa",
           "CoModulo",
           "CoPersona"
        );
        string sql = $"SELECT * FROM {_schema}.fn_modulousuario_sel_by_modulo_and_persona({args})";
        return await _connection.QueryFirstOrDefaultAsync<ModuloUsuarioModel>(sql, pr);
    }

}