using Blog.Application.Domain.IDomainServices;

namespace Blog.Application.AppServices
{
    public class AppUserServices
    {
        private readonly IUserService _userService;
        public AppUserServices(IUserService userService)
        {
            _userService = userService;
        }

    }
}
