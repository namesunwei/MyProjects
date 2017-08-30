namespace Chris.Platform.WeChat.Models.Configs
{
    public class WeChatOptions
    {
        #region 开发者信息
        /// <summary>
        /// 开发者Id
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// 开发者密码
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary> 
        #endregion

        #region 服务器配置
        /// 令牌
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 消息加解密密钥
        /// </summary>
        public string EncodingAesKey { get; set; }
        /// <summary>
        /// 微信上下文过期时间（单位：分钟）
        /// </summary>
        public double ExpireMinutes { get; set; }
        /// <summary>
        /// 每个人上下文消息储存的最大数量
        /// <remarks>设置每个人上下文消息储存的最大数量,防止内存占用过多，如果该参数小于等于0，则不限制</remarks>
        /// </summary>
        public int MaxRecordCount { get; set; }

        /// <summary>
        /// 忽略重复发送的同一条消息（通常因为微信服务器没有收到及时的响应）
        /// <remarks>默认为True</remarks>
        /// </summary>
        public bool OmitRepeatedMessage { get; set; } = true;


        #endregion
    }
}
