using Mozo.Helper.Services;

namespace Mozo.Helper.Global;

public class UserContext
{
    public int? CoPersona { get; }
    public int? CoEmpresa { get; } = 1;
    public int? CoPermiso { get; }
    public int? CoIngreso { get; }
    public string? NoUsuario { get; }
    public bool IsAuthenticated { get; }
    public bool IsValid => IsAuthenticated && CoEmpresa.HasValue;

    public UserContext(IClaimsService claimsService)
    {
        ArgumentNullException.ThrowIfNull(claimsService);

        IsAuthenticated = claimsService.IsAuthenticated;
        CoPersona = claimsService.GetInt32("CoPersona");
        CoEmpresa = claimsService.GetInt32("CoEmpresa") ?? CoEmpresa;
        CoPermiso = claimsService.GetInt32("CoPermiso");
        CoIngreso = claimsService.GetInt32("CoIngreso");
        NoUsuario = claimsService.GetString("NoUsuario");
    }

    public void Validate()
    {
        if (!IsValid)
            throw new InvalidOperationException("El contexto de usuario no es válido para la operación.");
    }
}
