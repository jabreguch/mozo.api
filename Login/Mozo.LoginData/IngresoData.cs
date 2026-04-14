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

public interface IIngresoData
{
    Task<int> InsertAsync(IngresoModel c);    
    Task UpdateCloseTokenAsync(IngresoModel c);
    Task<IngresoModel?> SelByIdAsync(IngresoModel c);

}
public partial class IngresoData : IIngresoData
{

    private readonly string _schema = EnuCommon.BdDefault.Schema.Login;
    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public IngresoData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }
    public async Task<int> InsertAsync(IngresoModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoPermiso",
            "CoPersona",          
            "NoIp",
            "NoRefreshToken"
        );
        string sql = $"SELECT {_schema}.fn_ingreso_insert({args})";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }
    public async Task<IngresoModel?> SelByIdAsync(IngresoModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoIngreso"
        );
        string sql = $"SELECT * FROM {_schema}.fn_ingreso_sel_by_id({args})";
        return await _connection.QueryFirstOrDefaultAsync<IngresoModel>(sql, pr);
    }

    public async Task UpdateCloseTokenAsync(IngresoModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoIngreso",
            "NoRefreshTokenPrevious"
        );
        string sql = $"CALL {_schema}.usp_ingreso_update_close_token({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }

}