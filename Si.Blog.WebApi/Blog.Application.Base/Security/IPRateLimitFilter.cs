using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Si.Framework.Base.Abstraction;

namespace Blog.Infrastructure.Security
{
    public class IpRateLimitFilter : ISiFilter, IAsyncActionFilter
    {
        private readonly IMemoryCache _cache;
        private readonly int _maxRequests;
        private readonly TimeSpan _timeSpan;
        public IpRateLimitFilter()
        {
        }

        public IpRateLimitFilter(IMemoryCache cache, IConfiguration configuration)
        {
            _cache = cache;
            _maxRequests = configuration.GetValue<int>("IpRateLimit:MaxRequests");
            var timeSpanMinutes = configuration.GetValue<int>("IpRateLimit:TimeSpanMinutes");
            _timeSpan = TimeSpan.FromMinutes(timeSpanMinutes);
            Order = 1;
        }
        public int Order { get; set; } = 0;
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var ip = context.HttpContext.Connection.RemoteIpAddress.ToString();
            var now = DateTime.UtcNow;
            if (!_cache.TryGetValue(ip, out (int Count, DateTime LastRequestTime) requestInfo))
            {
                // 如果缓存中没有此 IP 的信息，初始化请求计数和时间
                requestInfo = (1, now);
                _cache.Set(ip, requestInfo, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = _timeSpan // 设置绝对过期时间
                });
            }
            else
            {
                if (now - requestInfo.LastRequestTime > _timeSpan)
                {
                    // 超过时间段，重置请求计数和时间
                    requestInfo = (1, now);
                }
                else
                {
                    requestInfo.Count++;
                }

                if (requestInfo.Count > _maxRequests)
                {
                    context.Result = new Microsoft.AspNetCore.Mvc.StatusCodeResult(429);
                    return;
                }
                _cache.Set(ip, requestInfo, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = _timeSpan // 设置绝对过期时间
                });
            }

            await next();
        }
    }
}
