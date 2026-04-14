using Mozo.Helper.Enu;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mozo.Model.Seguridad;

public record PerfilFilter : BaseFilterDto   { }

[Serializable]
public partial class PerfilModel : BaseModel //<PerfilModel>
{
    public int? CoPerfil { get; set; }
    [Required(ErrorMessage = EnuCommon.MsgValiadation.Required)][DisplayName("Perfil:")] public string? NoPerfil { get; set; }
    public int? CoModulo { get; set; }

    private int? flDefault;

    public int? FlDefault
    {
        get => flDefault ?? 0;
        set
        {
            flDefault = value;
        }
    }
    public bool? FlDefault2 { get; set; }

}

public partial class PerfilModel
{
    public string? NoModuloDescripcion { get; set; }
    public List<PerfilPaginaModel>? PerfilPaginaLst { get; set; }
    public List<MenuModel>? MenuLst { get; set; }
}