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

public interface IMenuData
{
    Task<int> InsertAsync(MenuModel c);
    Task UpdateAsync(MenuModel c);
    Task UpdateStateAsync(MenuModel c);
    Task DeleteByIdAsync(MenuModel c);
    Task<IEnumerable<MenuModel>> SelAllAsync(MenuModel c);
    Task<IEnumerable<MenuModel>> SelAllActiveAsync(MenuModel c);
    Task<MenuModel?> SelByIdAsync(MenuModel c);
}

public partial class MenuData : IMenuData

{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Seguridad;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public MenuData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }

    public async Task<int> InsertAsync(MenuModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo",
           "NuOrden",
           "NoMenu",
           "CoUsuCre"
        );
        string sql = $"SELECT {_schema}.fn_menu_insert({args})";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }
    public async Task UpdateAsync(MenuModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoMenu",
           "NuOrden",
           "NoMenu",
           "CoUsuMod"
        );

        string sql = $"CALL {_schema}.usp_menu_update({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task UpdateStateAsync(MenuModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoMenu",
           "FlEstReg",
           "CoUsuMod"
        );
        string sql = $"CALL {_schema}.usp_menu_update_state({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task DeleteByIdAsync(MenuModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoMenu",
           "CoUsuEli"
        );
        string sql = $"CALL {_schema}.usp_menu_delete_by_id({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task<IEnumerable<MenuModel>> SelAllAsync(MenuModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo"
        );
        string sql = $"SELECT * FROM {_schema}.fn_menu_sel_all({args})";
        return await _connection.QueryAsync<MenuModel>(sql, pr);
    }
    public async Task<IEnumerable<MenuModel>> SelAllActiveAsync(MenuModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo"
        );
        string sql = $"SELECT * FROM {_schema}.fn_menu_sel_all_active({args})";
        return await _connection.QueryAsync<MenuModel>(sql, pr);
    }
    public async Task<MenuModel?> SelByIdAsync(MenuModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
           "CoModulo",
           "CoMenu"
        );
        string sql = $"SELECT * FROM {_schema}.fn_menu_sel_by_id({args})";
        return await _connection.QueryFirstOrDefaultAsync<MenuModel>(sql, pr);
    }

}