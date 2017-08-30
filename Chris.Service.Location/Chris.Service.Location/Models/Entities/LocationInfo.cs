namespace Chris.Service.Location.Models.Entities
{
    public class LocationInfo
    {
        /// <summary>
        /// 地区简述
        /// <example>"CN|河南|郑州|None|CERNET|1|None"</example>
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 地区信息
        /// </summary>
        public Content Content { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public int Status { get; set; }
        public string Message { get; set; }


    }
}
