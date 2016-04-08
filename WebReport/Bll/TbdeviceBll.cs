using System;
using System.ServiceModel.Activation;
using Newtonsoft.Json;
using ExceptionMsg = WebReport.ResultView.ExceptionMsg;
using ITbdeviceBll = WebReport.Interface.ITbdeviceBll;
using ResultDeviceList = WebReport.ResultView.ResultDeviceList;
using TbdeviceData = WebReport.Dal.TbdeviceData;

namespace WebReport.Bll
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TbdeviceBll:ITbdeviceBll
    {

        public string GetDeviceList()
        {
            var result = new ResultDeviceList();
            try
            {
                var res = TbdeviceData.GetDevList(new Tuple<string, string>("#andStr#", ""));
                result.DevList = res;
                result.Exception=new ExceptionMsg{Exsg="", Success=true};
            }
            catch (Exception ed)
            {
                result.DevList = null;
                result.Exception = new ExceptionMsg { Exsg = ed.Message, Success = false };
            }
            return JsonConvert.SerializeObject(result);
        }
    }
}
