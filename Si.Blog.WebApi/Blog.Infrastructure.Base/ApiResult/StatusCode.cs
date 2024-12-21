namespace Blog.Application.ApiResult
{
    public enum StatusCode:int
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 200,
        /// <summary>
        /// 请求参数错误
        /// </summary>
        BadRequest = 400,
        /// <summary>
        /// 未授权
        /// </summary>
        Unauthorized = 401,
        /// <summary>
        /// 禁止访问
        /// </summary>
        Forbidden = 403,
        /// <summary>
        /// 资源不存在
        /// </summary>
        NotFound = 404,
        /// <summary>
        /// 服务器错误
        /// </summary>
        InternalServerError = 500,
        /// <summary>
        /// 服务不可用
        /// </summary>
        ServiceUnavailable = 503
    }
}
