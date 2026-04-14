using Microsoft.AspNetCore.Http;

using System.Net;
using System.Security.Claims;

using static System.Net.Mime.MediaTypeNames;

namespace Mozo.Helper.Global;

public class UserContext
{
    public int? CoPersona { get; }
    public int? CoEmpresa { get; } = 1;
    public int? CoPermiso { get; }
    public int? CoIngreso { get; }   

    public UserContext(IHttpContextAccessor accessor)
    {
        var http = accessor.HttpContext!;

        Claim? coPersonaClaim = http.User.FindFirst("CoPersona");
        if (coPersonaClaim!=null)
            CoPersona = int.Parse(coPersonaClaim.Value);

        Claim? coEmpresaClaim = http.User.FindFirst("CoEmpresa");
        if (coEmpresaClaim != null)
            CoEmpresa = int.Parse(coEmpresaClaim.Value);

        Claim? coPermisoClaim = http.User.FindFirst("CoPermiso");
        if (coPermisoClaim != null)
            CoPermiso = int.Parse(coPermisoClaim.Value);

        Claim? coIngresoClaim = http.User.FindFirst("CoIngreso");
        if (coIngresoClaim != null)
            CoIngreso = int.Parse(coIngresoClaim.Value);

    }
}