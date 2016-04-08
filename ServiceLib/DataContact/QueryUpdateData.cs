using System;

namespace ServiceLib.DataContact
{
    /// <summary>
    /// 更新、保存操作，前端传入数据定义
    /// </summary>
    public class QueryUpdateData
    {
        /// <summary>
        /// 采样表类型
        /// </summary>
        public int TimeType { get; set; }
        /// <summary>
        /// 设备Id
        /// </summary>
        public int DevNum { get; set; }
        /// <summary>
        /// 采样时间
        /// </summary>
        public DateTime TimeId { get; set; }
        /// <summary>
        /// 能耗数据
        /// </summary>
        public decimal Val { get; set; }
    }
}
