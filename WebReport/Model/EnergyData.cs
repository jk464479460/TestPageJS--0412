using System;
using System.Data;

namespace WebReport.Model
{
    /// <summary>
    /// 采样表数据
    /// </summary>
    public  class EnergyData
    {
        [WebReport.Model.DataMapping("TiemId", "timeid", DbType.DateTime)]
        public DateTime TiemId { get; set; }

        [WebReport.Model.DataMapping("Val", "val", DbType.Decimal)]
        public decimal Val { get; set; }
    }

    public class PageInfo
    {
        [WebReport.Model.DataMapping("Num", "Num", DbType.Int32)]
        public int Num { get; set; }
    }

    public class UpdateModel
    {
        [WebReport.Model.DataMapping("A", "a", DbType.Int32)]
        public int A { get; set; }
    }
}
