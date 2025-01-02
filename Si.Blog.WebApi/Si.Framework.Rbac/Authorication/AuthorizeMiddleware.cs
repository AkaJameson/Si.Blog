using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Si.Framework.Rbac.Authorication
{
    public class AuthorizeMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Response.StatusCode == 403)
            {
                httpContext.Response.StatusCode = 200;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync
                    (JsonConvert.SerializeObject(new { code = 403, message = "权限不足，请联系管理员!" }));
            }
            await _next(httpContext);
        }
    }
}
