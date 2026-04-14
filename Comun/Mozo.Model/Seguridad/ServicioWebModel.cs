using System.Text.Json.Serialization;

namespace Mozo.Model.Seguridad;

public record ServicioWebFilter : BaseFilterDto  { }

[Serializable]
public partial class ServicioWebModel : BaseModel //<ServicioWebModel>
{

    public int? CoPagina { get; set; }
    public string? TxDescripcion { get; set; }
    public int? CoMenu { get; set; }
    public int? CoArea { get; set; }
    public int? CoPersona { get; set; }
    public int? CoPaginaPadre { get; set; }
    public string? NoControlador { get; set; }
    public string? NoAccion { get; set; }
    public string? NoMenu { get; set; }
    public string? NoParametro { get; set; }
    public string? NoOpcion { get; set; }
    public int? NuOrden { get; set; }
    public int? CoModulo { get; set; }
    public string? NoModuloDescripcion { get; set; }
    public string? NoArea { get; set; }
    public int? CoTipoPagina { get; set; }
}
public partial class ServicioWebModel
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
        get => "bi bi-cloud";
    }

}