using Dapper;

using Mozo.Helper.Enu;
using Mozo.Helper.Global;
using Mozo.Model.Maestro;

using System.Data;

///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	26/04/2022	Created
///</history>
namespace Mozo.MaestroData;

public interface ITipoGeneralData
{
    Task<IEnumerable<TipoGeneralModel>> SelAllActiveAsync(TipoGeneralModel c);
    Task<IEnumerable<TipoGeneralModel>> SelAllActiveByModuloAsync(TipoGeneralModel c);
    Task<TipoGeneralModel?> SelByIdAsync(TipoGeneralModel c);
}
public partial class TipoGeneralData : ITipoGeneralData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Maestro;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public TipoGeneralData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }
    public async Task<IEnumerable<TipoGeneralModel>> SelAllActiveAsync(TipoGeneralModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoGrupo"
        );
        string sql = $"SELECT * FROM {_schema}.fn_tipogeneral_sel_all_active({args})";
        return await _connection.QueryAsync<TipoGeneralModel>(sql, pr);
    }
    public async Task<IEnumerable<TipoGeneralModel>> SelAllActiveByModuloAsync(TipoGeneralModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoModulo",
            "CoGrupo"
        );
        string sql = $"SELECT * FROM {_schema}.fn_tipogeneral_sel_all_active_by_modulo({args})";
        return await _connection.QueryAsync<TipoGeneralModel>(sql, pr);
    }
    public async Task<TipoGeneralModel?> SelByIdAsync(TipoGeneralModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoGrupo",
            "CoTipo"
        );
        string sql = $"SELECT * FROM {_schema}.fn_tipogeneral_sel_by_id({args})";
        return await _connection.QueryFirstOrDefaultAsync<TipoGeneralModel>(sql, pr);
    }

}


