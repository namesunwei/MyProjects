using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinioServices.Models.Response
{
    public enum ResponseErrorCode
    {
        请求参数无效 = 4000,
        请求参数不合法 = 40001,
        文件大小超出限制 = 4100,
        请求内容不存在 = 4200,
        服务端错误 = 5000
    }
}
