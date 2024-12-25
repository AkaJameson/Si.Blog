using Blog.Application.Shared.Dtos;
using Blog.Application.Shared.Models;
using Blog.Infrastructure.Base.ApiResult;

namespace Blog.Application.Domain.IDomainServices
{
    public interface IUserService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ApiResult> Login(BaseUserInfo entity);
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ApiResult> Register(UserRegisterInfo entity);
    }
}
