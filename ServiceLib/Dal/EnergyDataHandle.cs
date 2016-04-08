using System;
using System.Collections.Generic;

namespace ServiceLib.Dal
{
    public class EnergyDataHandle
    {
        public static List<Model.EnergyData> GetEnergyDatas(Tuple<string, string> col, Tuple<string, string> table, Tuple<string, string> whereStr,
            Tuple<string,string> pageStr, Tuple<string,string> pageCurr )
        {
            var cmd = new DataCommand();
            var tupleList = new List<Tuple<string, string>> { col, table, whereStr, pageStr, pageCurr };
            var res = cmd.Exe<Model.EnergyData>("GetEnergyData", tupleList);
            return res;
        }

        public static List<Model.EnergyData> GetEnergyDatas(Tuple<string, string> col, 
            Tuple<string, string> table, Tuple<string, string> whereStr
       )
        {
            var cmd = new DataCommand();
            var tupleList = new List<Tuple<string, string>> { col, table, whereStr};
            var res = cmd.Exe<Model.EnergyData>("GetEnergyDataNew", tupleList);
            return res;
        }

        public static List<Model.PageInfo> GetPageInfos(Tuple<string, string> tupNum, Tuple<string,string> whereStr)
        {
            var cmd = new DataCommand();
            var tupleList = new List<Tuple<string, string>>();
            tupleList.Add(tupNum);
            tupleList.Add(whereStr);
            var res = cmd.Exe<Model.PageInfo>("GetPageNums", tupleList);
            return res;
        }

        public static void UpdateInsertData(List<Tuple<string, string>> tupList)
        {
            var cmd = new DataCommand();
            var tupleList = tupList;
            var res = cmd.Exe<Model.UpdateModel>("UpdaeInsertEnergyData", tupleList);
        }
    }

    public class EnergyMonthDataHandle
    {
        public static List<Model.EnergyData> GetEnergyMonthDatas(List<Tuple<string, string>> tupList)
        {
            var cmd = new DataCommand();
            var tupleList = tupList;
            var res = cmd.Exe<Model.EnergyData>("GetEnergyMonthData", tupleList);
            return res;
        }
    }
}
