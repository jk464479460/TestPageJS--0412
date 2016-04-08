using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using WebReport.Dal;
using WebReport.DataContact;
using EnergyData = WebReport.Model.EnergyData;
using ExceptionMsg = WebReport.ResultView.ExceptionMsg;
using IEnergyDataBll = WebReport.Interface.IEnergyDataBll;
using PageInfo = WebReport.Model.PageInfo;
using ResultEnergyData = WebReport.ResultView.ResultEnergyData;
using ResultEnergyDataList = WebReport.ResultView.ResultEnergyDataList;
using ReusltExport = WebReport.ResultView.ReusltExport;
using ReusltPageInfo = WebReport.ResultView.ReusltPageInfo;
using ReusltSavingData = WebReport.ResultView.ReusltSavingData;
using TtileRow = WebReport.Model.TtileRow;

namespace WebReport.Bll
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class EnergyDataBll : IEnergyDataBll
    {
        private readonly HttpContext _context = HttpContext.Current;
        public string GetEnergyDataList(QueryEnergyStatistic queryObj)
        {
            var query = _context.Request.Form["inputs"];
            var result = new ResultEnergyDataList();
            try
            {
                var obj = queryObj;//JsonConvert.DeserializeObject<QueryEnergyStatistic>(query);
                var devNum = obj.DevNum;
                /*组织参数*/
                var tableStr = "TS_#time#_#devid#";
                if (obj.TimeType == (int)TimeType.天)
                    tableStr = tableStr.Replace("#time#", "h1");
                else if (obj.TimeType == (int)TimeType.月)
                    tableStr = tableStr.Replace("#time#", "day");
                tableStr = tableStr.Replace("#devid#", GetTbNum(int.Parse(devNum)));

                var table = new Tuple<string, string>("#table#", tableStr.ToString());
                var col = new Tuple<string, string>("#col#", "v" +GetCol(int.Parse(devNum)));
                var where = " and timeid>='"+obj.StartTime+"' and timeid<='"+DateTime.Parse(obj.EndTime).ToString("yyyy-MM-dd 23:00:00")+"'";
                var whereStr = new Tuple<string, string>("#andStr#", where);
                var pageStr = new Tuple<string, string>("#NumAPage#",obj.Page.NumAPage.ToString());
                var pageCurr = new Tuple<string, string>("#CurrPage#", obj.Page.CurrPage.ToString());
                var res = EnergyDataHandle.GetEnergyDatas(col, table, whereStr,pageStr,pageCurr);
                result.DevList=new List<ResultEnergyData>();
                var i = 0;
                var baseId = (int.Parse(obj.Page.NumAPage))*(obj.Page.CurrPage-1);
                foreach (EnergyData p in res)
                {
                    var resultEnergyData = new ResultEnergyData();
                    resultEnergyData.Cname = obj.Cname;
                    resultEnergyData.DevNum = int.Parse(obj.DevNum);
                    resultEnergyData.EnergyName = ((EnergyType)obj.EnergyType).ToString();
                    resultEnergyData.Time = p.TiemId.ToString("yyyy-MM-dd HH:mm:ss");
                    resultEnergyData.Val = decimal.Round(p.Val,2);
                    resultEnergyData.Id = i++;
                    resultEnergyData.Id += baseId;
                    result.DevList.Add(resultEnergyData);
                    
                }
                result.Exception = new ExceptionMsg { Exsg = "", Success = true };
            }
            catch (Exception ed)
            {
                result.Exception=new ExceptionMsg{Exsg=ed.Message, Success=false};
            }

            return JsonConvert.SerializeObject(result);
        }


        public string GetPageInfo(QueryEnergyStatistic queryObj)
        {
            var result = new ReusltPageInfo();
            var query = _context.Request.Form["inputs"];
            try
            {
                var obj = queryObj;//JsonConvert.DeserializeObject<QueryEnergyStatistic>(query);
                var devNum = obj.DevNum;
                
                /*组织参数*/
                var tableStr = "TS_#time#_#devid#";
                if (obj.TimeType == (int)TimeType.天)
                    tableStr = tableStr.Replace("#time#", "h1");
                else if (obj.TimeType == (int)TimeType.月)
                    tableStr = tableStr.Replace("#time#", "h1");
                tableStr = tableStr.Replace("#devid#", GetTbNum(int.Parse(devNum)));
                var where = " and timeid>='" + obj.StartTime + "' and timeid<='" + (DateTime.Parse(obj.EndTime)).ToString("yyyy-MM-dd 23:00:00") + "'";
                var whereStr = new Tuple<string, string>("#andStr#", where);
                var res = EnergyDataHandle.GetPageInfos(new Tuple<string, string>("#table#", tableStr), whereStr);
                result.Page = res.Count==0 ? new PageInfo() : res[0];
                result.Page.Num = (result.Page.Num / (int.Parse(obj.Page.NumAPage)) >= 0 && result.Page.Num %(int.Parse(obj.Page.NumAPage))>0 ? result.Page.Num / (int.Parse(obj.Page.NumAPage)) + 1
                    : result.Page.Num/int.Parse(obj.Page.NumAPage));
                result.Exception = new ExceptionMsg { Exsg = "", Success = true };
            }
            catch (Exception ed)
            {
                result.Exception=new ExceptionMsg{Exsg=ed.Message, Success=false};
            }
            return JsonConvert.SerializeObject(result);
        }


        public string UpdateInsertEnergyData(QueryUpdateData queryObj)
        {
            var result = new ReusltSavingData();
            var query = _context.Request.Form["inputs"];
            try
            {
                var obj = queryObj;//JsonConvert.DeserializeObject<QueryUpdateData>(query);
                var devNum = obj.DevNum;
                
                var tableStr = "TS_#time#_#devid#";
                if (int.Parse(obj.TimeType) == (int)TimeType.天)
                    tableStr = tableStr.Replace("#time#", "h1");
                else if (int.Parse(obj.TimeType) == (int)TimeType.月)
                    tableStr = tableStr.Replace("#time#", "day");
                tableStr = tableStr.Replace("#devid#", GetTbNum(int.Parse(devNum)));
                var table = new Tuple<string, string>("#table#", tableStr.ToString());
                var where = " and timeid='" + obj.TimeId+"'";
                var whereStr = new Tuple<string, string>("#andStr#", where);
                var col = new Tuple<string, string>("#col#", "v" + GetCol(Int32.Parse(devNum)));
                var colVal = new Tuple<string, string>("#colVal#",obj.Val.ToString());
                var timeidStr = new Tuple<string, string>("#timeid#",DateTime.Parse(obj.TimeId).ToString("yyyy-MM-dd HH:mm:ss"));
                var tupList = new List<Tuple<string, string>>{table,whereStr,col,colVal,timeidStr};
                EnergyDataHandle.UpdateInsertData(tupList);
                result.Exception = new ExceptionMsg { Exsg = "", Success = true };
            }
            catch (Exception ed)
            {
                result.Exception = new ExceptionMsg { Exsg = ed.Message, Success = false };
            }
            return JsonConvert.SerializeObject(result);
        }


        public string ExportExcel()
        {
            var result = new ReusltExport();
            var query = _context.Request.Form["inputs"];
            try
            {
                var obj = JsonConvert.DeserializeObject<QueryExcelExport>(query);
                obj.EndTime = obj.EndTime.AddHours(23);
                var devNum = obj.DevNum;
                
                /*组织参数*/
                var tableStr = "TS_#time#_#devid#";
                if (obj.TimeType == (int)TimeType.天)
                    tableStr = tableStr.Replace("#time#", "h1");
                else if (obj.TimeType == (int)TimeType.月)
                    tableStr = tableStr.Replace("#time#", "day");
                tableStr = tableStr.Replace("#devid#", GetTbNum(devNum));

                var table = new Tuple<string, string>("#table#", tableStr.ToString());
                var col = new Tuple<string, string>("#col#", "v" + GetCol(devNum));
                var where = " and timeid>='" + obj.StartTime + "' and timeid<='" + obj.EndTime.ToString("yyyy-MM-dd 23:00:00") + "'";
                var whereStr = new Tuple<string, string>("#andStr#", where);
               
                var res = EnergyDataHandle.GetEnergyDatas(col, table, whereStr);
                var dataList = new List<ResultEnergyData>();
                var i = 0;
                
                foreach (EnergyData p in res)
                {
                    var resultEnergyData = new ResultEnergyData();
                    resultEnergyData.Cname = obj.Cname;
                    resultEnergyData.DevNum = obj.DevNum;
                    resultEnergyData.EnergyName = ((EnergyType)obj.EnergyType).ToString();
                    resultEnergyData.Time = p.TiemId.ToString("yyyy-MM-dd HH:mm:ss");
                    resultEnergyData.Val = decimal.Round(p.Val, 2);
                    resultEnergyData.Id = i++;
                    dataList.Add(resultEnergyData);

                }
                /*excel 处理业务*/
                var dt = new DataTable();
                dt.Columns.Add(new DataColumn("序号",typeof(int)));
                dt.Columns.Add(new DataColumn("时间", typeof(string)));
                dt.Columns.Add(new DataColumn("名称", typeof(string)));
                dt.Columns.Add(new DataColumn("电数据", typeof(decimal)));

               
                var index = 0;
                for (var time = obj.StartTime; time <= obj.EndTime; )
                {
                    var row = dt.NewRow();
                    DateTime time1 = time;
                    var energyName = "";
                    var val = 0m;
                    var temp = (from p in dataList where time1 == DateTime.Parse(p.Time) select p).ToList();
                    if (temp.Count > 0)
                    {
                        energyName=temp[0].EnergyName;
                        val = temp[0].Val;
                    }
                    else
                    {
                        energyName = ((EnergyType) obj.EnergyType).ToString();
                        val = 0m;
                    }
                    row[0] = index++;
                    row[1] = time.ToString("yyyy-MM-dd HH:mm:ss");
                    row[2] = energyName;
                    row[3] = val;
                    dt.Rows.Add(row);
                    time = obj.EnergyType == (int) TimeType.天 ? time.AddHours(1) : time.AddYears(1);
                }
                var tileRow = new List<TtileRow>();
                tileRow.Add(new TtileRow{C=0,R=0, Description=obj.Cname});
                tileRow.Add(new TtileRow { C = 0, R = 3, Description = obj.StartTime.ToString()+"~"+obj.EndTime.ToString() });
                tileRow.Add(new TtileRow{C=3,R=4});

                var tempPath = AppDomain.CurrentDomain.BaseDirectory + "temp_file\\";
                var savePath = DateTime.Now.Ticks + ".xls";
                var templatePath = AppDomain.CurrentDomain.BaseDirectory + "templatefiles\\综合报表.xls";
                WebReport.Bll.ExportExcelHandle.ExportExcel(dt,tempPath+savePath, templatePath, tileRow);
                result.Exception = new ExceptionMsg { Exsg = "/temp_file/" + savePath, Success = true };
            }
            catch (Exception ed)
            {
                result.Exception = new ExceptionMsg { Exsg = ed.Message, Success = false }; 
            }
            return JsonConvert.SerializeObject(result);
        }

        public static string GetTbNum(int devNum)
        {
            var r = devNum/128;
            r= r >= 1 ? r - 1 : r;
            var tbBuild = new StringBuilder();
            tbBuild.Append("000");
            if (r >= 100)
                tbBuild = tbBuild.Replace("000", r.ToString());
            else if (r >= 10)
                tbBuild = tbBuild.Replace("00", r.ToString(), 1, 2);
            else
            {
                tbBuild = tbBuild.Replace("0", r.ToString(), 2, 1);
            }
            return tbBuild.ToString();
        }

        public static string GetCol(int devNum)
        {
            var c = devNum%128;
            c= c == 0 ? 128 : c;
            var colBuild = new StringBuilder();
            colBuild.Append("000");
            
            if (c >= 100)
                colBuild = colBuild.Replace("000", c.ToString());
            else if (c >= 10)
                colBuild = colBuild.Replace("00", c.ToString(), 1, 2);
            else
            {
                colBuild = colBuild.Replace("0", c.ToString(), 2, 1);
            }
            return colBuild.ToString();
        }
    }
}
