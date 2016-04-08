using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLib.ResultView
{
    /// <summary>
    /// 返回能耗数据
    /// </summary>
    public class ResultEnergyData
    {
        public string Time { get; set; }
        public int DevNum { get; set; }
        public string Cname { get; set; }
        public string EnergyName { get; set; }
        public decimal Val { get; set; }
        public int Id { get; set; }
    }

    public class ResultEnergyDataList
    {
        public ExceptionMsg Exception { get; set; }

        public List<ResultEnergyData> DevList { get; set; }
    }
    /// <summary>
    /// 月报表数据
    /// </summary>
    public class ResultEnergyMonthData
    {
        /// <summary>
        /// 上月末数据
        /// </summary>
        public decimal MonthStart { get; set; }
        /// <summary>
        /// 本月末数据
        /// </summary>
        public decimal MonthEnd { get; set; }
        public int DevNum { get; set; }
        public string Cname { get; set; }
        public string EnergyName { get; set; }
        public decimal Val { get; set; }
        public int Id { get; set; }
        //public DateTime TimeId { get; set; }
    }
    public class ResultEnergMonthyDataList
    {
        public ExceptionMsg Exception { get; set; }

        public List<ResultEnergyMonthData> DevList { get; set; }
    }

    public class ReusltPageInfo
    {
        public ExceptionMsg Exception { get; set; }
        public Model.PageInfo Page { get; set; }
    }
    /// <summary>
    /// 数据导出返回参数
    /// </summary>
    public class ReusltExport
    {
        public ExceptionMsg Exception { get; set; }
        
    }
}
