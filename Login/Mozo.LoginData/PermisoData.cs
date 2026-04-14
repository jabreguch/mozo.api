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

public interface IPermisoData
{
    Task<PermisoModel?> SelByUserAsync(PermisoFilterDto c);
    Task<PermisoModel?> SelByIdAsync(PermisoFilterDto c);
    Task UpdateLanguageAsync(PermisoModel c);
}
public partial class PermisoData : IPermisoData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Login;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public PermisoData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    } 
    public async Task<PermisoModel?> SelByUserAsync(PermisoFilterDto c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, null, null,
            "NoUsuario",
            "NoClave"
        );
        string sql = $"SELECT * FROM {_schema}.fn_permiso_sel_by_user({args})";
        return await _connection.QueryFirstOrDefaultAsync<PermisoModel>(sql, pr);
    }

    public async Task<PermisoModel?> SelByIdAsync(PermisoFilterDto c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoPermiso"
        );
        string sql = $"SELECT * FROM {_schema}.fn_permiso_sel_by_id({args})";
        return await _connection.QueryFirstOrDefaultAsync<PermisoModel>(sql, pr);
    }

    public async Task UpdateLanguageAsync(PermisoModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoPermiso",
            "CoLang"
        );
        string sql = $"CALL {_schema}.usp_permiso_update_language({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }

}