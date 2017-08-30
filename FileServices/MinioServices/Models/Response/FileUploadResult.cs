using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinioServices.Models.Response
{
    public class FileUploadResult
    {
        public string UploadedFileName { get; set; }
        public string SavedFileName { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrMsg { get; set; }
    }
}
