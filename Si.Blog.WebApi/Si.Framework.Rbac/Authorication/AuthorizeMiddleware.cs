using Blog.Infrastructure.Base.ApiResult;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Blog.Infrastructure.Rbac.Authorication
{
    public class AuthorizeMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext, RequestDelegate next)
        {
            if (httpContext.Response.StatusCode == 403)
            {
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync
                    (JsonConvert.SerializeObject(ResultHelper.Error(StatusCode.Unauthorized, "权限不足，请联系管理员!")));
            }
        }
    }
}
