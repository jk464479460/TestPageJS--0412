using System.ServiceModel;
using System.ServiceModel.Web;
using WebReport.DataContact;

namespace WebReport.Interface
{
    [ServiceContract]
    public interface IEnergyMonthDataBll
    {
        [OperationContract]
        [WebInvoke(Method="POST",RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        string GetEnergyDataList(QueryEnergyStatistic queryObj);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        string GetPageInfo(QueryEnergyStatistic queryObj);


        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string ExportExcel();
    }
}
