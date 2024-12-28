using Blog.Application.Shared.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Application.Shared.EntityConfiguration
{
    internal class BlogCategoryConfiguration : IEntityTypeConfiguration<BlogCategory>
    {
        public void Configure(EntityTypeBuilder<BlogCategory> builder)
        {
            builder.HasKey(bc => new { bc.CategoryId, bc.BlogId });
            builder.Property(bc => bc.CategoryId)
                   .IsRequired();
            builder.Property(bc => bc.BlogId)
                   .IsRequired();
        }
    }
}
