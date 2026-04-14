using System.ComponentModel.DataAnnotations;

namespace Mozo.Model.Maestro;

public record RedSocialFilter : BaseFilterDto   { }
public partial class RedSocialModel : BaseModel //<RedSocialModel>
{

    public int? CoRedSocial { get; set; }
    public int? CoEtiqueta { get; set; }
    public int? CoTipoUrl { get; set; }
    public int? CoTipoRedSocial { get; set; }
    [Required(ErrorMessage = "Ingrese la información"), MinLength(1), MaxLength(250)] public string? NoRedSocial { get; set; }
    public int? CoPersona { get; set; }
    public int? FlPersona { get; set; }
    public int? FlWhatsapp { get; set; }

}

public partial class RedSocialModel
{
    public bool? FlWhatsapp2 { get; set; }
    public string? NoIconoTipoUrl { get; set; }
    public string? NoEtiqueta { get; set; }
    public string? NoTipoRedSocial { get; set; }
    public string? NoTipoUrl { get; set; }

    public List<TipoGeneralModel>? EtiquetaLst { get; set; }
    public List<TipoGeneralModel>? TipoUrlLst { get; set; }



}