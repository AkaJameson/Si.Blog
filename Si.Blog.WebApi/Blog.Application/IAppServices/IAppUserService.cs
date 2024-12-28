using Blog.Application.Shared.Models;
using Blog.Infrastructure.Base.ApiResult;

namespace Blog.Application.IAppServices
{
    public interface IAppUserService
    {
        Task<ApiResult> Login(BaseUserInfo userInfo);
    }
}
