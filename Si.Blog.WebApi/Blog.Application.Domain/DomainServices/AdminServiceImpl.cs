using Blog.Application.Domain.IDomainServices;
using Blog.Application.Shared.Dtos;
using Blog.Application.Shared.Entity;
using Blog.Application.Shared.Models;
using Blog.Infrastructure.Base.ApiResult;
using Si.Framework.AutoMapper.MapServices;
using Si.Framework.EntityFramework.UnitofWork;
using Si.Framework.ToolKit.Extension;

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
            var newUser = new User
            {
                Account = userInfo.Account,
                UserName = userInfo.Username,
                PasswordRsa = userInfo.Password.AESEncrypt(),
                Gender = userInfo.Gender,
                Email = userInfo.Email,
                Role = userInfo.Role,
                CreateTime = DateTime.Now
            };
            var userRepository = unitOfWork.GetRepository<User>();
            await userRepository.AddAsync(newUser);
            var flag = await userRepository.SaveChangesAsync();
            if(flag)
            {
                return ResultHelper.Success("创建成功");
            }
            else
            {
                return ResultHelper.Error(StatusCode.ServiceUnavailable,"创建失败");
            }

        }
    }
}
