using Blog.Infrastructure.Base.ApiResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Si.Framework.Base.Abstraction;
using Si.Framework.Serilog;
using System.Net;

namespace Blog.Infrastructure.Security
{
    public class ExceptionHandlingFilter : IExceptionFilter, ISiFilter
    {
        public int Order { get; set; } = 1;

        public void OnException(ExceptionContext context)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;
            if (context.Exception is ArgumentNullException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
            }
            // 返回统一的错误响应
            LogHub.Error(context.Exception.ToString());
            context.Result = new JsonResult(ResultHelper.Error(StatusCode.Unauthorized, "服务器内部错误"));
            context.ExceptionHandled = true;
        }
    }
}
