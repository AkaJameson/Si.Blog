using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Blog.Application.Shared.EntityConfiguration
{
    public class BlogConfiguration : IEntityTypeConfiguration<Entity.Post>
    {
        public void Configure(EntityTypeBuilder<Entity.Post> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Title).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Views).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.CreateTime).IsRequired();
        }
    }
}
