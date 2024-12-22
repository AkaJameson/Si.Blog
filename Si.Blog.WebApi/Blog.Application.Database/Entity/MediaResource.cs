using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Application.Database.Entity
{
    public class MediaResource
    {
        public int MediaId { get; set; }
        public int ResourceId { get; set; }
    }
    public class MediaResourceMap : IEntityTypeConfiguration<MediaResource>
    {
        public void Configure(EntityTypeBuilder<MediaResource> builder)
        {
            builder.HasKey(m => new { m.MediaId, m.ResourceId });
            builder.Property(m => m.MediaId).IsRequired();
            builder.Property(m => m.ResourceId).IsRequired();
        }
    }
}
