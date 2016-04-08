using System;
using System.Runtime.Serialization;

namespace WebReport.DataContact
{
    /// <summary>
    /// 更新、保存操作，前端传入数据定义
    /// </summary>
    [DataContract]
    public class QueryUpdateData
    {
        /// <summary>
        /// 采样表类型
        /// </summary>
        [DataMember]
        public string TimeType { get; set; }
        /// <summary>
        /// 设备Id
        /// </summary>
        [DataMember]
        public string DevNum { get; set; }
        /// <summary>
        /// 采样时间
        /// </summary>
        [DataMember]
        public string TimeId { get; set; }
        /// <summary>
        /// 能耗数据
        /// </summary>
        [DataMember]
        public string Val { get; set; }
    }
}
