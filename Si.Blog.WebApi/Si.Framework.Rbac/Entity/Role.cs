namespace Blog.Infrastructure.Rbac.Entity
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        
    }
}
