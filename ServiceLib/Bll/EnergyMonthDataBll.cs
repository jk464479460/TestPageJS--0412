using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;
using System.Web.UI;
using Newtonsoft.Json;
using ServiceLib.Dal;
using ServiceLib.DataContact;
using ServiceLib.Interface;
using ServiceLib.Model;
using ServiceLib.ResultView;

namespace ServiceLib.Bll
{
    /// <summary>
    /// 导出月报表数据
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class EnergyMonthDataBll : IEnergyMonthDataBll
    {
        private readonly HttpContext _context = HttpContext.Current;
        public string GetEnergyDataList(string Inputs)
        {
            var query = _context.Request.Form["inputs"];
            var result = new ResultView.ResultEnergMonthyDataList {DevList = new List<ResultEnergyMonthData>()};
            try
            {
                var obj = JsonConvert.DeserializeObject<QueryEnergyStatistic>(query);
                var devList = TbdeviceData.GetDevList(new Tuple<string, string>("#andStr#", ""));
                var id = 0;
                foreach (var dev in devList)
                {
                    var devNum = dev.DevNum;

                    /*组织参数*/
                    var tableStr = "TS_#time#_#devid#";
                    tableStr = tableStr.Replace("#time#", "h1");
                    tableStr = tableStr.Replace("#devid#", EnergyDataBll.GetTbNum(devNum));

                    var table = new Tuple<string, string>("#table#", tableStr.ToString());
                    var col = new Tuple<string, string>("#col#", "v" + EnergyDataBll.GetCol(devNum));

                    var monthStart = obj.StartTime;//查询小时
                    var monthEnd = obj.EndTime;//monthStart.AddHours(1);//下一个小时

                    obj.StartTime = monthStart;
                    obj.EndTime = monthEnd;

                    var whereStart = " and timeid='" + obj.StartTime + "'";
                    var whereEnd = " and timeid='" + obj.EndTime + "'";

                    var whereStr = new Tuple<string, string>("#andStr#", whereStart);
                    var tupList = new List<Tuple<string, string>> {whereStr,col,table};
                    var res = EnergyMonthDataHandle.GetEnergyMonthDatas(tupList);
                    var valStart = 0m;
                    var valEnd=0m;
                    if (res != null && res.Count>0)
                        valStart = res[0].Val;
                    whereStr = new Tuple<string, string>("#andStr#", whereEnd);
                    tupList = new List<Tuple<string, string>> { whereStr, col, table };
                    res = EnergyMonthDataHandle.GetEnergyMonthDatas(tupList);
                    if (res != null && res.Count > 0)
                        valEnd = res[0].Val;
                    var val = valEnd - valStart;
                    var tempObj = new ResultEnergyMonthData
                    {
                        Cname = dev.Cname,
                        DevNum = dev.DevNum,
                        EnergyName = ((EnergyType) 1).ToString(),
                        MonthStart = valStart,
                        MonthEnd = valEnd,
                        Val = val,
                        Id = id++
                    };
                    result.DevList.Add(tempObj);
                }
                result.Exception = new ExceptionMsg { Exsg = "", Success = true };
                obj.Page.CurrPage = obj.Page.CurrPage - 1;
                result.DevList = (result.DevList.Skip(obj.Page.CurrPage*obj.Page.NumAPage)).Take(obj.Page.NumAPage).ToList();
            }
            catch (Exception ed)
            {
                result.Exception = new ExceptionMsg { Exsg = ed.Message, Success = false };
            }
            
            return JsonConvert.SerializeObject(result);
        }

        public string GetPageInfo(string Inputs)
        {
            var query = _context.Request.Form["inputs"];
            var result = new ResultView.ResultEnergMonthyDataList { DevList = new List<ResultEnergyMonthData>() };
            var result2 = new ReusltPageInfo();
            try
            {
                var obj = JsonConvert.DeserializeObject<QueryEnergyStatistic>(query);
                var devList = TbdeviceData.GetDevList(new Tuple<string, string>("#andStr#", ""));
                var id = 0;
                foreach (var dev in devList)
                {
                    var devNum = dev.DevNum;

                    /*组织参数*/
                    var tableStr = "TS_#time#_#devid#";
                    tableStr = tableStr.Replace("#time#", "h1");
                    tableStr = tableStr.Replace("#devid#", EnergyDataBll.GetTbNum(devNum));

                    var table = new Tuple<string, string>("#table#", tableStr.ToString());
                    var col = new Tuple<string, string>("#col#", "v" + EnergyDataBll.GetCol(devNum));

                    var monthStart = obj.StartTime;//查询小时
                    var monthEnd = obj.EndTime;//monthStart.AddHours(1);//下一个小时

                    obj.StartTime = monthStart;
                    obj.EndTime = monthEnd;

                    var whereStart = " and timeid='" + obj.StartTime + "'";
                    var whereEnd = " and timeid='" + obj.EndTime + "'";

                    var whereStr = new Tuple<string, string>("#andStr#", whereStart);
                    var tupList = new List<Tuple<string, string>> { whereStr, col, table };
                    var res = EnergyMonthDataHandle.GetEnergyMonthDatas(tupList);
                    var valStart = 0m;
                    var valEnd = 0m;
                    if (res != null && res.Count > 0)
                        valStart = res[0].Val;
                    whereStr = new Tuple<string, string>("#andStr#", whereEnd);
                    tupList = new List<Tuple<string, string>> { whereStr, col, table };
                    res = EnergyMonthDataHandle.GetEnergyMonthDatas(tupList);
                    if (res != null && res.Count > 0)
                        valEnd = res[0].Val;
                    var val = valEnd - valStart;
                    var tempObj = new ResultEnergyMonthData
                    {
                        Cname = dev.Cname,
                        DevNum = dev.DevNum,
                        EnergyName = ((EnergyType)1).ToString(),
                        MonthStart = valStart,
                        MonthEnd = valEnd,
                        Val = val,
                        Id = id++
                    };
                    result.DevList.Add(tempObj);
                }
                result2.Page = result.DevList.Count == 0 ? new PageInfo() : new PageInfo { Num = result.DevList.Count };
                result2.Page.Num = (result2.Page.Num / obj.Page.NumAPage >= 0 && result2.Page.Num % obj.Page.NumAPage > 0 ? result2.Page.Num / obj.Page.NumAPage + 1
                    : result2.Page.Num / obj.Page.NumAPage);
                result2.Exception = new ExceptionMsg { Exsg = "", Success = true };
            }
            catch (Exception ed)
            {
                result2.Exception = new ExceptionMsg { Exsg = ed.Message, Success = false };
            }
            
            return JsonConvert.SerializeObject(result2);
        }

        public string ExportExcel()
        {
            var result = new ReusltExport();
            var result1 = new ResultView.ResultEnergMonthyDataList { DevList = new List<ResultEnergyMonthData>() };
            var query = _context.Request.Form["inputs"];
            try
            {

                #region 数据获取
                var obj = JsonConvert.DeserializeObject<QueryEnergyStatistic>(query);
                var devList = TbdeviceData.GetDevList(new Tuple<string, string>("#andStr#", ""));
                var id = 0;
                foreach (var dev in devList)
                {
                    var devNum = dev.DevNum;

                    /*组织参数*/
                    var tableStr = "TS_#time#_#devid#";
                    tableStr = tableStr.Replace("#time#", "h1");
                    tableStr = tableStr.Replace("#devid#", EnergyDataBll.GetTbNum(devNum));

                    var table = new Tuple<string, string>("#table#", tableStr.ToString());
                    var col = new Tuple<string, string>("#col#", "v" + EnergyDataBll.GetCol(devNum));

                    var monthStart = obj.StartTime;//查询小时
                    var monthEnd = obj.EndTime;//monthStart.AddHours(1);//下一个小时

                    obj.StartTime = monthStart;
                    obj.EndTime = monthEnd;

                    var whereStart = " and timeid='" + obj.StartTime + "'";
                    var whereEnd = " and timeid='" + obj.EndTime + "'";

                    var whereStr = new Tuple<string, string>("#andStr#", whereStart);
                    var tupList = new List<Tuple<string, string>> { whereStr, col, table };
                    var res = EnergyMonthDataHandle.GetEnergyMonthDatas(tupList);
                    var valStart = 0m;
                    var valEnd = 0m;
                    if (res != null && res.Count > 0)
                        valStart = res[0].Val;
                    whereStr = new Tuple<string, string>("#andStr#", whereEnd);
                    tupList = new List<Tuple<string, string>> { whereStr, col, table };
                    res = EnergyMonthDataHandle.GetEnergyMonthDatas(tupList);
                    if (res != null && res.Count > 0)
                        valEnd = res[0].Val;
                    var val = valEnd - valStart;
                    var tempObj = new ResultEnergyMonthData
                    {
                        Cname = dev.Cname,
                        DevNum = dev.DevNum,
                        EnergyName = ((EnergyType)1).ToString(),
                        MonthStart = valStart,
                        MonthEnd = valEnd,
                        Val = val,
                        Id = id++
                       
                    };
                    result1.DevList.Add(tempObj);
                }
                #endregion

                #region excel处理
                var dt = new DataTable();
                dt.Columns.Add(new DataColumn("序号", typeof(int)));
                dt.Columns.Add(new DataColumn("开始时间", typeof(string)));
                dt.Columns.Add(new DataColumn("结束时间", typeof(string)));
                dt.Columns.Add(new DataColumn("名称", typeof(string)));
                dt.Columns.Add(new DataColumn("电数据", typeof(decimal)));


                #endregion
                


                var index = 0;
                foreach (var dev  in  devList )
                {
                    var row = dt.NewRow();
                    
                    var energyName = "";
                    var valS = 0m;
                    var valE = 0m;
                    var val = 0m;
                    var temp = (from p in result1.DevList where dev.DevNum == p.DevNum select p).ToList();/*依据设备号查看是否存在数据*/
                    if (temp.Count > 0)
                    {
                        energyName = temp[0].EnergyName;
                        valS = temp[0].MonthStart;
                        valE = temp[0].MonthEnd;
                        val = temp[0].Val;
                    }
                    else
                    {
                        energyName = ((EnergyType)obj.EnergyType).ToString();
                       
                    }
                    row[0] = index++;
                    row[2] = valS;
                    row[3] = valE;
                    row[1] = dev.Cname;
                    row[4] = val;
                    dt.Rows.Add(row);
                    
                }
                var tileRow = new List<TtileRow>();
                tileRow.Add(new TtileRow { C = 0, R = 0, Description = "" });
                tileRow.Add(new TtileRow { C = 0, R = 3, Description = obj.StartTime.ToString() + "~" + obj.EndTime.ToString() });
                tileRow.Add(new TtileRow { C = 3, R = 4 });

                var tempPath = AppDomain.CurrentDomain.BaseDirectory + "temp_file\\";
                var savePath = DateTime.Now.Ticks + ".xls";
                var templatePath = AppDomain.CurrentDomain.BaseDirectory + "templatefiles\\综合报表1.xls";
                ExportExcelHandle.ExportExcel(dt, tempPath + savePath, templatePath, tileRow);
                result.Exception = new ExceptionMsg { Exsg = "/temp_file/" + savePath, Success = true };
            }
            catch (Exception ed)
            {
                result.Exception = new ExceptionMsg { Exsg = ed.Message, Success = false };
            }
            return JsonConvert.SerializeObject(result);
        }
    }
}
