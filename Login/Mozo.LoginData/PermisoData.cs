using Dapper;

using Microsoft.Extensions.Logging;

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
    private readonly ILogger<PermisoData> _logger;
    public PermisoData(IDbConnection connection, UserContext user, ILogger<PermisoData> logger)
    {
        _connection = connection;
        _user = user;
        _logger = logger;
    } 
    public async Task<PermisoModel?> SelByUserAsync(PermisoFilterDto c)
    {
        if (c == null)
            throw new ArgumentNullException(nameof(c));

        DynamicParameters pr = new();
        Glo.AddParameter(pr, "NoUsuario", c.NoUsuario, DbType.String);
        Glo.AddParameter(pr, "NoClave", c.NoClave, DbType.String);

        string sql = $"SELECT * FROM {_schema}.fn_permiso_sel_by_user(@NoUsuario,@NoClave)";
        _logger.LogInformation("Consulta permiso por usuario - Usuario: {Usuario}", c.NoUsuario);
        return await _connection.QueryFirstOrDefaultAsync<PermisoModel>(sql, pr);
    }

    public async Task<PermisoModel?> SelByIdAsync(PermisoFilterDto c)
    {
        if (c == null)
            throw new ArgumentNullException(nameof(c));
        Glo.ValidateUserContext(_user);

        DynamicParameters pr = new();
        Glo.AddParameter(pr, "CoEmpresa", _user.CoEmpresa, DbType.Int32);
        Glo.AddParameter(pr, "CoPermiso", c.CoPermiso, DbType.Int32);
        string sql = $"SELECT * FROM {_schema}.fn_permiso_sel_by_id(@CoEmpresa,@CoPermiso)";
        _logger.LogInformation("Consulta permiso por id - Usuario: {Usuario}", _user.NoUsuario);
        return await _connection.QueryFirstOrDefaultAsync<PermisoModel>(sql, pr);
    }

    public async Task UpdateLanguageAsync(PermisoModel c)
    {
        if (c == null)
            throw new ArgumentNullException(nameof(c));
        Glo.ValidateUserContext(_user);

        DynamicParameters pr = new();
        Glo.AddParameter(pr, "CoEmpresa", _user.CoEmpresa, DbType.Int32);
        Glo.AddParameter(pr, "CoPermiso", c.CoPermiso, DbType.Int32);
        Glo.AddParameter(pr, "CoLang", c.CoLang, DbType.Int32);

        string sql = $"CALL {_schema}.usp_permiso_update_language(@CoEmpresa,@CoPermiso,@CoLang)";
        _logger.LogInformation("Actualiza idioma de permiso - Usuario: {Usuario}", _user.NoUsuario);
        await _connection.ExecuteScalarAsync(sql, pr);
    }

}
