using Mozo.Helper.Enu;
using Mozo.Model.Empresa;
using Mozo.Model.Maestro;

using System.ComponentModel.DataAnnotations;

///<summary>
///
///</summary>
///<remarks>
///</remarks>
///<history>
/// t[Jonatan Abregu]	16/11/2018	Created
///</history>
namespace Mozo.Model.Seguridad;

public record EmpresaFilter : BaseFilterDto //EmpresaFilter>
{
    public string? NoEmpresaCorto { get; set; }
    public string? NuDocumento { get; set; }

}
[Serializable]
public partial class EmpresaModel : BaseModel
{
    
         public int? CoEmpresa { get; set; }
    public string? NoVision { get; set; }
    public string? NoMision { get; set; }
    public string? TxQuienSoy { get; set; }
    public string? NoDireccion { get; set; }
    [Required(ErrorMessage = EnuCommon.MsgValiadation.Required)] public string? NuDocumento { get; set; }
    public string? NoSeo { get; set; }


    [Required(ErrorMessage = EnuCommon.MsgValiadation.Required)] public string? NoEmpresa { get; set; }
    [Required(ErrorMessage = EnuCommon.MsgValiadation.Required)] public string? NoEmpresaCorto { get; set; }
    public int? CoPais { get; set; }
    public int? CoMoneda { get; set; }

    public int? CoDocumentoIdentidad { get; set; }
    public int? NuDocumentoFiscal { get; set; }
}

public partial class EmpresaModel
{
    public List<EmpresaModuloModel>? EmpresaModuloLst { get; set; }
    public List<HorarioModel>? HorarioCol { get; set; }
    public List<RedSocialModel>? RedSocialLst { get; set; }

    //public string? NoDireccionCompleto => string.Concat(NoDireccion,
    //    NoDistrito != null
    //        ? string.Concat(", distrito de ", NoDistrito.PriLetraMayuscula(), ", provincia de ",
    //            NoProvincia?.PriLetraMayuscula(), ", departamento de ", NoDepartamento?.PriLetraMayuscula())
    //        : "");


}