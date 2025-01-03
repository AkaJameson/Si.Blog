﻿using Blog.Application.Shared.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Application.Shared.EntityConfiguration
{
    public class BlogTagConfiguration : IEntityTypeConfiguration<BlogTag>
    {
        public void Configure(EntityTypeBuilder<BlogTag> builder)
        {
            builder.HasKey(t => new { t.TagId, t.BlogId });
            builder.Property(t => t.TagId).IsRequired();
            builder.Property(t => t.BlogId).IsRequired();
        }
    }
}
