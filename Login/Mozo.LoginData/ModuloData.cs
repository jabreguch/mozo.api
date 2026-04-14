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

public interface IModuloData
{
    Task<ModuloModel?> SelByIdAsync(ModuloModel c);
    Task<IEnumerable<ModuloModel>> SelAllAsync();

}
public partial class ModuloData : IModuloData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Login;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public ModuloData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }

    public async Task<IEnumerable<ModuloModel>> SelAllAsync()
    {
        string sql = $"SELECT * FROM {_schema}.fn_modulo_sel_all()";
        return await _connection.QueryAsync<ModuloModel>(sql);
    }
    public async Task<ModuloModel?> SelByIdAsync(ModuloModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoModulo"
        );
        string sql = $"SELECT * FROM {_schema}.fn_modulo_sel_by_id({args})";
        return await _connection.QueryFirstOrDefaultAsync<ModuloModel>(sql, pr);
    }

}