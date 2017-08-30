using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinioServices.Models.ConfigModel
{
    public class MinioConfigOptions
    {
        public string EndPonint { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public PictureManager PictureManager { get; set; }

    }

    /// <summary>
    /// 图片相关配置
    /// </summary>
    public class PictureManager
    {
        public List<string> PictureTypes { get; set; }
        public long MaxAllowedContentLength { get; set; }
    }

}
