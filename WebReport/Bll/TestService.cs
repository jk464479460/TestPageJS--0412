using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;
using Newtonsoft.Json;
using WebReport.Interface;
using ITest = WebReport.Interface.ITest;
using Test = WebReport.Model.Test;

namespace WebReport.Bll
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TestService:ITest
    {
        private readonly HttpContext _context = HttpContext.Current;
        public string GetJson(City inputs)
        {
            var j = _context.Request.Form["inputs"];
var aa=            OperationContext.Current.RequestContext.RequestMessage;
            var a = JsonConvert.DeserializeObject<Test>(j);
           
            //var str = JsonConvert.SerializeObject(j);
            return JsonConvert.SerializeObject(a);
        }
    }
}
