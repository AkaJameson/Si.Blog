using Blog.Application.Shared.Entity;
using Si.Framework.DDDComm.Abstraction;
using Si.Framework.EntityFramework.UnitofWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Domain.Aggregates
{
    public class UserAggregate
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

        public UserAggregate(IRepository<User> userRepository, IRepository<Post> postRepository, IRepository<Media> mediaRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _mediaRepository = mediaRepository;
        }

    }
}
