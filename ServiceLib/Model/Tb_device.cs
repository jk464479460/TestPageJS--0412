using System.Data;

namespace ServiceLib.Model
{
    /// <summary>
    /// 设备定义
    /// </summary>
    public class TbDevice
    {
        [DataMapping("DevNum", "devicenum", DbType.Int32)]
        public int DevNum { get; set; }

        [DataMapping("Cname", "cname", DbType.String)]
        public string Cname { get; set; }
    }

}
