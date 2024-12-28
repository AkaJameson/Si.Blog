using Blog.Application.IAppServices;
using Blog.Application.Shared.Models;
using Blog.Infrastructure.Base.ApiResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Si.Framework.Base;

namespace Blog.Application.WebApi
{
    [ApiController]
    [Route("api/[Controller]/[Action]")]
    public class UserController:Default
    {
        private readonly IAppUserService appUserService;
        public UserController(IAppUserService appUserService)
        {
            this.appUserService = appUserService;
        }
        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="baseUserInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public Task<ApiResult> Login([FromBody] BaseUserInfo baseUserInfo)
        {
            return appUserService.Login(baseUserInfo);
        }
    }
}
