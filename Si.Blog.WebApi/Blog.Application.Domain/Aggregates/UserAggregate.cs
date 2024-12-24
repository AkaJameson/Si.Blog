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

        public UserAggregate(IUnitOfWork unitOfWork
            ,IMediator mediator):base(mediator)
        {
            _userRepository = unitOfWork.GetRepository<User>();
            _postRepository = unitOfWork.GetRepository<Post>();
            _mediaRepository = unitOfWork.GetRepository<Media>();
        }

        public async Task InitByUserId(int userId)
        {
            user = await _userRepository.GetByIdAsync(userId);
        }

    }
}
