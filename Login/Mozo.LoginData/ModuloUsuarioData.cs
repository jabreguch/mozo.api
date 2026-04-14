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

public interface IModuloUsuarioData
{
    Task<ModuloUsuarioModel?> SelByIdAsync(ModuloUsuarioModel c);
    Task<ModuloUsuarioModel?> SelByModuloAsync(ModuloUsuarioModel c);
    Task<IEnumerable<ModuloUsuarioModel>> SelAllByPersonaAsync(ModuloUsuarioModel c);

}
public partial class ModuloUsuarioData : IModuloUsuarioData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Login;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public ModuloUsuarioData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }
    public async Task<ModuloUsuarioModel?> SelByIdAsync(ModuloUsuarioModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoModuloUsuario"
        );
        string sql = $"SELECT * FROM {_schema}.fn_modulousuario_sel_by_id({args})";
        return await _connection.QueryFirstOrDefaultAsync<ModuloUsuarioModel>(sql, c);
    }
    public async Task<ModuloUsuarioModel?> SelByModuloAsync(ModuloUsuarioModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoModuloUsuario",
            "CoModulo"
        );
        string sql = $"SELECT * FROM {_schema}.fn_modulousuario_sel_by_modulo({args})";
        return await _connection.QueryFirstOrDefaultAsync<ModuloUsuarioModel>(sql, pr);
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



}