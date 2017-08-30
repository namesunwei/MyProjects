using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Minio.DataModel;
using MinioServices.Models.Request;
using MinioServices.Models.Response;
using MinioServices.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MinioServices.Controllers
{
    [Route("[controller]")]
    public class BucketsController : Controller
    {
        private readonly MinioService _minioService;

        public BucketsController(MinioService minioService)
        {
            _minioService = minioService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateBucket(string bucketName)
        {
            if (string.IsNullOrWhiteSpace(bucketName))
            {
                return BadRequest(new JsonResponse(ResponseErrorCode.请求参数无效, "请求参数bucketName不可为空!"));
            }
            var result = await _minioService.CreateBucketAsync(bucketName.ToLower());

            if (result == null)
            {
                return NotFound();
            }
            return result.Item1 ? Json(new JsonResponse<bool>(true)) : Json(new JsonResponse(ResponseErrorCode.服务端错误, result.Item2));
        }
        [HttpGet("collections")]
        public async Task<IActionResult> GetAllMyBucket()
        {
            var result = await _minioService.ListBucketAsync();

            if (result == null)
            {
                return NotFound();
            }
            return Json(new JsonResponse<ListAllMyBucketsResult>(result));
        }
        [HttpGet("exists")]
        public async Task<IActionResult> IsBucketExists(string bucketName)
        {
            var result = await _minioService.BucketExistsAsync(bucketName);
            if (result == null)
            {
                return NotFound();
            }

            return Json(new JsonResponse<bool>(result.Value));
        }
        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveBucket(string bucketName)
        {
            var result = await _minioService.RemoveBucketAsync(bucketName);

            if (result == null)
            {
                return NotFound();
            }

            return Json(!result.Item1 ? new JsonResponse<bool>(ResponseErrorCode.请求参数无效, result.Item2) : new JsonResponse<bool>(result.Item1));
        }

        [HttpPost("object/collections")]
        public async Task<IActionResult> GetAllObjectList([FromBody]ListObjectsRequestModel req)
        {
            IObservable<Item> result;

            if (req.Recursive.HasValue)
            {
                result = await _minioService.ListObjectsAsync(req.BucketName, req.Prefix, req.Recursive.Value);
            }
            else
            {
                result = await _minioService.ListObjectsAsync(req.BucketName, req.Prefix);
            }

            if (result == null)
            {
                return NotFound();
            }

            return Json(new JsonResponse<IObservable<Item>>(result));
        }
        ////[HttpGet("{bucket}/{object}")]
        ////public async Task<IActionResult> GetObject(string bucket, string @object)
        ////{
        ////    var result = await _minioService.GetObjectAsync(bucket, @object);
        ////    if (result == null)
        ////    {
        ////        return NotFound();
        ////    }
        ////    // return 
        ////}

    }
}
