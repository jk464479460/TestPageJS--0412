using System;
using System.ServiceModel.Activation;
using System.Web.Management;
using Newtonsoft.Json;
using ServiceLib.Dal;
using ServiceLib.Interface;
using ServiceLib.ResultView;

namespace ServiceLib.Bll
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TbdeviceBll:ITbdeviceBll
    {

        public string GetDeviceList()
        {
            var result = new ResultView.ResultDeviceList();
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
