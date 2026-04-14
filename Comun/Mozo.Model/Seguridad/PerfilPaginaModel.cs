namespace Mozo.Model.Seguridad;

public record PerfilPaginaFilter : BaseFilterDto //PerfilPaginaFilter>
{
    public int? CoPerfil { get; set; }
    public int? CoModulo { get; set; }
}
[Serializable]
public partial class PerfilPaginaModel : BaseModel //<PerfilPaginaModel>
{
    public int? CoPerfilPrivilegio { get; set; }
    public int? CoPerfil { get; set; }
    public int? CoModulo { get; set; }
    public int? CoMenu { get; set; }
    public int? CoPagina { get; set; }
    public int? CoPaginaPadre { get; set; }

}

public partial class PerfilPaginaModel
{
    public int? NuOrden { get; set; }
    public string? NoMenu { get; set; }

    public int? CoTipoPagina { get; set; }
    public string? NoArea { get; set; }
    public string? NoControlador { get; set; }
    public string? NoAccion { get; set; }
    public string? NoOpcion { get; set; }

}