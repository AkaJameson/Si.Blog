using Blog.Application.Domain.IDomainServices;
using Blog.Application.Shared.Entity;
using Blog.Application.Shared.Models;
using Blog.Infrastructure.Base.ApiResult;
using Blog.Infrastructure.Rbac.Entity;
using Blog.Infrastructure.Rbac.JWT;
using Si.Framework.EntityFramework.UnitofWork;
using Si.Framework.ToolKit.Extension;
using System.Security.Claims;

namespace Blog.Application.Domain.DomainServices
{
    public class UserServiceImpl : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        public UserServiceImpl(IRepository<User> userRepository,IRepository<Role> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        public async Task<ApiResult> Login(BaseUserInfo entity)
        {
            //如果传参没有加密，则默认加密
            if(entity.Encrypted.HasValue && !entity.Encrypted.Value)
            {
                entity.Password = entity.Password.AESEncrypt();
            }
            var user = await _userRepository.SingleOrDefaultAsync(u => u.Account == entity.Username && u.PasswordRsa == entity.Password);
            if (user == null)
                return ResultHelper.Error(StatusCode.BadRequest, "用户名或密码错误");
            //获取用户角色
            var role = await _roleRepository.SingleOrDefaultAsync(r => r.Id == user.RoleId);
            var _token = JWTHelper.GenerateToken(user.UserName, new List<Claim>
            {
                new Claim(ClaimTypes.Role,role.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email??string.Empty),
                new Claim(ClaimTypes.Gender,user.Gender??string.Empty),
                new Claim(ClaimTypes.Name,user.UserName??string.Empty)
            });
            return ResultHelper.Success(new
            {
                Token = _token,
                ExpiresAt = new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeMilliseconds()
            }, "登录成功");
        }
        public Task<ApiResult> Register(BaseUserInfo entity)
        {
            throw new NotImplementedException();
        }
    }
}
