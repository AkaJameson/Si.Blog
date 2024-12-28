using Blog.Application.IAppServices;
using Blog.Application.Shared.Models;
using Blog.Infrastructure.Base.ApiResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Si.Framework.Base;

namespace Blog.Application.WebApi
{
    [Authorize("Admin")]
    [Route("api/[Controller]/[Action]")]
    public class AdminController : Default
    {
        private readonly IAppAdminService _appAdminService;
        public AdminController(IAppAdminService appAdminService)
        {
            _appAdminService = appAdminService;
        }
        [HttpPost]
        public async Task<ApiResult> CreateUserAsync([FromBody] UserRegisterInfo userInfo)
        {
            return await _appAdminService.CreateUser(userInfo);
        }
    }
}
