using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chris.Service.Location.Models.Configs;
using Chris.Service.Location.Services;
using Microsoft.AspNetCore.Mvc;
using Chris.Service.Location.Models.Enum;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Chris.Service.Location.Controllers
{
    [Route("[controller]")]
    public class TestController : Controller
    {
        private readonly BaiduMapClient _baiduMapClient;
        public TestController(BaiduMapClient baiduMapClient)
        {
            _baiduMapClient = baiduMapClient;
        }
        [HttpGet("GetLocationByIp")]
        public async Task<IActionResult> GetLocationByIp(string ak, string ip, string sn, CoorEnumOptions coor)
        {
            var data = await _baiduMapClient.GetLocationInfoByIpAsync(ak, ip, sn, coor);

            return Json(data);
        }
    }
}
