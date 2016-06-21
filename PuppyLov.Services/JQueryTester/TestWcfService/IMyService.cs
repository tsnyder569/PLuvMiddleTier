using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using TestWcfService.Items;

namespace TestWcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IMyService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "/UpdateItems", Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool UpdateItems(IEnumerable<StatusItem> items);

        [OperationContract]
        [WebInvoke(UriTemplate = "/Update", Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool Update(StatusItem item);

        [OperationContract]
        [WebInvoke(UriTemplate = "/Delete", Method = "POST", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        void Delete(int ID);

        [OperationContract]
        [WebGet(UriTemplate = "ItemData/{value}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        [return: MessageParameter(Name = "results")]
        TestItem GetItemData(string value);
        
        [OperationContract]
        [WebGet(UriTemplate = "Items", RequestFormat=WebMessageFormat.Json, ResponseFormat=WebMessageFormat.Json, BodyStyle=WebMessageBodyStyle.Wrapped)]
        [return: MessageParameter(Name = "results")]
        List<TestItem> GetItems();
    }
}
