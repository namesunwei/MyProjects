using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chris.Service.Location.Models.Enum
{
    /// <summary>
    /// 坐标类型
    /// </summary>
    public enum CoorEnumOptions
    {
        /// <summary>
        /// 百度墨卡托坐标（默认）
        /// </summary>
        bd09mc = 0,
        /// <summary>
        /// 百度经纬度坐标
        /// </summary>
        bd09ll,
        /// <summary>
        /// 国测局坐标
        /// </summary>
        gcj02
    }
}
