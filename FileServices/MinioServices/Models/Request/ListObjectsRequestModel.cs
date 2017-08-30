using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinioServices.Models.Request
{
    public class ListObjectsRequestModel
    {
        public string BucketName { get; set; }
        public string Prefix { get; set; }
        public bool? Recursive { get; set; }
    }
}
