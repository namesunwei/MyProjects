using Newtonsoft.Json;

namespace Chris.Service.Location.Models.Entities
{
    /// <summary>
    /// 地区详细信息类
    /// </summary>
    public class AddressDetial
    {
        /// <summary>
        /// 城市名
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 城市编号
        /// </summary>
        public int CityCode { get; set; }
        /// <summary>
        /// 地区名
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// 省份名
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 街道名
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// 街道编号
        /// </summary>
        [JsonProperty("street_number")]
        public string StreetNumber { get; set; }

    }
}
