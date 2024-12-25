using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Si.Framework.Base
{
    [Authorize(AuthenticationSchemes = "Bearer",Policy ="Read")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 1)]
    public class Default : ControllerBase
    {
    }
}
