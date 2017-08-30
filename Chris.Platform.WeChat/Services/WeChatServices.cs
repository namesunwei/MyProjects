using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chris.Framework.Infrastructure.Extensions;
using Chris.Platform.WeChat.Models.Configs;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;

namespace Chris.Platform.WeChat.Services
{
    public class WeChatServices
    {
        private readonly ILogger _logger;
        private readonly EventId _eventInfo;
        private readonly WeChatOptions _weChatOptions;
        private readonly IDistributedCache _redisCache;
        public WeChatServices(IOptions<WeChatOptions> wechatOptions, ILoggerFactory loggerFactory, IDistributedCache cache)
        {
            _logger = loggerFactory.CreateLogger<WeChatServices>();
            _weChatOptions = wechatOptions.Value;
            _eventInfo = new EventId(5000, "WeChatServices");
            _redisCache = cache;
        }

        /// <summary>
        /// 获取AccessToken信息
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetToken()
        {
            var redisData = await _redisCache.GetStringAsync("AccessToken");
            if (redisData.IsNotNull())
            {
                return redisData;
            }

            var result = await CommonApi.GetTokenAsync(_weChatOptions.AppId, _weChatOptions.AppSecret);

            await _redisCache.SetStringAsync("AccessToken", result.access_token, new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.UtcNow.AddSeconds(7000)
                //AbsoluteExpirationRelativeToNow
                //SlidingExpiration
            });

            return result.access_token;
        }
    }
}
