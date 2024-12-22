using Microsoft.EntityFrameworkCore;
using Si.Framework.EntityFramework;

namespace Blog.Application.Database
{
    public class BlogDbContext : SiDbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }


    }
}
