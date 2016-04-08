using System.ServiceModel;
using System.ServiceModel.Web;

namespace WebReport.Interface
{
    [ServiceContract]
    public interface ITbdeviceBll
    {
        [OperationContract]
        [WebInvoke(Method="POST",RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        string GetDeviceList();
    }
}
