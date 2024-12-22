using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Database.enums
{
    public enum BlogStatus
    {
        /// <summary>
        /// 草稿
        /// </summary>
        Draft = 0,
        /// <summary>
        /// 已发布
        /// </summary>
        Published = 1,
        /// <summary>
        /// 已删除
        /// </summary>
        Deleted = 2
    }
}
