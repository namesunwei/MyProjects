using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinioServices.Models.Response
{
    public class ImagesResponse
    {
        public ImagesResponse()
        {
            FileUploadResult = new List<FileUploadResult>();
        }
        public List<FileUploadResult> FileUploadResult { get; set; }
    }


}
