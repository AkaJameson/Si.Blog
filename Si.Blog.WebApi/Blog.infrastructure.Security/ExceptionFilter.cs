using Blog.Application.ApiResult;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Si.Framework.Base.Abstraction;
using System.Net;

namespace Blog.Application.Filter
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
            context.Result = new JsonResult(ResultHelper.Error(StatusCode.Unauthorized, "服务器内部错误"));
            context.ExceptionHandled = true;
        }
    }
}
