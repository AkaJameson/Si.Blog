using Blog.Application.Shared.enums;

namespace Blog.Application.Shared.Models
{

    public class BaseUserInfo
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public bool? Encrypted { get; set; }
    }
    /// <summary>
    /// 用户注册信息
    /// </summary>
    public class UserRegisterInfo
    {
        public string Username { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public RoleEnum Role { get; set; }
    }
    /// <summary>
    /// 修改密码信息
    /// </summary>
    public class ChangePasswordInfo
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
