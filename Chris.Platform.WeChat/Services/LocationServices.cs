using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.BaiduMap;
using Senparc.Weixin.MP.Helpers;

namespace Chris.Platform.WeChat.Services
{
    public class LocationServices
    {
        public ResponseMessageNews GetResponseMessage(RequestMessageLocation requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(requestMessage);
            #region 百度地图

            {
                var markersList = new List<BaiduMarkers>();
                markersList.Add(new BaiduMarkers
                {
                    Longitude = requestMessage.Location_X,
                    Latitude = requestMessage.Location_Y,
                    Color = "red",
                    Label = "S",
                    Size = BaiduMarkerSize.m
                });

                var mapUrl = BaiduMapHelper.GetBaiduStaticMap(requestMessage.Location_X, requestMessage.Location_Y, 1, 6, markersList);
                responseMessage.Articles.Add(new Article()
                {
                    Description = string.Format($"【来自百度地图】您刚才发送了地理位置信息。Location_X：{requestMessage.Location_X}，Location_Y：{requestMessage.Location_Y}，Scale：{requestMessage.Scale}，标签：{requestMessage.Label}"),
                    PicUrl = mapUrl,
                    Title = "定位地点周边地图",
                    Url = mapUrl
                });

                return responseMessage;
            }

            #endregion
        }
    }
}
