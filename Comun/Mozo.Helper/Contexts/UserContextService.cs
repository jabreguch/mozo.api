using Mozo.Helper.Global;
using Mozo.Helper.Services;

namespace Mozo.Helper.Contexts;

public class UserContextService
{
    private readonly IClaimsService _claimsService;

    public UserContextService(IClaimsService claimsService)
    {
        _claimsService = claimsService;
    }

    public UserContext Create()
    {
        return new UserContext(_claimsService);
    }
}
