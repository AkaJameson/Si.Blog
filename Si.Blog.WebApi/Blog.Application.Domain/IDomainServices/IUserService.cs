using Blog.Application.Shared.Models;
using Blog.Infrastructure.Base.ApiResult;

namespace Blog.Application.Domain.IDomainServices
{
    public interface IUserService
    {
        Task<ApiResult> Login(BaseUserInfo entity);
        Task<ApiResult> Register(BaseUserInfo entity);
    }
}
