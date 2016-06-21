using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using TIC.PuppyLove.Services.Contracts.Common;
using TIC.PuppyLove.Services.Contracts.Config.Data;

namespace TIC.PuppyLove.Services.Contracts.Config.Service
{
    [ServiceContract]
    public interface IConfigService
    {
        /// <summary>
        /// Returns all entries in config table
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetAllConfigurations",
                RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
                BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ConfigurationResponse GetAllConfigurations();

        /// <summary>
        /// Returns specific config entry based on id or name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetSpecificConfiguration",
                RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
                BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ConfigurationResponse GetSpecificConfiguration(ConfigEntry request);

        /// <summary>
        /// Adds or Updates entries in the configuration table based on a collection passed
        /// in to the service.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/AddUpdateConfigurations",
                RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
                BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ConfigurationResponse AddUpdateConfigurations(ConfigMultiEntries request);

        /// <summary>
        /// Deletes entries in the configuration table based on a collection passed
        /// in to the service.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/DeleteConfigurations",
                RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
                BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ConfigurationResponse DeleteConfigurations(ConfigMultiEntries request);
    }
}