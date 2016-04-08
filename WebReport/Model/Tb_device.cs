using System.Data;

namespace WebReport.Model
{
    /// <summary>
    /// 设备定义
    /// </summary>
    public class TbDevice
    {
        [WebReport.Model.DataMapping("DevNum", "devicenum", DbType.Int32)]
        public int DevNum { get; set; }

        [WebReport.Model.DataMapping("Cname", "cname", DbType.String)]
        public string Cname { get; set; }
    }

}
