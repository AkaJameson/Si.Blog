using Blog.Application.Shared.Dtos;
using Blog.Application.Shared.Entity;
using MediatR;
using Microsoft.AspNetCore.Http;
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

        private IRepository<User> _userRepository;
        private IRepository<Post> _postRepository;
        private IRepository<Media> _mediaRepository;
        private IHttpContextAccessor _httpContextAccessor;
        public UserAggregate(IUnitOfWork unitOfWork
            ,IMediator mediator,IHttpContextAccessor httpContextAccessor):base(mediator)
        {
            _userRepository = unitOfWork.GetRepository<User>();
            _postRepository = unitOfWork.GetRepository<Post>();
            _mediaRepository = unitOfWork.GetRepository<Media>();
            //所有请求都有且必须有身份
            var userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Sid)!.Value;

        }

        private async Task EditUserInfo(UserDto userDto)
        {
            
        }

    }
}
