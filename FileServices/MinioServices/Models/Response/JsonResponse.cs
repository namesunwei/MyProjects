using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinioServices.Models.Response
{
    public class JsonResponse<T>
    {
        public JsonResponse(ResponseErrorCode code, string errMsg)
        {
            Code = code;
            ErrMsg = errMsg;
            Data = default(T);
        }
        public JsonResponse(T data)
        {
            Code = 0;
            Data = data;
            ErrMsg = "";
        }
        /// <summary>
        /// 错误状态码
        /// </summary>
        public ResponseErrorCode Code { get;  }
        /// <summary>
        /// 错误提示信息
        /// </summary>
        public string ErrMsg { get; }
        /// <summary>
        /// 返回的数据
        /// </summary>
        public T Data { get; }

    }

    public class JsonResponse
    {
        public JsonResponse(ResponseErrorCode code, string errMsg)
        {
            Code = code;
            ErrMsg = errMsg;
            Data = "";
        }
        public ResponseErrorCode Code { get;  }
        /// <summary>
        /// 错误提示信息
        /// </summary>
        public string ErrMsg { get;  }
        /// <summary>
        /// 返回的数据
        /// </summary>
        public string Data { get; }
    }
}
