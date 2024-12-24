using Blog.Application.Shared.Entity;
using MediatR;
using Si.Framework.DDDComm.Abstraction;
using Si.Framework.EntityFramework.UnitofWork;

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

        public UserAggregate(IRepository<User> userRepository
            , IRepository<Post> postRepository
            , IRepository<Media> mediaRepository
            ,IMediator mediator,int userId):base(mediator)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _mediaRepository = mediaRepository;
            user = _userRepository.GetByIdAsync(userId).Result;
        }

    }
}
