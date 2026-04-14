namespace Mozo.Model.Seguridad;

public record EmpresaModuloFilter : BaseFilterDto //EmpresaModuloFilter>
{



}
[Serializable]
public partial class EmpresaModuloModel : BaseModel //<EmpresaModuloModel>
{

    public int? CoEmpresaModulo { get; set; }
    public int? NuOrden { get; set; }
    public int? CoModulo { get; set; }
    public int? FlOnlyTypeMasterTable { get; set; }
}
public partial class EmpresaModuloModel
{
    public int? CoEmpresa { get; set; }
    public string? NoModuloDescripcion { get; set; } = null!;
}