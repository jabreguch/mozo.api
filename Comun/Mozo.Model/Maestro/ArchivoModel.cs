namespace Mozo.Model.Maestro;

public record ArchivoFilter : BaseFilterDto   { }
[Serializable]
public partial class ArchivoModel : BaseModel //<ArchivoModel>
{
    public int? CoTipoEntidad { get; set; }
    public int? CoEntidad { get; set; }
    public int? CoCalidad { get; set; }
    public string? NoArchivo { get; set; }
    public string? NoExtension { get; set; }
    public int? CoArchivo { get; set; }
    public int? CoTipo { get; set; }
    public int? NuOrden { get; set; }
    public string? TxDescripcion { get; set; }
    public int? FlDefault { get; set; }
    public long? NuBytes { get; set; }
    public int? NuAlto { get; set; }
    public int? NuAncho { get; set; }
    public string? NoTitulo { get; set; }

}
public partial class ArchivoModel
{

    //string fileName = request.Headers["X-File-Name"].ToString();
    //int chunkIndex = int.Parse(request.Headers["X-Chunk-Index"].ToString());
    //int totalChunks = int.Parse(request.Headers["X-Total-Chunks"].ToString());


    public int? NuChunkIndex { get; set; }
    public int? NuTotalChunks { get; set; }


}

