using Blog.Infrastructure.Rbac.Entity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Database.EntityConfiguration
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<RolePermission> builder)
        {
            builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });
        }
    }
}
