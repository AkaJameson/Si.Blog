using Blog.Application.Contracts;
using Blog.Application.Domain.IDomainServices;
using Blog.Application.IAppServices;
using Blog.Application.Shared.Models;
using Blog.Infrastructure.Base.ApiResult;
using Microsoft.Extensions.Configuration;

namespace Blog.Application.AppServices
{
    public class AppAdminServiceImpl : IAppAdminService
    {
        private readonly BlogRegexHelper blogRegexHelper;
        private readonly IAdminService adminService;
        public AppAdminServiceImpl(BlogRegexHelper blogRegexHelper,
            IAdminService adminService)
        {
            this.blogRegexHelper = blogRegexHelper;
            this.adminService = adminService;
        }
        public async Task<ApiResult> CreateUser(UserRegisterInfo userInfo)
        {
            var regexFlag = blogRegexHelper.AccountRegex(userInfo.Account ?? string.Empty)
                            && blogRegexHelper.PasswordRegex(userInfo.Password);
            if (!regexFlag)
            {
                return ResultHelper.Error(StatusCode.BadRequest, "账号或者密码格式错误！\n" +
                    "账号：4-16位（字母，数字，下划线，减号 密码：至少三个字符，至少一个大写字母一个小写字母一个数字和一个特殊字符");
            }
            return await adminService.CreateUser(userInfo);
        }
    }
}
