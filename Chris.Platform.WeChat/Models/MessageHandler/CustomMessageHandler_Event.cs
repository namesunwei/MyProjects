using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.MessageHandlers;

namespace Chris.Platform.WeChat.Models.MessageHandler
{
    public partial class CustomMessageHandler 
    {
        #region Event Handle

        /// <summary>
        /// 处理点击事件请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            return base.OnEvent_ClickRequest(requestMessage);
        }

        /// <summary>
        /// 处理Enter(进入)事件请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_EnterRequest(RequestMessageEvent_Enter requestMessage)
        {
            return base.OnEvent_EnterRequest(requestMessage);
        }

        /// <summary>
        /// 处理Location(位置)事件请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        {
            return base.OnEvent_LocationRequest(requestMessage);
        }

        /// <summary>
        /// 打开网页事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ViewRequest(RequestMessageEvent_View requestMessage)
        {
            return base.OnEvent_ViewRequest(requestMessage);
        }

        /// <summary>
        /// 订阅（关注）事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            return base.OnEvent_SubscribeRequest(requestMessage);
        }

        /// <summary>
        /// 取消订阅（关注）事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {
            return base.OnEvent_UnsubscribeRequest(requestMessage);
        }

        /// <summary>
        ///  通过二维码扫描关注扫描事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ScanRequest(RequestMessageEvent_Scan requestMessage)
        {
            return base.OnEvent_ScanRequest(requestMessage);
        }

        /// <summary>
        /// 扫码推事件（ScanCode_Push)
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ScancodePushRequest(RequestMessageEvent_Scancode_Push requestMessage)
        {
            return base.OnEvent_ScancodePushRequest(requestMessage);
        }

        /// <summary>
        /// 扫码推事件并且弹出“消息接收中”提示框（ScanCode_WaitMsg）
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_ScancodeWaitmsgRequest(RequestMessageEvent_Scancode_Waitmsg requestMessage)
        {
            return base.OnEvent_ScancodeWaitmsgRequest(requestMessage);
        }

        /// <summary>
        /// 弹出系统拍照发图事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_PicSysphotoRequest(RequestMessageEvent_Pic_Sysphoto requestMessage)
        {
            return base.OnEvent_PicSysphotoRequest(requestMessage);
        }

        /// <summary>
        /// 弹出微信相册发图事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_PicWeixinRequest(RequestMessageEvent_Pic_Weixin requestMessage)
        {
            return base.OnEvent_PicWeixinRequest(requestMessage);
        }

        /// <summary>
        /// 发送模板消息返回结果
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_TemplateSendJobFinishRequest(RequestMessageEvent_TemplateSendJobFinish requestMessage)
        {
            return base.OnEvent_TemplateSendJobFinishRequest(requestMessage);
        }

        /// <summary>
        /// 弹出地理位置选择器事件
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_LocationSelectRequest(RequestMessageEvent_Location_Select requestMessage)
        {
            return base.OnEvent_LocationSelectRequest(requestMessage);
        }

        #endregion
    }
}
