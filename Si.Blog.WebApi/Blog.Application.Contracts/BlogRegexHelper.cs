using Microsoft.Extensions.Configuration;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace Blog.Application.Contracts
{
    public class BlogRegexHelper
    {
        private readonly IConfiguration configuration;
        public BlogRegexHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool AccountRegex(string account)
        {
            var regexPattern = configuration["Pattern:Account"];
            if (string.IsNullOrEmpty(regexPattern))
            {
                throw new InvalidOperationException("Account regex pattern is not configured.");
            }
            var regex = new Regex(regexPattern);
            var flag = regex.IsMatch(account);
            return flag;
        }
        public bool PasswordRegex(string password)
        {
            var regexPattern = configuration["Pattern:Password"];
            if (string.IsNullOrEmpty(regexPattern))
            {
                throw new InvalidOperationException("Account regex pattern is not configured.");
            }
            var regex = new Regex(regexPattern);
            var flag = regex.IsMatch(password);
            return flag;
        }
    }
}
