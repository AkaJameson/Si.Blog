using Blog.Application.Shared.Dtos;
using Blog.Application.Shared.Entity;
using Blog.Infrastructure.Base.ApiResult;
using MediatR;
using Microsoft.AspNetCore.Http;
using Si.Framework.AutoMapper.MapServices;
using Si.Framework.DDDComm.Abstraction;
using Si.Framework.EntityFramework.UnitofWork;
using System.Security.Claims;

namespace Blog.Application.Domain.Aggregates
{
    public class UserAggregate:AggregateRoot<Guid>
    {
        /// <summary>
        /// 用户
        /// </summary>
        public User user { get; private set; }
        /// <summary>
        /// 文章
        /// </summary>
        public List<Post> posts { get; private set; }

        /// <summary>
        /// 媒体
        /// </summary>
        public List<Media> medias { get; private set; }

        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<Media> _mediaRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapperService _mapperService;
        public UserAggregate(IUnitOfWork unitOfWork
            ,IMediator mediator
            ,IHttpContextAccessor httpContextAccessor
            ,IMapperService mapperService
            ):base(mediator)
        {
            _userRepository = unitOfWork.GetRepository<User>();
            _postRepository = unitOfWork.GetRepository<Post>();
            _mediaRepository = unitOfWork.GetRepository<Media>();
            //所有请求都有且必须有身份
            var userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Sid)!.Value;
            _httpContextAccessor = httpContextAccessor;
            _mapperService = mapperService;
        }

        private async Task<ApiResult> EditUserInfo(UserDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(userDto.Id);
            if (user == null)
            {
                throw new ArgumentException("用户未找到");
            }
            _mapperService.Map(userDto, user);
            return ResultHelper.Success("操作成功");
        }

    }
}
