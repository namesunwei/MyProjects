using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chris.Platform.WeChat.Models.Configs;
using Chris.Platform.WeChat.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Menu;

namespace Chris.Platform.WeChat.Controllers
{
    [Obsolete("个人公众号暂不支持")]
    [Route("[controller]")]
    public class MenuController : Controller
    {
        private readonly WeChatOptions _weiChatOptions;
        private readonly WeChatServices _weChatServices;
        public MenuController(IOptions<WeChatOptions> wechatOptions, WeChatServices weChatServices)
        {
            _weiChatOptions = wechatOptions.Value;
            _weChatServices = weChatServices;
        }
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var token = await _weChatServices.GetToken();
            var result = CommonApi.GetMenu(token);

            return View(result);
        }

    }
}
