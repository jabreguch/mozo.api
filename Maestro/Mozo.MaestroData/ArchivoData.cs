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

public interface IArchivoData
{
    Task<int> InsertAsync(ArchivoModel c);
    Task UpdateAsync(ArchivoModel c);
    Task DeleteByIdAsync(ArchivoModel c);
    Task<ArchivoModel?> SelByIdAsync(ArchivoModel c);
    Task<IEnumerable<ArchivoModel>> SelAllAsync(ArchivoModel c);
    Task<IEnumerable<ArchivoModel>> SelAllActiveAsync(ArchivoModel c);
}

public partial class ArchivoData : IArchivoData
{
    private readonly string _schema = EnuCommon.BdDefault.Schema.Maestro;

    private readonly IDbConnection _connection;
    private readonly UserContext _user;
    public ArchivoData(IDbConnection connection, UserContext user)
    {
        _connection = connection;
        _user = user;
    }
    public async Task<int> InsertAsync(ArchivoModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoTipoEntidad",
            "CoEntidad",
            "CoTipo",
            "CoCalidad",
            "NuOrden",
            "TxDescripcion",
            "NoArchivo",
            "NoExtension",
            "FlDefault",
            "NuBytes",
            "NuAlto",
            "NuAncho",
            "NoTitulo",
            "CoUsuCre"
        );
        string sql = $"SELECT {_schema}.fn_archivo_insert({args})";
        return await _connection.ExecuteScalarAsync<int>(sql, pr);
    }
    public async Task UpdateAsync(ArchivoModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoArchivo",
            "CoTipo",
            "CoCalidad",
            "NuOrden",
            "TxDescripcion",
            "NoArchivo",
            "NoExtension",
            "FlDefault",
            "NuBytes",
            "NuAlto",
            "NuAncho",
            "NoTitulo",
            "CoUsuMud"
        );
        string sql = $"CALL {_schema}.usp_archivo_update({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task DeleteByIdAsync(ArchivoModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoArchivo",
            "CoUsuEli"
        );
        string sql = $"CALL {_schema}.usp_archivo_delete_by_id({args})";
        await _connection.ExecuteScalarAsync(sql, pr);
    }
    public async Task<IEnumerable<ArchivoModel>> SelAllAsync(ArchivoModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoTipoEntidad",
            "CoEntidad"
        );
        string sql = $"SELECT * FROM {_schema}.fn_archivo_sel_all({args})";
        return await _connection.QueryAsync<ArchivoModel>(sql, pr);
    }
    public async Task<IEnumerable<ArchivoModel>> SelAllActiveAsync(ArchivoModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoTipoEntidad",
            "CoEntidad"
        );
        string sql = $"SELECT * FROM {_schema}.fn_archivo_sel_all_active({args})";
        return await _connection.QueryAsync<ArchivoModel>(sql, pr);
    }
    public async Task<ArchivoModel?> SelByIdAsync(ArchivoModel c)
    {
        (DynamicParameters pr, string args) = Glo.Build(c, _user.CoEmpresa, _user.CoPersona,
            "CoEmpresa",
            "CoArchivo"
        );
        string sql = $"SELECT * FROM {_schema}.fn_archivo_sel_by_id({args})";
        return await _connection.QueryFirstOrDefaultAsync<ArchivoModel>(sql, pr);
    }

}
