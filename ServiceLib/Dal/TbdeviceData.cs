using System;
using System.Collections.Generic;

namespace ServiceLib.Dal
{
    public static class TbdeviceData
    {
        public static List<Model.TbDevice> GetDevList(Tuple<string,string> whereStr)
        {
            var cmd = new DataCommand();
            var tupleList = new List<Tuple<string, string>> {whereStr};
            var res = cmd.Exe<Model.TbDevice>("GetDevList", tupleList);
            return res;
        }
    }
}
