using Chris.Platform.WeChat.Models.Configs;
using Chris.Platform.WeChat.Models.MessageHandler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities.Request;
using System;
using System.IO;
using Chris.Platform.WeChat.Services;
using Newtonsoft.Json;
using Senparc.Weixin.MP.CoreMvcExtension;

namespace Chris.Platform.WeChat.Controllers
{
    [Route("[Controller]")]
    public class WeChatController : Controller
    {
        #region Init
        private readonly WeChatOptions _weChatOptions;
        private readonly ILogger _logger;
        private readonly EventId _eventInfo;
        public WeChatController(IOptions<WeChatOptions> options, ILoggerFactory loggerFactory)
        {
            _weChatOptions = options.Value;
            _logger = loggerFactory.CreateLogger<WeChatController>();
            _eventInfo = new EventId(5200, "WeChatController");
        }
        #endregion

        [HttpGet("Index")]
        public IActionResult Get(PostModel postModel, string echostr)
        {
            var requstParams = "";
            if (postModel != null)
            {
                requstParams = JsonConvert.SerializeObject(postModel);
            }

            if (postModel != null && CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, _weChatOptions.Token))
            {

                _logger.LogInformation($"微信开发服务器认证成功!,requstParams:{requstParams}");
                return Content(echostr);  //返回随机字符串则表示验证通过
            }

            _logger.LogError($"微信开发服务器认证失败 , requstParams:{requstParams}!");

            return Content($"failed:{postModel?.Signature},{CheckSignature.GetSignature(postModel?.Timestamp, postModel?.Nonce, _weChatOptions.Token)}。如果你在浏览器中看到这句话，说明此地址可以被作为微信公众账号后台的Url，请注意保持Token一致。");

        }

        [HttpPost("Index")]
        public ContentResult Post(PostModel postModel)
        {
            _logger.LogInformation(_eventInfo, "服务器接收到微信转发的请求。");

            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, _weChatOptions.Token))
            {
                _logger.LogError("参数错误!");
                return Content("参数错误！");
            }

            //打包PostModel信息
            postModel.Token = _weChatOptions.Token;
            postModel.EncodingAESKey = _weChatOptions.EncodingAesKey;
            postModel.AppId = _weChatOptions.AppId;

            var data = new MemoryStream();
            Request.Body.CopyTo(data);
            data.Seek(0, SeekOrigin.Begin);

            var messageHandler = new CustomMessageHandler(data, postModel, _weChatOptions.MaxRecordCount)
            {
                WeixinContext =
                {
                    ExpireMinutes = _weChatOptions.ExpireMinutes //设置微信上下文过期时间
                },

                //忽略重复发送的同一条消息（通常因为微信服务器没有收到及时的响应）
                OmitRepeatedMessage = _weChatOptions.OmitRepeatedMessage  //默认已经开启，此处仅作为演示，也可以设置为false在本次请求中停用此功能
            };

            try
            {
                //处理微信请求
                messageHandler.Execute();

                //响应微信请求
                //return new ContentResult(messageHandler);
                return new FixWeixinBugWeixinResult(messageHandler);
            }
            catch (Exception ex)
            {
                _logger.LogError(_eventInfo, $"MessageHandle发生错误：{ex.Message},StackTrace:{ex.StackTrace}");
                return Content("服务器发生错误！");
            }
        }
    }
}
