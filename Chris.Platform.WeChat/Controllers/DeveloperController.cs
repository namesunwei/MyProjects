using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chris.Platform.WeChat.Models.Configs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Senparc.Weixin.MP.Containers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chris.Platform.WeChat.Controllers
{
    [Route("[Controller]")]
    public class DeveloperController : Controller
    {
        private readonly WeChatOptions _weiChatOptions;
        private readonly ILogger _logger;
        private readonly EventId _eventInfo;

        public DeveloperController(IOptions<WeChatOptions> wechatOptions, ILoggerFactory loggerFactory)
        {
            _weiChatOptions = wechatOptions.Value;
            _logger = loggerFactory.CreateLogger<DeveloperController>();
            _eventInfo = new EventId(5100, "DeveloperController");
        }
        [HttpGet("Register")]
        public IActionResult Register()
        {
            try
            {
                AccessTokenContainer.Register(_weiChatOptions.AppId, _weiChatOptions.AppSecret, "Chris_Developer");
                return Content("注册应用凭证信息成功！");
            }
            catch (Exception ex)
            {
                _logger.LogError(_eventInfo, $"ErrorMsg:{ex.Message},StackTrace:{ex.StackTrace}");
                return Content("注册应用凭证信息失败！");
            }

        }
    }
}
