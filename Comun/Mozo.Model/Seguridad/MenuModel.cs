using Mozo.Helper.Enu;

using System.ComponentModel.DataAnnotations;

namespace Mozo.Model.Seguridad;

public record MenuFilter : BaseFilterDto //MenuFilter>
{
    public int? CoModulo { get; set; }
}
[Serializable]
public partial class MenuModel : BaseModel //<MenuModel>
{
    [Required(ErrorMessage = EnuCommon.MsgValiadation.Required)] public int? NuOrden { get; set; }
    public int? CoMenu { get; set; }
    [Required(ErrorMessage = EnuCommon.MsgValiadation.Required)] public string? NoMenu { get; set; }
    public int? CoPersona { get; set; }
    public int? CoModulo { get; set; }
    public string? NoModuloDescripcion { get; set; }
    public int? CoPerfil { get; set; }

}
public partial class MenuModel
{
    public List<PaginaModel>? PaginaLst { get; set; }

}