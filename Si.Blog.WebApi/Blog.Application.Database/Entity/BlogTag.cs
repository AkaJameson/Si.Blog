using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Application.Database.Entity
{
    public class BlogTag
    {
        public int TagId { get; set; }
        public int BlogId { get; set; }
    }
    public class BlogTagMap : IEntityTypeConfiguration<BlogTag>
    {
        public void Configure(EntityTypeBuilder<BlogTag> builder)
        {
            builder.HasKey(t => new { t.TagId, t.BlogId });
            builder.Property(t => t.TagId).IsRequired();
            builder.Property(t => t.BlogId).IsRequired();
        }
    }
}
