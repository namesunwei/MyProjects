using System.IO;
using Chris.Platform.WeChat.Models.Configs;
using Chris.Platform.WeChat.Models.MessageModels;
using Chris.Platform.WeChat.Services;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;

namespace Chris.Platform.WeChat.Models.MessageHandler
{
    /// <summary>
    /// 自定义消息处理类
    /// 把MessageHandler作为基类，重写对应请求的处理方法
    /// </summary>
    public partial class CustomMessageHandler : MessageHandler<CustomMessageContext>
    {
        #region Init

        public CustomMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0) : base(inputStream, postModel, maxRecordCount)
        {
            //在指定条件下，不使用消息去重
            OmitRepeatedMessageFunc = requestMessage => !(RequestMessage is RequestMessageText textRequestMessage) || textRequestMessage.Content != "容错";
        }

        #endregion

        #region Filter

        /// <summary>
        /// 消息处理前方法
        /// </summary>
        public override void OnExecuting()
        {
            if (CurrentMessageContext.StorageData == null)
            {
                CurrentMessageContext.StorageData = 0;
            }
            base.OnExecuting();
        }
        /// <summary>
        /// 消息处理后方法
        /// </summary>
        public override void OnExecuted()
        {
            base.OnExecuted();
            CurrentMessageContext.StorageData = (int)CurrentMessageContext.StorageData + 1;
        }

        #endregion

        #region Handler

        /// <summary>
        /// 预处理文字或事件请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnTextOrEventRequest(RequestMessageText requestMessage)
        {
            return base.OnTextOrEventRequest(requestMessage);
        }

        /// <summary>
        /// 处理文字请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您的OpenID是：" + requestMessage.FromUserName      //这里的requestMessage.FromUserName也可以直接写成base.WeixinOpenId
                                      + "。\r\n您发送了文字信息：" + requestMessage.Content;  //\r\n用于换行，requestMessage.Content即用户发过来的文字内容
            return responseMessage;
        }

        /// <summary>
        /// 处理图片请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        {
            // return base.OnImageRequest(requestMessage);
            var responseData = CreateResponseMessage<ResponseMessageNews>();
            responseData.Articles.Add(new Article
            {
                Title = "您刚才发送了图片信息",
                Description = "您发送的图片将会显示在边上",
                PicUrl = requestMessage.PicUrl,
                Url = "http://www.baidu.com"
            });
            responseData.Articles.Add(new Article()
            {
                Title = "第二条",
                Description = "第二条带连接的内容",
                PicUrl = requestMessage.PicUrl,
                Url = "https://www.cnblogs.com/"
            });
            return responseData;
        }

        /// <summary>
        /// 处理链接地址请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnLinkRequest(RequestMessageLink requestMessage)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            responseMessage.Content =
                $@"您发送了一条连接信息：Title：{requestMessage.Title},Description:{requestMessage.Description},Url:{
                        requestMessage.Url
                    }";
            return responseMessage;
        }
                                                       
        /// <summary>
        /// 处理位置类型的请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        {
            var locationServices = new LocationServices();
            var responseMessage = locationServices.GetResponseMessage(requestMessage);
            return responseMessage;
        }

        /// <summary>
        /// 处理小视频类型的请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnShortVideoRequest(RequestMessageShortVideo requestMessage)
        {
            return base.OnShortVideoRequest(requestMessage);
        }

        /// <summary>
        /// 处理视频类型的请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnVideoRequest(RequestMessageVideo requestMessage)
        {
            return base.OnVideoRequest(requestMessage);
        }

        /// <summary>
        /// 处理语音类型的请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        {
            return base.OnVoiceRequest(requestMessage);
        }

        /// <summary>
        /// 默认消息处理方法
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            var responseData = CreateResponseMessage<ResponseMessageText>();
            responseData.Content = MessageHelper.DefaultMessage;
            return responseData;
        }

        #endregion
    }
}
