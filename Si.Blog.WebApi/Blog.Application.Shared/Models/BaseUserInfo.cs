using Blog.Application.Shared.enums;

namespace Blog.Application.Shared.Models
{
    public class BaseUserInfo
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public bool? Encrypted { get; set; }
    }

    public class UserRegisterInfo
    {
        public string Username { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool? Encrypted { get; set; }
        public Gender Gender { get; set; }
    }

    public class ChangePasswordInfo
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
