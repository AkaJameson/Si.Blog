﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Si.Framework.Rbac.Entity;

namespace Blog.Application.Shared.EntityConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.RoleName).HasMaxLength(50).IsRequired();
            builder.Property(r => r.Description).HasMaxLength(200).IsRequired(false);
            builder.Property(r => r.CreateTime).IsRequired();
        }
    }
}
