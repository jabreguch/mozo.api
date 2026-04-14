using Mozo.Helper.Enu;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mozo.Model.Seguridad;

public record PaginaFilter : BaseFilterDto   { }

[Serializable]
public partial class PaginaModel : BaseModel //<PaginaModel>
{
    [Required(ErrorMessage = EnuCommon.MsgValiadation.Required)] public int? CoModulo { get; set; }
    [Required(ErrorMessage = EnuCommon.MsgValiadation.Required)] public int? CoTipoPagina { get; set; }
    public int? CoPagina { get; set; }
    public string? TxDescripcion { get; set; }
    [Required(ErrorMessage = EnuCommon.MsgValiadation.Required)] public int? CoMenu { get; set; }
    public int? CoPaginaPadre { get; set; }
    [Required(ErrorMessage = EnuCommon.MsgValiadation.Required)] public int? CoArea { get; set; }
    [Required(ErrorMessage = EnuCommon.MsgValiadation.Required)] public string? NoControlador { get; set; }
    [Required(ErrorMessage = EnuCommon.MsgValiadation.Required)] public string? NoAccion { get; set; }
    [Required(ErrorMessage = EnuCommon.MsgValiadation.Required)] public string? NoOpcion { get; set; }
    [Required(ErrorMessage = EnuCommon.MsgValiadation.Required)] public int? NuOrden { get; set; }
    public string? NoParametro { get; set; }
}
public partial class PaginaModel
{
    [JsonIgnore]
    public string? NoPathPage
    {
        get
        {
            if (NoControlador != null)
                return string.Concat(NoArea, "/", NoControlador, "/", NoAccion);
            else return string.Empty;
        }
    }

    [JsonIgnore]
    public string? Icono
    {
        get => "bi bi-arrow-right-circle";
    }

    //public string? Icono()
    //{
    //    return "bi bi-arrow-right-circle";
    //    //coTipoPagina = coTipoPagina ?? CoTipoPagina;


    //    //if (CoTipoPagina == EnuSeguridad.TipoPagina.Menu)
    //    //    return "bi bi-menu-app";
    //    //else if (CoTipoPagina == EnuSeguridad.TipoPagina.Pagina)
    //    //    return "bi bi-arrow-right-circle";
    //    //else if (CoTipoPagina == EnuSeguridad.TipoPagina.SubPagina)
    //    //    return "bi bi-caret-right";
    //    //else if (CoTipoPagina == EnuSeguridad.TipoPagina.PaginaFlotante)
    //    //    return "bi bi-window";
    //    //else if (CoTipoPagina == EnuSeguridad.TipoPagina.ServicioWeb)
    //    //    return "bi bi-cloud";

    //    //return "";

    //}

    //

    public int? CoPersona { get; set; }
    public string? NoArea { get; set; }
    public List<SubPaginaModel>? SubPaginaLst { get; set; }
    public List<PaginaFlotanteModel>? PaginaFlotanteLst { get; set; }
    public List<ServicioWebModel>? ServicioWebLst { get; set; }
    public int? CoPerfil { get; set; }
    public string? NoModuloDescripcion { get; set; }
    public string? NoMenu { get; set; }
}