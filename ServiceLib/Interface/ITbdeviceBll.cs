using System.ServiceModel;
using System.ServiceModel.Web;

namespace ServiceLib.Interface
{
    [ServiceContract]
    public interface ITbdeviceBll
    {
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string GetDeviceList();
    }
}
