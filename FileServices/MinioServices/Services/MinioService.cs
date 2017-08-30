using Minio;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Minio.DataModel;
using Minio.Exceptions;
using MinioServices.Models.ConfigModel;


namespace MinioServices.Services
{
    public class MinioService
    {
        private readonly MinioClient _client;
        private readonly ILogger<MinioService> _logger;
        private readonly EventId _eventInfo;
        public MinioService(IOptions<MinioConfigOptions> miniOptions, ILogger<MinioService> logger)
        {
            _logger = logger;
            _eventInfo = new EventId(5000, "MinioService");
            _client = new MinioClient(miniOptions.Value.EndPonint, miniOptions.Value.AccessKey, miniOptions.Value.SecretKey);
        }

        /// <summary>
        /// create a new Bucket
        /// </summary>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        public async Task<Tuple<bool, string>> CreateBucketAsync(string bucketName)
        {
            try
            {
                var isExist = await _client.BucketExistsAsync(bucketName);
                if (isExist)
                {
                    _logger.LogWarning($"{ bucketName } already exists !");
                    return new Tuple<bool, string>(false, $"{ bucketName } already exists !");
                }
                await _client.MakeBucketAsync(bucketName);
                _logger.LogInformation($"{ bucketName } is created successfully !");
                return new Tuple<bool, string>(true, $"{ bucketName } is created successfully !");
            }
            catch (MinioException ex)
            {
                _logger.LogError($"{ bucketName } is created unsuccessfully , ErrorMessage :{ex.Message} , StackTrace :{ex.StackTrace} !");
                return null;
            }

        }

        public async Task<ListAllMyBucketsResult> ListBucketAsync()
        {
            try
            {
                return await _client.ListBucketsAsync();
            }
            catch (MinioException ex)
            {
                _logger.LogError($" An error occurred while executing the method ListBucketAsync() ,ErrorMessage :{ex.Message} , StackTrace :{ex.StackTrace} !");
                return null;
            }
        }

        public async Task<bool?> BucketExistsAsync(string bucketName)
        {
            try
            {
                var isExists = await _client.BucketExistsAsync(bucketName);
                return isExists;
            }
            catch (MinioException ex)
            {
                _logger.LogError($" An error occurred while executing the method BucketExistsAsync() , ErrorMessage :{ex.Message} , StackTrace :{ex.StackTrace} !");
                return null;
            }
        }

        public async Task<Tuple<bool, string>> RemoveBucketAsync(string bucketName)
        {
            try
            {
                var isExists = await _client.BucketExistsAsync(bucketName);

                if (!isExists) return new Tuple<bool, string>(false, $"{bucketName} is not exist .");

                await _client.RemoveBucketAsync(bucketName);
                return new Tuple<bool, string>(true, $"Remove {bucketName} is successfully !");
            }
            catch (MinioException ex)
            {
                _logger.LogError($" An error occurred while executing the method RemoveBucketAsync() , ErrorMessage :{ex.Message} , StackTrace :{ex.StackTrace} !");
                return null;
            }
        }

        /// <summary>
        /// 获取Bucket中的所有对象
        /// </summary>
        /// <param name="bucketName">Bucket的名称</param>
        /// <param name="prefix">前缀字符串</param>
        /// <param name="recursive">当为false时，模拟目录结构，其中每个列表返回的是完整对象或对象的键的一部分，直到第一个“/”。具有相同前缀的所有对象到第一个'/'将被合并到一个条目中</param>
        /// <returns></returns>
        public async Task<IObservable<Item>> ListObjectsAsync(string bucketName, string prefix = null, bool recursive = true)
        {
            try
            {
                var isBucketExist = await _client.BucketExistsAsync(bucketName);
                if (!isBucketExist)
                {
                    _logger.LogWarning($"{bucketName} is not exist .");
                    return null;
                }

                var observable = _client.ListObjectsAsync(bucketName, prefix, recursive);
                return observable;
            }
            catch (MinioException ex)
            {
                _logger.LogError($" An error occurred while executing the method RemoveBucketAsync() , ErrorMessage :{ex.Message} , StackTrace :{ex.StackTrace} !");
                return null;
            }
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="bucketName">存储空间名称</param>
        /// <param name="objectName">存储对象名称</param>
        /// <param name="data">文件流</param>
        /// <param name="size">文件大小</param>
        /// <param name="contentType">文件类型</param>
        /// <param name="cancellationToken">撤销凭据</param>
        /// <returns></returns>
        public async Task<bool> PutObjectAsync(string bucketName, string objectName, Stream data, long size, string contentType = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                await _client.PutObjectAsync(bucketName, objectName, data, size, contentType, cancellationToken);

                return true;
            }
            catch (MinioException ex)
            {
                _logger.LogError(_eventInfo, ex, $" An error occurred while executing the method PutObjectAsync() , ErrorMessage :{ex.Message} , StackTrace :{ex.StackTrace} !");
                return false;
            }
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="bucketName">存储空间名称</param>
        /// <param name="objectName">存储对象名称</param>
        /// <param name="cancellationToken">撤销凭据</param>
        /// <returns></returns>
        public async Task<MemoryStream> GetObjectAsync(string bucketName, string objectName, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                await _client.StatObjectAsync(bucketName, objectName, cancellationToken); //判断当前对象是否存在，不存在则抛异常

                var result = new MemoryStream();

                await _client.GetObjectAsync(bucketName, objectName, stream =>
                {
                    stream.CopyTo(result);
                    result.Seek(0, SeekOrigin.Begin);
                }, cancellationToken);

                return result;
            }
            catch (MinioException ex)
            {
                _logger.LogError($" An error occurred while executing the method RemoveBucketAsync() , ErrorMessage :{ex.Message} , StackTrace :{ex.StackTrace} !");
                return null;
            }
        }

    }
}
