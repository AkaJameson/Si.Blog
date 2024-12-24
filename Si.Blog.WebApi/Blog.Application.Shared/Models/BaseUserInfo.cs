namespace Blog.Application.Shared.Models
{
    public class BaseUserInfo
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public bool? Encrypted { get; set; }
    }
}
