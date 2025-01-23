namespace Blog.Infrastructure.Rbac.Entity
{
    public class Permission
    {
        public int Id { get; set; }
        public string PermissionName { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }

    }
}
