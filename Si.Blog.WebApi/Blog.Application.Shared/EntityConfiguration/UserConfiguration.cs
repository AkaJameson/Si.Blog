using Blog.Application.Shared.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Application.Shared.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.UserName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.AvatarPath).HasMaxLength(200);
            builder.Property(x => x.Account).HasMaxLength(50).IsRequired();
            builder.Property(x => x.PasswordRsa).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Gender).IsRequired();
            builder.Property(x => x.Stamp).HasMaxLength(200);
            builder.Property(x => x.Role).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(100);
        }
    }
}
