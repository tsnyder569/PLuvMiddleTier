using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using TIC.PuppyLove.Services.Contracts.Common;
using TIC.PuppyLove.Services.Contracts.Match.Data;

namespace TIC.PuppyLove.Services.Contracts.Match.Service
{
    [ServiceContract]
    public interface IProfileMatchService
    {
        /// <summary>
        /// This is for JQuery
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetMatchByLocation",
                RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
                BodyStyle = WebMessageBodyStyle.Wrapped)]
        MatchResponse GetMatchByLocation(MatchRequest request);
    }
}
