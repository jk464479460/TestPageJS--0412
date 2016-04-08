using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WebReport.Interface
{
    [ServiceContract]
    public interface ITest
    {
        [OperationContract]
        [WebInvoke(Method="POST",RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string GetJson(City inputs);
    }
    [DataContract]
    public class City
    {
        [DataMember]
        public string Name { get; set; }
    }
}
