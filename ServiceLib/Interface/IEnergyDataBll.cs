using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ServiceLib.Interface
{
    [ServiceContract]
    public interface IEnergyMonthDataBll
    {
        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string GetEnergyDataList(string Inputs);

        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string GetPageInfo(string Inputs);


        [OperationContract]
        [WebInvoke(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string ExportExcel();
    }
}
