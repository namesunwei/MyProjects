using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Chris.Service.Location.Models.Configs;
using Chris.Service.Location.Models.Entities;
using Chris.Service.Location.Models.Enum;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Chris.Service.Location.Services
{
    public class BaiduMapClient
    {
        private readonly HttpClient _httpClient;
        private readonly BaiduMapOptions _baiduMapOptions;
        private readonly ILogger _logger;
        private readonly EventId _eventInfo;
        public BaiduMapClient(IOptions<BaiduMapOptions> baiduMapOptions, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<BaiduMapClient>();
            _baiduMapOptions = baiduMapOptions.Value;
            _httpClient = new HttpClient();
            _eventInfo = new EventId(6000, "BaiduMapClient");
        }
        /// <summary>
        /// 根据IP获取定位信息
        /// </summary>
        /// <param name="ak">开发者密钥</param>
        /// <param name="ip">（可选）定位的IP地址，不填取当前地址</param>
        /// <param name="sn">用户的权限签名</param>
        /// <param name="coor">输出的坐标格式</param>
        /// <returns></returns>
        public async Task<LocationInfo> GetLocationInfoByIpAsync(string ak, string ip = null, string sn = null, CoorEnumOptions coor = CoorEnumOptions.bd09mc)
        {
            if (string.IsNullOrWhiteSpace(ak))
            {
                throw new ArgumentException("ak(开发者密钥)不能为空");
            }
            var baseUrl = _baiduMapOptions.LocationByIp;

            var sb = new StringBuilder();

            sb.Append($"?ak={ak}&coor={coor}");

            if (!string.IsNullOrWhiteSpace(ip))
            {
                sb.Append($"&ip={ip}");
            }
            if (!string.IsNullOrWhiteSpace(sn))
            {
                sb.Append($"sn={sn}");
            }

            var finalUrl = baseUrl + sb;

            try
            {
                var response = await _httpClient.GetStringAsync(finalUrl);

                var data = JsonConvert.DeserializeObject<LocationInfo>(response);

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(_eventInfo, ex, $"ErrMsg:{ex.Message},StackTrace:{ex.StackTrace}");
                throw new Exception("调用百度接口期间发生异常", ex);
            }



        }
    }
}
