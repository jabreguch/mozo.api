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

public interface IRedSocialData
{
    Task<int> InsertAsync(RedSocialModel c, IDbTransaction? tran = null);
    Task UpdateAsync(RedSocialModel c);
    Task DeleteByIdAsync(RedSocialModel c);
    Task<RedSocialModel?> SelByIdAsync(RedSocialModel c);
    Task<IEnumerable<RedSocialModel>> SelAllAsync(RedSocialModel c);
    Task<IEnumerable<RedSocialModel>> SelAllActiveAsync(RedSocialModel c);
}
public partial class RedSocialData : IRedSocialData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Maestro;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public RedSocialData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }


    public async Task<int> InsertAsync(RedSocialModel c, IDbTransaction? tran = null)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "FlPersona",
            "CoPersona",
            "CoTipoRedSocial",
            "CoTipoUrl",
            "CoEtiqueta",
            "NoRedSocial",
            "FlWhatsapp",
            "CoUsuCre"
        );
        string sql = $"SELECT {_schema}.fn_redsocial_insert({args})";

        if (tran == null)
            return await _connection.ExecuteScalarAsync<int>(sql, pr);
        else
            return await _connection.ExecuteScalarAsync<int>(sql, pr, tran);
    }
    public async Task UpdateAsync(RedSocialModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoRedSocial",
            "FlPersona",
            "CoTipoRedSocial",
            "CoTipoUrl",
            "CoEtiqueta",
            "NoRedSocial",
            "FlWhatsapp",
            "CoUsuMod"
        );
        string sql = $"CALL {_schema}.usp_redsocial_update({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task DeleteByIdAsync(RedSocialModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoRedSocial",
            "CoUsuEli"
        );
        string sql = $"CALL {_schema}.usp_redsocial_delete_by_id({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task<IEnumerable<RedSocialModel>> SelAllAsync(RedSocialModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "FlPersona",
            "CoTipoRedSocial",
            "CoPersona",
            "CoRedSocial"
        );
        string sql = $"SELECT * FROM {_schema}.fn_redsocial_sel_all({args})";
        return await _connection.QueryAsync<RedSocialModel>(sql, pr);
    }
    public async Task<IEnumerable<RedSocialModel>> SelAllActiveAsync(RedSocialModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "FlPersona",
            "CoTipoRedSocial",
            "CoPersona"
        );
        string sql = $"SELECT * FROM {_schema}.fn_redsocial_sel_all_active({args})";
        return await _connection.QueryAsync<RedSocialModel>(sql, pr);
    }
    public async Task<RedSocialModel?> SelByIdAsync(RedSocialModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoRedSocial"
        );
        string sql = $"SELECT * FROM {_schema}.fn_redsocial_sel_by_id({args})";
        return await _connection.QueryFirstOrDefaultAsync<RedSocialModel>(sql, pr);
    }

}
