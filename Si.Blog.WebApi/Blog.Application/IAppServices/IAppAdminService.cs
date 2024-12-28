using Blog.Application.Shared.Models;
using Blog.Infrastructure.Base.ApiResult;

namespace Blog.Application.IAppServices
{
    public interface IAppAdminService
    {
        public Task<ApiResult> CreateUser(UserRegisterInfo userInfo);
    }
}
