using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinioServices.Models.Request
{
    public class GetObjectRequestModel
    {
        public string BucketName { get; set; }
        public string ObjectName { get; set; }

    }
}
