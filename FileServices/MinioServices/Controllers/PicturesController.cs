using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageMagick;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MinioServices.Models.ConfigModel;
using MinioServices.Models.Handler;
using MinioServices.Models.Response;
using MinioServices.Services;
using Snowflake;

namespace MinioServices.Controllers
{
    /// <summary>
    /// 图片操作相关接口
    /// </summary>
    [Route("[controller]")]
    [EnableCors("AllowSpecificOrigin"), Produces("application/json")]
    public class PicturesController : Controller
    {
        #region Init
        private readonly MinioService _minioService;
        private readonly IdWorker _idWorker;
        private readonly MinioConfigOptions _minioOptions;
        public PicturesController(MinioService minioService, IOptions<MinioConfigOptions> options)
        {
            _minioService = minioService;
            _idWorker = new IdWorker(1, 1);
            _minioOptions = options.Value;
        }

        #endregion

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <param name="bucket">存储空间名称</param>
        /// <returns></returns>
        [HttpPost("upload-batch/{bucket}")]
        public async Task<IActionResult> UploadBatchPost(string bucket)
        {
            if (!Request.HasFormContentType)
            {
                return BadRequest(new JsonResponse(ResponseErrorCode.请求参数无效, "提交表单内容不能为空。"));
            }

            if (string.IsNullOrEmpty(bucket) || bucket.Length < 3)
            {
                return BadRequest(new JsonResponse(ResponseErrorCode.请求参数无效, "bucket参数不能为空,且长度不能小于3个字符"));
            }

            var bucketIsExist = await _minioService.BucketExistsAsync(bucket);

            if (bucketIsExist != null && bucketIsExist.Value == false)
            {
                return BadRequest(new JsonResponse(ResponseErrorCode.请求参数不合法, $"{bucket}存储空间名称不存在。"));
            }

            var files = Request.Form.Files;

            var totalSize = files.Sum(f => f.Length);

            if (totalSize > _minioOptions.PictureManager.MaxAllowedContentLength)
            {
                return BadRequest(new JsonResponse(ResponseErrorCode.文件大小超出限制, $"单次上传图片总大小必须小于{_minioOptions.PictureManager.MaxAllowedContentLength / 1024 / 1024}MB！"));
            }

            var responseData = new ImagesResponse();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var extensionName = file.FileName.Split('.').LastOrDefault();

                    if (_minioOptions.PictureManager.PictureTypes.Contains(extensionName, StringComparer.CurrentCultureIgnoreCase))
                    {
                        var fileSaveedName = $"{_idWorker.NextId()}.{extensionName}";

                        using (var stream = new MemoryStream())
                        {
                            await file.CopyToAsync(stream);
                            stream.Seek(0, SeekOrigin.Begin);

                            //判断上传图片的是否通过图片校验
                            var isValid = ImageHandler.Invalid(stream);

                            if (isValid.Item1)
                            {
                                var result = await _minioService.PutObjectAsync(bucket, fileSaveedName, stream, stream.Length);
                                stream.Flush();
                                responseData.FileUploadResult.Add(new FileUploadResult
                                {
                                    IsSuccess = result,
                                    UploadedFileName = file.FileName,
                                    SavedFileName = result ? fileSaveedName : null,
                                    ErrMsg = result ? null : "500服务器内部错误。"
                                });
                            }
                            else
                            {
                                responseData.FileUploadResult.Add(new FileUploadResult
                                {
                                    IsSuccess = false,
                                    UploadedFileName = file.FileName,
                                    SavedFileName = null,
                                    ErrMsg = isValid.Item2
                                });
                            }

                        }
                    }
                    else
                    {
                        responseData.FileUploadResult.Add(new FileUploadResult
                        {
                            IsSuccess = false,
                            UploadedFileName = file.FileName,
                            SavedFileName = null,
                            ErrMsg = $"暂不支持.{extensionName}格式文件的上传。"
                        });
                    }
                }
                else
                {
                    responseData.FileUploadResult.Add(new FileUploadResult
                    {
                        IsSuccess = false,
                        UploadedFileName = file.FileName,
                        SavedFileName = null,
                        ErrMsg = "不支持空文件上传"
                    });
                }
            }

            Task.WaitAll();

            return Json(new JsonResponse<ImagesResponse>(responseData));
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="bucket">存储空间名称</param>
        /// <param name="file">文件名称</param>
        /// <param name="w">宽度</param>
        /// <param name="h">高度</param>
        /// <returns></returns>
        [HttpGet("{bucket}")]
        public async Task<IActionResult> GetPicture(string bucket, string file, int? w, int? h)
        {
            if (string.IsNullOrEmpty(bucket) || bucket.Length < 3)
            {
                return BadRequest(new JsonResponse(ResponseErrorCode.请求参数无效, "bucket参数不能为空,且长度不能小于3个字符。"));
            }
            if (string.IsNullOrWhiteSpace(file))
            {
                return BadRequest(new JsonResponse(ResponseErrorCode.请求参数无效, "fileName参数不能为空。"));
            }

            var bucketIsExist = await _minioService.BucketExistsAsync(bucket);

            if (bucketIsExist != null && bucketIsExist.Value == false)
            {
                return BadRequest(new JsonResponse(ResponseErrorCode.请求参数不合法, $"{bucket}存储空间名称不存在。"));
            }

            var extensionName = file.Split('.').LastOrDefault();

            var data = await _minioService.GetObjectAsync(bucket, file);

            if (w.HasValue || h.HasValue)
            {
                if (w.HasValue && h.HasValue)
                {
                    data = ImageHandler.ReSizeImage(data, w.Value, h.Value, true);
                }
                if (w.HasValue && !h.HasValue)
                {
                    data = ImageHandler.ReSizeImage(data, w.Value);
                }
                if (h.HasValue && !w.HasValue)
                {
                    data = ImageHandler.ReSizeImage(data, 0, h.Value);
                }
            }

            if (data != null && data.Length > 0)
            {
                if (extensionName != null) return File(data, $"image/{extensionName.ToLower()}");
            }

            return BadRequest(new JsonResponse(ResponseErrorCode.请求内容不存在, "请求内容不存在。"));
        }

        #region TEST

        [HttpGet("upload-batch")]
        public IActionResult UploadBatch()
        {
            return View();
        }

        #endregion
    }
}
