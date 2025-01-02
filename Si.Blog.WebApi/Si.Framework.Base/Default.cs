using Microsoft.AspNetCore.Mvc;

namespace Si.Framework.Base
{
    [ResponseCache(VaryByHeader = "User-Agent", Duration = 1)]
    public class Default : ControllerBase
    {
    }
}
