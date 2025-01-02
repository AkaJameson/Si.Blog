using Blog.Application.Domain.IDomainServices;
using Blog.Application.Shared.Entity;
using Blog.Application.Shared.Models;
using Blog.Infrastructure.Base.ApiResult;
using Si.Framework.AutoMapper.MapServices;
using Si.Framework.Base.Extension;
using Si.Framework.EntityFramework.UnitofWork;
using Si.Framework.Rbac.Entity;

namespace Blog.Application.Domain.DomainServices
{
    public class AdminServiceImpl : IAdminService
    {
        private readonly IMapperService mapperService;
        private readonly IUnitOfWork unitOfWork;
        public AdminServiceImpl(IMapperService mapperService,
            IUnitOfWork unitOfWork)
        {
            this.mapperService = mapperService;
            this.unitOfWork = unitOfWork;
        }
        public async Task<ApiResult> CreateUser(UserRegisterInfo userInfo)
        {
            
            await unitOfWork.ExecuteTransactionAsync(async () =>
            {
                var newUser = new User
                {
                    Account = userInfo.Account ?? string.Empty,
                    UserName = userInfo.Username ?? string.Empty,
                    PasswordRsa = userInfo.Password.AESEncrypt() ?? string.Empty,
                    Gender = userInfo.Gender,
                    CreateTime = DateTime.Now,
                };
                await unitOfWork.GetRepository<User>().AddAsync(newUser);
                foreach(var roleId in userInfo.UserRoles)
                {
                    await unitOfWork.GetRepository<UserRole>().AddAsync(new UserRole { UserId = newUser.Id, RoleId = roleId.GetHashCode() });
                }
            });
            await unitOfWork.CommitAsync();
            return ResultHelper.Success("创建成功");
        }
    }
}
