using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Application.Database.Entity
{
    public class BlogCategory
    {
        public int CategoryId { get; set; }
        public int BlogId { get; set; }
    }

    public class BlogCategoryMap : IEntityTypeConfiguration<BlogCategory>
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
