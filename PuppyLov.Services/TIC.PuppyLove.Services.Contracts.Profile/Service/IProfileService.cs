
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;
using TIC.PuppyLove.Services.Contracts.Profile.Data;
using TIC.PuppyLove.Services.Contracts.Common;


namespace TIC.PuppyLove.Services.Contracts.Profile.Service
{
    [ServiceContract] 
    public interface IProfileService
    {
        // This is complete goose crap

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userUpdate"></param>
        /// <returns></returns>
        
        [OperationContract]
        [WebInvoke(Method = "POST",
                   BodyStyle = WebMessageBodyStyle.WrappedRequest,
                   ResponseFormat = WebMessageFormat.Json,
                   RequestFormat = WebMessageFormat.Json
        )]
        CommonResponse CreateProfile(UserProfileData request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userequest"></param>
        /// <returns></returns>
        
        [OperationContract]
        [WebInvoke(Method = "POST",
                   BodyStyle = WebMessageBodyStyle.WrappedRequest,
                   ResponseFormat = WebMessageFormat.Json,
                   RequestFormat = WebMessageFormat.Json
        )]
        UserProfileData GetProfileData(UserProfileRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        
        [OperationContract]
        [WebInvoke(Method = "POST",
                   BodyStyle = WebMessageBodyStyle.WrappedRequest,
                   ResponseFormat = WebMessageFormat.Json,
                   RequestFormat = WebMessageFormat.Json
        )]
        ProfileQuestions GetQuestions(ProfileQuestionRequest request);

        /// <summary>
        ///  
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        
        [OperationContract]
        [WebInvoke(Method = "POST",
                   BodyStyle = WebMessageBodyStyle.WrappedRequest,
                   ResponseFormat = WebMessageFormat.Json,
                   RequestFormat = WebMessageFormat.Json
                   
        )]         
        LoginResponse Login(UserProfileRequest request);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
                   BodyStyle = WebMessageBodyStyle.WrappedRequest,
                   ResponseFormat = WebMessageFormat.Json,
                   RequestFormat = WebMessageFormat.Json
        )] 
        CommonResponse UpdateProfile(UserProfileData request);

        [OperationContract]
        [WebInvoke(Method = "POST",
                   BodyStyle = WebMessageBodyStyle.WrappedRequest,
                   ResponseFormat = WebMessageFormat.Json,
                   RequestFormat = WebMessageFormat.Json
        )]
        ChoiceProfile GetMatchProfileChoiceStatus(ProfileChoiceRequest request);

        [OperationContract]
        [WebInvoke(Method = "POST",
                   BodyStyle = WebMessageBodyStyle.WrappedRequest,
                   ResponseFormat = WebMessageFormat.Json,
                   RequestFormat = WebMessageFormat.Json

        )]
        CommonResponse ValidateLoginUserName(UserProfileRequest request);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/GetPhotosByUserID",
                   BodyStyle = WebMessageBodyStyle.WrappedRequest,
                   ResponseFormat = WebMessageFormat.Json,
                   RequestFormat = WebMessageFormat.Json

        )]
        ProfilePhotos GetPhotosByUserID(ProfilePhoto request);
    }
}