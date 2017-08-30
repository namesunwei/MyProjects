using Newtonsoft.Json;

namespace Chris.Service.Location.Models.Entities
{
    /// <summary>
    /// 地区信息
    /// </summary>
    public class Content
    {
        /// <summary>
        /// 地址信息
        /// <example>河南省郑州市</example>
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 地址详细信息
        /// </summary>
        [JsonProperty("address_detail")]
        public AddressDetial AddressDetial { get; set; }
        /// <summary>
        /// 坐标信息
        /// </summary>
        public Point Point { get; set; }

    }
}
