using Blog.Infrastructure.Rbac.enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Application.Database.Entity
{
    public class User
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string AvatarPath { get; set; }
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
        public string Stamp { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public Role Role { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
    }
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.AvatarPath).HasMaxLength(200);
            builder.Property(x => x.Account).HasMaxLength(50).IsRequired();
            builder.Property(x => x.PasswordRsa).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Stamp).HasMaxLength(200);
            builder.Property(x => x.Role).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(100);
        }
    }
}
