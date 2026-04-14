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

public interface IRedirectData
{
    Task<int> InsertAsync(RedirectModel c);
    Task DeleteByIdAsync(RedirectModel c);
    Task<RedirectModel?> SelByIdAsync(RedirectModel c);
}
public partial class RedirectData : IRedirectData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Login;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public RedirectData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }
    public async Task<int> InsertAsync(RedirectModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoPermiso",
            "CoPersona",
            "CoModulo",
            "FeRedirect"
        );
        string sql = $"SELECT {_schema}.fn_redirect_insert({args})";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }
    public async Task DeleteByIdAsync(RedirectModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoRedirect"
        );
        string sql = $"SELECT {_schema}.usp_redirect_delete_by_id({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task<RedirectModel?> SelByIdAsync(RedirectModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoRedirect"
        );
        string sql = $"SELECT * FROM {_schema}.fn_redirect_sel_by_id({args})";
        return await _connection.QueryFirstOrDefaultAsync<RedirectModel>(sql, pr);
    }

}