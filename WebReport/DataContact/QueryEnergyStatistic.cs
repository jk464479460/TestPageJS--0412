using System;
using System.Runtime.Serialization;

namespace WebReport.DataContact
{
    /// <summary>
    /// 查询参数：能耗数据查询
    /// </summary>
    [DataContract]
    public class QueryEnergyStatistic
    {
        /// <summary>
        /// 设备Id
        /// </summary>
        [DataMember]
        public string DevNum { get; set; }
        /// <summary>
        /// 设备名字
        /// </summary>
        [DataMember]
        public string Cname { get; set; }
        /// <summary>
        /// 时间类型
        /// </summary>
        [DataMember]
        public int TimeType { get; set; }
        [DataMember]
        public string StartTime { get; set; }
        [DataMember]
        public string EndTime { get; set; }
        /// <summary>
        /// 能源类型
        /// </summary>
        [DataMember]
        public int EnergyType { get; set; }
        /// <summary>
        /// 分页处理
        /// </summary>
        [DataMember]
        public PagePadding Page { get; set; }
    }
    /// <summary>
    /// 分页类
    /// </summary>
    [DataContract]
    public class PagePadding
    {
        /// <summary>
        /// 当前页数
        /// </summary>
        [DataMember]
        public int CurrPage { get; set; }
        /// <summary>
        /// 单页显示条数
        /// </summary>
        [DataMember]
        public string NumAPage { get; set; }
    }
    public enum TimeType
    {
        天=1,
        月=2
    }

    public enum EnergyType
    {
        电=1,
        水=2
    }

    /// <summary>
    /// 查询参数，导出数据
    /// </summary>
    public class QueryExcelExport
    {
        /// <summary>
        /// 设备Id
        /// </summary>
        public int DevNum { get; set; }
        /// <summary>
        /// 设备名字
        /// </summary>
        public string Cname { get; set; }
        /// <summary>
        /// 时间类型
        /// </summary>
        public int TimeType { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 能源类型
        /// </summary>
        public int EnergyType { get; set; }
      
    }
}
