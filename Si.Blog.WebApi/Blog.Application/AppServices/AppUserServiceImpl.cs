using Blog.Application.Domain.IDomainServices;
using Blog.Application.IAppServices;
using Blog.Application.Shared.Models;
using Blog.Infrastructure.Base.ApiResult;

namespace Blog.Application.AppServices
{
    public class AppUserServiceImpl: IAppUserService
    {
        private readonly IUserService _userService;
        public AppUserServiceImpl(IUserService userService)
        {
            _userService = userService;
        }
        public Task<ApiResult> Login(BaseUserInfo userInfo)
        {
            return _userService.Login(userInfo);
        }

    }
}
