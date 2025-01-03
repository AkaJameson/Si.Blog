using Blog.Application.Domain.IDomainServices;
using Blog.Application.Shared.Entity;
using Blog.Application.Shared.enums;
using Blog.Application.Shared.Models;
using Blog.Infrastructure.Base.ApiResult;
using Si.Framework.Base.Extension;
using Si.Framework.EntityFramework.UnitofWork;
using Si.Framework.Rbac.Entity;
using Si.Framework.Rbac.JWT;
using System.Security.Claims;

namespace Blog.Application.Domain.DomainServices
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserRole> _userRoleRepository;
        public UserServiceImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = unitOfWork.GetRepository<User>();
            _roleRepository = unitOfWork.GetRepository<Role>();
            _userRoleRepository = unitOfWork.GetRepository<UserRole>();
        }
        public async Task<ApiResult> Login(BaseUserInfo entity)
        {
            //如果传参没有加密，则默认加密
            if (entity.Encrypted.HasValue && !entity.Encrypted.Value)
            {
                entity.Password = entity.Password.AESEncrypt();
            }
            var user = await _userRepository.SingleOrDefaultAsync(u => u.Account == entity.Account && u.PasswordRsa == entity.Password);
            if (user == null)
                return ResultHelper.Error(StatusCode.BadRequest, "用户名或密码错误");
            //获取用户角色
            var userRoles = await _userRoleRepository.FindAsync(u => u.UserId == user.Id);
            var _token = JWTHelper.GenerateToken(userRoles.Select(ur => ur.RoleId).ToList(), new List<Claim>
            {
                new Claim(ClaimTypes.Sid,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email??string.Empty),
                new Claim(ClaimTypes.Gender,user.Gender.GetHashCode().ToString()),
                new Claim(ClaimTypes.Name,user.UserName??string.Empty),
            });
            return ResultHelper.Success(new
            {
                Token = _token,
                ExpiresAt = new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeMilliseconds()
            }, "登录成功");
        }
        public async Task<ApiResult> Register(UserRegisterInfo entity)
        {
            var user = new User
            {
                Account = entity.Account,
                PasswordRsa = entity.Password.AESEncrypt(),
                Email = entity.Email,
                Gender = entity.Gender,
                UserName = entity.Username,
            };
            //默认注册用户角色为普通用户
            await _unitOfWork.ExecuteTransactionAsync(async () =>
            {
                await _userRepository.AddAsync(user);
                await _userRoleRepository.AddAsync(new UserRole
                {
                    UserId = user.Id,
                    RoleId = RoleEnum.User.GetHashCode() //普通用户角色ID
                });
            });
            await _unitOfWork.CommitAsync();
            return ResultHelper.Success("注册成功");
        }

    }
}
