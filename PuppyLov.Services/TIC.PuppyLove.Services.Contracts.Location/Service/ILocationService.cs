using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using TIC.PuppyLove.Services.Contracts.Common;
using TIC.PuppyLove.Services.Contracts.Location.Data;

namespace TIC.PuppyLove.Services.Contracts.Location.Service
{

    [ServiceContract]
    public interface ILocationService
    {
        /// <summary>
        /// This is for JQuery
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/UpdateLocation",
                RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
                BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LocationResponse UpdateLocation(LocationData request);

        /// <summary>
        /// This is for jquery
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/RemoveLocation",
                RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
                BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        CommonResponse RemoveLocation(LocationData request);

        /// <summary>
        /// This is for jquery
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetCurrentLocations",
                RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
                BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LocationResponse GetCurrentLocations(LocationData request);

        /// <summary>
        /// This is for jquery
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetNearestNeighbors",
                RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
                BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        LocationResponse GetNearestNeighbors(NearestNeighborRequest request);
    }
}
