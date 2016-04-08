using System.Collections.Generic;
using TbDevice = WebReport.Model.TbDevice;

namespace WebReport.ResultView
{
    public class ExceptionMsg
    {
        public bool Success { get; set; }
        public string Exsg { get; set; }
    }
    public class ResultDeviceList
    {
        public ExceptionMsg Exception { get; set; }

        public List<TbDevice> DevList { get;set; } 
    }
}
