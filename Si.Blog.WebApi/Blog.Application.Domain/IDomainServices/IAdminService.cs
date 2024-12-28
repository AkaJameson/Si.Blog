using Blog.Application.Shared.Models;
using Blog.Infrastructure.Base.ApiResult;

namespace Blog.Application.Domain.IDomainServices
{
    public interface IAdminService
    {
        Task<ApiResult> CreateUser(UserRegisterInfo userInfo);
    }
}
