namespace Blog.Infrastructure.Base.ApiResult
{
    public class ApiResult
    {
        public StatusCode Code { get; set; }

        public string Message { get; set; }
        public object? Data { get; set; }
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        public int? Total { get; set; }
    }


}
