using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLib.ResultView
{
    public class ExceptionMsg
    {
        public bool Success { get; set; }
        public string Exsg { get; set; }
    }
    public class ResultDeviceList
    {
        public ExceptionMsg Exception { get; set; }

        public List<Model.TbDevice> DevList { get;set; } 
    }
}
