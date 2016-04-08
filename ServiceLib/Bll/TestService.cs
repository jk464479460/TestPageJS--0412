using System.Runtime.InteropServices;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Newtonsoft.Json;
using ServiceLib.Interface;
using ServiceLib.Model;
using System.Web;

namespace ServiceLib.Bll
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TestService:ITest
    {
        private readonly HttpContext _context = HttpContext.Current;
        public string GetJson()
        {
            var j = _context.Request.Form["inputs"];
var aa=            OperationContext.Current.RequestContext.RequestMessage;
            var a = JsonConvert.DeserializeObject<Test>(j);
           
            //var str = JsonConvert.SerializeObject(j);
            return JsonConvert.SerializeObject(a);
        }
    }
}
