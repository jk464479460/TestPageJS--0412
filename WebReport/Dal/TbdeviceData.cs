using System;
using System.Collections.Generic;
using WebReport.Model;
using DataCommand = WebReport.Model.DataCommand;

namespace WebReport.Dal
{
    public static class TbdeviceData
    {
        public static List<TbDevice> GetDevList(Tuple<string,string> whereStr)
        {
            var cmd = new DataCommand();
            var tupleList = new List<Tuple<string, string>> {whereStr};
            var res = cmd.Exe<TbDevice>("GetDevList", tupleList);
            return res;
        }
    }
}
