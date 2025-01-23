using Blog.Infrastructure.Rbac.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Application.Database.EntityConfiguration
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.RoleName).HasMaxLength(50).IsRequired();
            builder.Property(r => r.Description).HasMaxLength(200).IsRequired(false);
            builder.Property(r => r.CreateTime).IsRequired();
        }
    }
}
