using Blog.Application.Shared.enums;
using Si.Framework.Rbac.Entity;

namespace Blog.Application.Shared.Entity
{
    public class User:BaseUser
    {
       
        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string? AvatarPath { get; set; }
        /// <summary>
        /// 账户名
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码哈希值
        /// </summary>
        public string PasswordRsa { get; set; }
        /// <summary>
        /// 密码盐值
        /// </summary>
        public string? Stamp { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string? Email { get; set; }
        public string? Reserve1 { get; set; }
        public string? Reserve2 { get; set; }
        public string? Reserve3 { get; set; }
    }
}
