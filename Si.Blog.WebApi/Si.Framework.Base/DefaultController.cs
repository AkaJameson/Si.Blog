using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Si.Framework.Base
{
    [Authorize(AuthenticationSchemes = "Bearer,Cookies")]
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 1)]
    internal class DefaultController : ControllerBase
    {
    }
}
