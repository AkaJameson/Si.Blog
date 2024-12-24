namespace Blog.Infrastructure.Base.ApiResult
{
    public static class ResultHelper
    {
        public static ApiResult Success(object data, string message = "操作成功")
        {
            return new ApiResult
            {
                Code = StatusCode.Success,
                Message = message,
                Data = data
            };
        }
        public static ApiResult Success(string message = "操作成功")
        {
            return new ApiResult
            {
                Code = StatusCode.Success,
                Message = message
            };
        }
        public static ApiResult SuccessByPage(int pageSize, int pageIndex, int Total, object data = null, string message = "操作成功")
        {
            return new ApiResult
            {
                Code = StatusCode.Success,
                Message = message,
                Data = data,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Total = Total
            };
        }

        public static ApiResult Error(StatusCode code, string message = "操作失败")
        {
            return new ApiResult
            {
                Code = code,
                Message = message
            };
        }
    }
}
