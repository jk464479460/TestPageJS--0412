using System;
using System.Data;

namespace ServiceLib.Model
{
    /// <summary>
    /// 采样表数据
    /// </summary>
    public  class EnergyData
    {
        [DataMapping("TiemId", "timeid", DbType.DateTime)]
        public DateTime TiemId { get; set; }

        [DataMapping("Val", "val", DbType.Decimal)]
        public decimal Val { get; set; }
    }

    public class PageInfo
    {
        [DataMapping("Num", "Num", DbType.Int32)]
        public int Num { get; set; }
    }

    public class UpdateModel
    {
        [DataMapping("A", "a", DbType.Int32)]
        public int A { get; set; }
    }
}
