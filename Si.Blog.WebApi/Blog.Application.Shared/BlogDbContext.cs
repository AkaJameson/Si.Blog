﻿using Blog.Application.Shared.Entity;
using Blog.Application.Shared.EntityConfiguration;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Si.Framework.EntityFramework;
using Si.Framework.Rbac.Configuration;
using Si.Framework.Rbac.Entity;

namespace Blog.Application.Shared
{
    public class BlogDbContext : SiDbContext
    {
        private readonly string connectionString;
        public BlogDbContext(DbContextOptions<BlogDbContext> options,IMediator mediator) : base(options,mediator)
        {
        }
        public BlogDbContext(DbContextOptions<BlogDbContext> options,string connectionString) : base(options)
        {
            this.connectionString = connectionString;
        }
        public DbSet<Entity.Post> Blogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogTag> BlogTages { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BlogCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new BlogConfiguration());
            modelBuilder.ApplyConfiguration(new BlogTagConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new MediaConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new ResourceConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString));
            }
        }
    }
}
