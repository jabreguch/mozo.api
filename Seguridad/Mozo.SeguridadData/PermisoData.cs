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

public interface IPermisoData
{
    Task<int> InsertAsync(PermisoModel c);
    Task UpdateAsync(PermisoModel c);
    Task UpdateClaveAsync(PermisoModel c);
    Task UpdateStateAsync(PermisoModel c);
    Task<PermisoModel?> SelByIdAsync(PermisoModel c);
    //Task<IEnumerable<PermisoModel>> SelAllAsync(PermisoModel c);
    //Task<IEnumerable<PermisoModel>> SelAllActiveAsync(PermisoModel c);
}
public partial class PermisoData : IPermisoData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Seguridad;
    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public PermisoData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }
    public async Task<int> InsertAsync(PermisoModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoEmpresa",
           "CoPersona",
           "NoUsuario",
           "NoClave",
           "CoUsuCre"
        );
        string sql = $"SELECT {_schema}.fn_permiso_insert({args})";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }
    public async Task UpdateAsync(PermisoModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoEmpresa",
           "CoPermiso",
           "CoPersona",
           "NoUsuario",
           "NoClave",
           "CoUsuMod"
        );
        string sql = $"CALL {_schema}.usp_perfil_update({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task UpdateClaveAsync(PermisoModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoEmpresa",
           "CoPermiso",
           "NoClaveNuevo",
           "CoUsuMod"
        );
        string sql = $"CALL {_schema}.usp_permiso_update_clave({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task UpdateStateAsync(PermisoModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoEmpresa",
           "CoPermiso",
           "FlEstReg",
           "CoUsuMod"
        );
        string sql = $"CALL {_schema}.usp_permiso_update_state({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
   
    public async Task<PermisoModel?> SelByIdAsync(PermisoModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoEmpresa",
           "CoPersona"
        );
        string sql = $"SELECT * FROM {_schema}.fn_permiso_sel_by_id({args})";
        return await _connection.QueryFirstOrDefaultAsync<PermisoModel>(sql, pr);
    }
    
}